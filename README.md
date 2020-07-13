# Etherscan
Etherscan API serving as API for retrieving transaction for block
* Configuration
* More

Contains:
* Etherscan API - .NET CORE 3.1 API

# How to build & run
## API
* Install dotnet core 3.1
* `dotnet restore`
* `dotnet build`
* `dotnet run`

## Environments
Environments in use within the project are all that exist within the current hosting heirarchy. Defined within the **ASPNETCORE_ENVIRONMENT** variable on the host machine.
* Development
  * The local Developer computer
* DEV
  * The Horizon Development environment.
* UAT
  * The Horizon User Acceptance testing environment.
* PRD
  * The Horizon Production environment.

## Structure
Project structure for easier development (with description where necessary).
* Constants
  *  String (and other) constants used in more than 1 place within the application
* Controllers
* Enums
* Extensions
  * Class extensions such as for IHostingEnvironment.IsUat()
* Models
  * Data storage models that are persisted within the database.
* Services
  * Services (usually added as singletons) to run backend calculations and whatnot.
* ViewModels
  * Adaptor models to pass data to UI elements for backend processing instead of frontend.
  

