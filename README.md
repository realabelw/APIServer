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
$ cd ├APIServer

followed by

$ dotnet restore


-----------------
Development/Debugging/Deploying/Hosting the Application
-----------------
Option 1. Deploying or hosting in IIS Server. The published solution can be found in folder: bin\Release\netcoreapp3.1\publish\

-----------------
Endpoints
-----------------
http://{ip_address}:{port}/api/restaurants/{location}/{term}
http://{ip_address}:{port}/api/restaurants/{id}



Option 2. Development/Debugging : using Visual Studio 2019
-----------------
Endpoints
-----------------
http://localhost:36114/api/restaurants/{location}/{term} e.g. http://localhost:36114/api/restaurants/usa/sushi
http://localhost:36114/api/restaurants/{id} e.g. http://localhost:36114/api/restaurants/123903


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

Tests for Controller Actions
----------------------------

------------------------------------------------------------------------------------
Method								Status 			Test Case Name
------------------------------------------------------------------------------------
api/restaurants/{location}/{term}		
									OK				GetRestaurants_ShouldReturnOkResponse_WhenDataFound
									OK				GetRestaurants_ShouldReturnNotFound_WhenDataNotFound
									OK				GetRestaurants_ShouldReturnBadResponse_WhenRequestNotValidInputIsNullOrEmpty
									OK				GetRestaurants_ShouldReturnOkResponse_WhenValidRequest
									OK				GetRestaurants_ShouldReturnInternalServerError_WhenErrorReturnedFromAPI
									
									

api/restaurants/{id}		
									OK				GetRestaurantsById_ShouldReturnOkResponse_WhenDataFound
									OK				GetRestaurantsById_ShouldReturnNotFound_WhenDataNotFound
									OK				GetRestaurantsById_ShouldReturnBadResponse_WhenRequestNotValidInputIsNullOrEmpty
									OK				GetRestaurantsById_ShouldReturnOkResponse_WhenValidRequest								




----------------------------
Yelp Fusion API Details
----------------------------
Client ID
AGHG9OSiGMHxvHd3J6c1oQ

API Key
DmNBSx8-kLn2NBAlaPxjoP-gOKgQUl1ea1LJYmGuoGcbuWAto1BHFzGShzKb8nz7J-AHi3gyrAl0kutEhNXSlMRSGOx4FAslzbSDv4g-LVfl7V3kSfkx8ixQv2sYZHYx

Base Address
-----------
https://api.yelp.com/v3/businesses/

App Name
RestaurantAPIServer


