
using Renci.SshNet;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace UniFiProtectG4Doorbell.Services.Helpers
{
    public static class sshManager
    {
        public static bool isIpValid(string ipString)
        {
            if ((string.IsNullOrWhiteSpace(ipString) || ((ipString.AsSpan().Count(":") != 7 || ipString.Contains("::")) && ipString != "::1")) && ipString.AsSpan().Count(".") != 3)
            {
                return false;
            }

            try
            {
                IPAddress address = IPAddress.Parse(ipString);
                return address.AddressFamily == AddressFamily.InterNetworkV6 || address.AddressFamily == AddressFamily.InterNetwork;
            }
            catch
            {
                return false;
            }
        }

        public static bool testConnection(string host, string username, string password)
        {
            try
            {
                bool connected = false;
                using (var client = new SshClient(host, username, password))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        connected = true;
                    }
                    client.Disconnect();
                }
                return connected;
            }
            catch
            {

            }

            return false;
        }

        public static bool uploadFile(string host, string username, string password, string sourcePath, string destinationPath)
        {
            bool result = false;
            using (ScpClient scpClient = new ScpClient(host, username, password))
            {
                scpClient.Connect();
                uploadFile(scpClient, sourcePath, destinationPath);
                scpClient.Disconnect();
                result = true;
            }
            return result;
        }

        public static bool uploadFile(ScpClient client, string sourcePath, string destinationPath)
        {

            if (File.Exists(sourcePath))
            {
                if (!client.IsConnected)
                {
                    client.Connect();
                }
                
                client.Upload(new FileInfo(sourcePath), destinationPath);

                return true;
            }
            else
            {
                throw new FileNotFoundException($"File at location {sourcePath} not found. Please verify the path and file and try again.");
            }
        }

        public static void killProcess(string host, string username, string password, string processName)
        {

            using (SshClient client = new SshClient(host, username, password))
            {
                client.Connect();
                killProcess(client, processName);
                client.Disconnect();
            }
        }

        public static void killProcess(SshClient client, string processName)
        {
            if(!client.IsConnected)
            {
                client.Connect();
            }

            Models.SshProcess? processModel = getProcessList(client).Where(w => w.COMMAND?.ToLower() == processName).FirstOrDefault();
            if (processModel != null)
            {
                client.CreateCommand("kill -TERM " + processModel.PID).Execute();
                client.Disconnect();
            }
            else
            {
                throw new Exception($"Process Item for {processName} not found");
            }
        }

        public static List<Models.SshProcess> getProcessList(string host, string username, string password)
        {
            List<Models.SshProcess> pmList = new ();
            using (SshClient client = new SshClient(host, username, password))
            {
                client.Connect();
                pmList = getProcessList(client);
                client.Disconnect();
            }

            return pmList;
        }

        public static List<Models.SshProcess> getProcessList(SshClient client)
        {
            if (!client.IsConnected)
            {
                client.Connect();
            }

            SshCommand command = client.CreateCommand("ps");
            IAsyncResult asyncResult = command.BeginExecute();

            Task<List<Models.SshProcess>> task = Task.Factory.FromAsync(asyncResult, ar =>
            {
                List<Models.SshProcess> processList = new ();
                StreamReader streamReader = new StreamReader(command.OutputStream);

                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {

                    string formatedLine = Regex.Replace(line.Trim(), "\\s+", ",");

                    string[] strArray = formatedLine.Split(",");

                    Models.SshProcess process = new ()
                    {
                        PID = strArray[0],
                        USER = strArray[1],
                        VSZ = strArray[2],
                        STAT = strArray[3],
                        COMMAND = strArray[4]
                    };
                    processList.Add(process);
                }

                return processList;
            });

            task.Wait();

            command.EndExecute(asyncResult);

            return task.Result;

        }
    }
}
