ASP.NET Core API Server (Restaurant Search API)
-----------------
This is an application that you can use to search for nearby restaurants using the Yelp Fusion API. This is built using .NET Core 3.1.

-----------------
Project Structure
-----------------
│________________________________________________________________________________  
│  
├APIServer  
├── Controllers  
│   └── RestaurantSearchAPIController.cs  
├── BusinessLayer  
│   └── RestaurantBusinessLayer.cs  
├── DataAccessLayer  
│   └── DataAccessLayer.cs  
├── Models  
│   └── Restaurant.cs  
│   └── Location.cs  
│   └── Category.cs  
│   └── Coordinates.cs  
│   └── SearchResult.cs  
├── Program.cs  
├── Properties  
│   └── launchSettings.json  
├── README.md  
├── Startup.cs  
├── appsettings.json  
├── appsettings.Development.json  
├── bin  
│   └── Debug  
│   └── Release  
│       └──netcoreapp3.1  
│          └──publish  
├── APIServer.csproj  
│  
├APIServerTest  
├── bin  
│   └── Debug  
├── Controllers  
│   └── RestaurantSearchAPIControllerTest.cs  
├── APIServer.csproj  
│  
├APIServer.sln  
├.vs  
├.git  
├.gitignore  
├.gitattributes  
├.README.md  
│________________________________________________________________________________  
  
->Startup.cs is .NET Core Web API startup & path routing config  
->Program.cs is .NET Core Web API environment variable mapping config  
->configs folder will contain .NET Core Web API centralized config structure   
->appsettings.Development.json is .NET Core Web API development environment config  
->publish folder contains deployable app ready for hosting e.g. in IIS server  
  
-----------------  
Setting Up  
-----------------  
To setup this project, you need to clone the git repo  
  
$ git clone https://github.com/realabelw/apiserver.git  
$ cd APIServer  
  
followed by  
  
$ dotnet restore  
  
  
-----------------  
Running the Application  
-----------------  
Option 1. Hosting in IIS Server. [folder: bin\Release\netcoreapp3.1\publish\]  
  
Option 2. Development/Debugging : using Visual Endpoints  

Endpoints  
-----------------  
http://{ip_address}:{port}/api/restaurants/{location}/{term}  
http://{ip_address}:{port}/api/restaurants/{id}  
  
  
-----------------  
Notes  
-----------------  
->Not configured for HTTPS  
->Not implemented authorizations  
->Paging not implemented  
->The environment is set to Development  
  
-----------------  
Prerequisites  
-----------------  
->.Net Core 3.1  
->System.Net.Http.Json  
->swishbuckle swagger OpenAPI for documentation, URL: http://localhost:36114/swagger/index.html  
->Visual Studio 2019  
  
  
-----------------  
Unit Tests  
-----------------  
Packages used:   
xUnit for .NET Framework. The test cases built upon AAA (Arrange, Act, and Assert)  
Moq - to mimic real class objects and minimize dependecies.  
Autofixture - to minimize the Arrange phase of your unit tests.  
FluentAssertions - for readable assertions to specify the expected outcome.  
  
Tests on Controller Actions  
----------------------------  
api/restaurants/{location}/{term}  	
----------------------------
  
GetRestaurants_ShouldReturnOkResponse_WhenDataFound  
GetRestaurants_ShouldReturnNotFound_WhenDataNotFound  
GetRestaurants_ShouldReturnBadResponse_WhenRequestNotValidInputIsNullOrEmpty  
GetRestaurants_ShouldReturnOkResponse_WhenValidRequest  
GetRestaurants_ShouldReturnInternalServerError_WhenErrorReturnedFromAPI  
  
  
api/restaurants/{id}  
----------------------------

GetRestaurantsById_ShouldReturnOkResponse_WhenDataFound  
GetRestaurantsById_ShouldReturnNotFound_WhenDataNotFound  
GetRestaurantsById_ShouldReturnBadResponse_WhenRequestNotValidInputIsNullOrEmpty  
GetRestaurantsById_ShouldReturnOkResponse_WhenValidRequest  
 

----------------------------  
Yelp Fusion API Details  
----------------------------  
API Key
<appsettings>  
  
Base Address  
https://api.yelp.com/v3/businesses/  
  
App Name  
RestaurantAPIServer  
  

