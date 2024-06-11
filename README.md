# UniFiProtectG4Doorbell
## Overview
The UniFiProtectG4Doorbell project is designed to interact with and manage UniFi Protect G4 Doorbell systems. This project is divided into several parts, including data models, services, and testing modules, ensuring a well-structured and maintainable codebase.

## Project Structure
* **UniFiProtectG4Doorbell.sln**: The main solution file for the project.
* **UniFiProtectG4Doorbell.Data**: Contains the data access layer, including entity definitions and database context.
* **UniFiProtectG4Doorbell.Data.Tests**: Includes unit tests for the data access layer to ensure data integrity and consistency.
* **UniFiProtectG4Doorbell.Models**: Defines the models used throughout the project.
* **UniFiProtectG4Doorbell.Services**: Implements the business logic and service layer for the application.
* **UniFiProtectG4Doorbell.Services.Tests**: Contains unit tests for the services to ensure the correctness of the business logic.
* **UniFiProtectG4Doorbell.FrontEnd** (Pending): The UI to manage the devices and sounds. It also uses Hangfire for scheduled tasks to update the sound on the device depending on the schedule.

## Getting Started
### Prerequisites
* .NET SDK
* Visual Studio or any other compatible IDE
* UniFi Protect G4 Doorbell system

### Setup
1. **Clone the repository**:
   ```
   git clone https://github.com/your-repo/UniFiProtectG4Doorbell.git
   cd UniFiProtectG4Doorbell
   ```
2. **Open the solution file**:
   Open UniFiProtectG4Doorbell.sln in Visual Studio.

3. **Build the solution**:
   Ensure that all dependencies are restored and build the solution.

### Running the Tests
To run the unit tests, use the Test Explorer in Visual Studio or execute the following command in the terminal:

```
dotnet test
```


## Acknowledgments
[https://www.youtube.com/watch?v=wjw0OUBWLdQ](https://www.youtube.com/watch?v=wjw0OUBWLdQ)