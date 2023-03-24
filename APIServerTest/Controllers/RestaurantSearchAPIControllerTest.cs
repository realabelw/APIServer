using System;
using Xunit;
using AutoFixture;
using Moq;
using FluentAssertions;
using APIServer.Abstractions;
using APIServer.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using APIServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIServerTest
{
    public class RestaurantSearchAPIControllerTest
    {

        //Tools to be used: 
        //Moq - to mimic real class objects and minimize dependecies.
        //Autofixture - to minimize the Arrange phase of your unit tests.
        //Fluent Assertions - for readable assertions to specify the expected outcome.

        private readonly IFixture _fixture;
        private readonly Mock<IRestaurantSearchService> _serviceMock;
        private readonly Mock<IMemoryCache> _memoryCacheMock;
        private readonly Mock<ILogger<RestaurantSearchAPIController>> _loggerMock;
        private readonly RestaurantSearchAPIController _controllerTest;

        public RestaurantSearchAPIControllerTest()
        {
            //create the mock object implementations in memory

            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IRestaurantSearchService>>(); //fixture will create an instance from the service interface
            _memoryCacheMock = _fixture.Freeze<Mock<IMemoryCache>>();

            //_loggerMock = _fixture.Freeze<Mock<ILogger<RestaurantSearchAPIController>>>();
            _loggerMock = new Mock<ILogger<RestaurantSearchAPIController>>();

            _controllerTest = new RestaurantSearchAPIController(_serviceMock.Object, _loggerMock.Object, _memoryCacheMock.Object);
        }

        [Fact]
        public async Task GetRestaurants_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arrange
            string location = _fixture.Create<string>(); //generate random string data
            string term = _fixture.Create<string>(); //generate random string data

            var searchResultMock = _fixture.Create<BusinessSearchResult>(); //mock result returned from Yelp API list of restaurants
            searchResultMock.Error = string.Empty; //this is only used in testing error returns
            
            _serviceMock.Setup(x => x.GetRestaurants(location, term)).ReturnsAsync(searchResultMock); //the call to the service method returns the mock object

            var restaurants = searchResultMock.businesses;

            //Act
            //this is the call to the controller method in a synchronous context
            var result = await _controllerTest.Restaurants(location, term).ConfigureAwait(false);
            //var result = await _controllerTest.Restaurants(location, term);

            //Asset
            //Assert.NotNull(restaurants);
            result.Should().NotBeNull();
            restaurants.Should().BeAssignableTo<List<Restaurant>>();
            result.Should().BeAssignableTo<OkObjectResult>();
            //verify that our service method is getting called
            _serviceMock.Verify(x => x.GetRestaurants(location, term), Times.Once());
        }

        [Fact]
        public async Task GetRestaurants_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arrange
            string location = _fixture.Create<string>(); //generate random string data
            string term = _fixture.Create<string>(); //generate random string data

            var searchResultMock = _fixture.Create<BusinessSearchResult>(); //mock result returned from Yelp API list of restaurants
            searchResultMock.Error = string.Empty; //this is only used in testing error returns
            
            _serviceMock.Setup(x => x.GetRestaurants(location, term)).ReturnsAsync(searchResultMock); //the call to the service method returns the mock object

            var restaurants = searchResultMock.businesses;

            //Act
            //this is the call to the controller method in a synchronous context
            var result = await _controllerTest.Restaurants(location, term).ConfigureAwait(false);
            //var result = await _controllerTest.Restaurants(location, term);

            //Asset
            //Assert.NotNull(restaurants);
            result.Should().NotBeNull();
            restaurants.Should().BeAssignableTo<List<Restaurant>>();
            result.Should().BeAssignableTo<OkObjectResult>();
            //result.Should().BeAssignableTo<ActionResult<List<Restaurant>>>();

            //verify that our service method is getting called
            _serviceMock.Verify(x => x.GetRestaurants(location, term), Times.Once());
        }

        [Fact]
        public async Task GetRestaurants_ShouldReturnNotFound_WhenDataNotFound()
        {
            //Arrange
            string location = _fixture.Create<string>(); //generate random string data
            string term = _fixture.Create<string>(); //generate random string data

            BusinessSearchResult searchResultMock = null; //search result returned nothing from Yelp API
            _serviceMock.Setup(x => x.GetRestaurants(location, term)).ReturnsAsync(searchResultMock); //the call to the service method returns the mock object

            var restaurants = searchResultMock?.businesses;

            //Act
            //this is the call to the controller method in a synchronous context
            var result = await _controllerTest.Restaurants(location, term).ConfigureAwait(false);
            //var result = await _controllerTest.Restaurants(location, term);

            //Asset
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();

            //verify that our service method is getting called
            _serviceMock.Verify(x => x.GetRestaurants(location, term), Times.Once());
        }

        [Fact]
        public async Task GetRestaurants_ShouldReturnBadResponse_WhenRequestNotValidInputIsNullOrEmpty()
        {
            //Arrange
            string location = null; //supplied empty parameter values
            string term = null; //supplied empty parameter values

            BusinessSearchResult searchResultMock = null; //this is for a case the result is empty from Yelp API list of restaurants
            _serviceMock.Setup(x => x.GetRestaurants(location, term)).ReturnsAsync(searchResultMock); //the call to the service method returns the mock object

            var restaurants = searchResultMock?.businesses;

            //Act
            //this is the call to the controller method in a synchronous context
            var result = await _controllerTest.Restaurants(location, term).ConfigureAwait(false);
            //var result = await _controllerTest.Restaurants(location, term);

            //Asset
            //Assert.NotNull(restaurants);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();

            //verify that our service method is getting called
            _serviceMock.Verify(x => x.GetRestaurants(location, term), Times.Never());
        }

        [Fact]
        public async Task GetRestaurants_ShouldReturnInternalServerError_WhenErrorReturnedFromAPI()
        {
            //Arrange
            string location = _fixture.Create<string>(); //generate random string data
            string term = _fixture.Create<string>(); //generate random string data

            var searchResultMock = _fixture.Create<BusinessSearchResult>(); //mock result returned from Yelp API list of restaurants
            searchResultMock.Error = _fixture.Create<string>();  // server error occurred fetching data from API

            _serviceMock.Setup(x => x.GetRestaurants(location, term)).ReturnsAsync(searchResultMock); //the call to the service method returns the mock object

            var restaurants = searchResultMock?.businesses;

            //Act
            //this is the call to the controller method in a synchronous context
            var result = await _controllerTest.Restaurants(location, term).ConfigureAwait(false);
            //var result = await _controllerTest.Restaurants(location, term);

            //Asset
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<StatusCodeResult>(); //should return 500 Internal Server Error
            ((StatusCodeResult)result).StatusCode.Should().Be(500); //verify that the return code is 500
            //verify that our service method is getting called
            _serviceMock.Verify(x => x.GetRestaurants(location, term), Times.Once());
        }




        [Fact]
        public async Task GetRestaurantsById_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arrange
            string id = _fixture.Create<string>(); //generate random string data

            var searchResult = _fixture.Create<Restaurant>(); //mock result returned from Yelp API list of restaurants
            _serviceMock.Setup(x => x.GetRestaurant(id)).ReturnsAsync(searchResult); //the call to the service method returns the mock object

            //Act
            //this is the call to the controller method in a synchronous context
            var result = await _controllerTest.Restaurants(id).ConfigureAwait(false);

            //Asset
            result.Should().NotBeNull();
            searchResult.Should().BeAssignableTo<Restaurant>();
            result.Should().BeAssignableTo<OkObjectResult>();

            //verify that our service method is getting called
            _serviceMock.Verify(x => x.GetRestaurant(id), Times.Once());
        }

        [Fact]
        public async Task GetRestaurantsById_ShouldReturnNotFound_WhenDataNotFound()
        {
            //Arrange
            string id = _fixture.Create<string>(); //generate random string data

            Restaurant searchResultMock = null; //this is for a case the result is empty from Yelp API list of restaurants
            _serviceMock.Setup(x => x.GetRestaurant(id)).ReturnsAsync(searchResultMock); //the call to the service method returns the mock object

            //Act
            //this is the call to the controller method in a synchronous context
            var result = await _controllerTest.Restaurants(id).ConfigureAwait(false);

            //Asset
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();

            //verify that our service method is getting called
            _serviceMock.Verify(x => x.GetRestaurant(id), Times.Once());
        }

        [Fact]
        public async Task GetRestaurantsById_ShouldReturnBadResponse_WhenRequestNotValidInputIsNullOrEmpty()
        {
            //Arrange
            string id = null; //supplied empty parameter values

            Restaurant searchResultMock = null; //this is for a case the result is empty from Yelp API list of restaurants
            _serviceMock.Setup(x => x.GetRestaurant(id)).ReturnsAsync(searchResultMock); //the call to the service method returns the mock object

            //Act
            //this is the call to the controller method in a synchronous context
            var result = await _controllerTest.Restaurants(id).ConfigureAwait(false);

            //Asset
            //Assert.NotNull(restaurants);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();

            //verify that our service method is getting called
            _serviceMock.Verify(x => x.GetRestaurant(id), Times.Never());
        }

    }
}
