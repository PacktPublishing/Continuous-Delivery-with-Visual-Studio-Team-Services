using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Wonders.Api.Models;

namespace Wonders.IntegrationTests
{
    [TestClass]
    public class WondersApiTests
    {
        private static WondersApiTestServer _server;
        private static HttpClient _client;
        private static WondersControllerDriver _sut;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            _server = new WondersApiTestServer();
            _server.Init();
            _client = _server.Client;
            _sut = new WondersControllerDriver(_client);
        }

        [TestMethod]
        public void AddWondersTest()
        {
            //Arrange
            var wonder = new Wonder
            {
                Id = 12,
                Title = "Wonder1",
                Description = "This wonder is added as part of integration tests",
                Country = "Country1"
            };

            //Act
            var result = _sut.Create(wonder);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OkResult>();
        }

        [TestMethod]
        public void GetAllWondersTest()
        {
            //Arrange
            var wonder = new Wonder
            {
                Id = 14,
                Title = "Wonder2",
                Description = "This wonder is added as part of integration tests",
                Country = "Country1"
            };
            _sut.Create(wonder);

            //Act
            var result = _sut.Get();

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OkObjectResult>();

            var content = result as OkObjectResult;
            content.ShouldNotBeNull();

            var wonders = content.Value as IEnumerable<Wonder>;
            wonders.ShouldNotBeNull();
            wonders.Any(w => w.Title == "Wonder2").ShouldBeTrue();
        }

        [TestMethod]
        public void GetByNameTest()
        {
            //Arrange
            var wonder = new Wonder
            {
                Id = 24,
                Title = "Wonder4",
                Description = "This wonder is added as part of integration tests",
                Country = "Country1"
            };
            _sut.Create(wonder);

            //Act
            var result = _sut.Get("Wonder4");
            
            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OkObjectResult>();

            var content = result as OkObjectResult;
            content.ShouldNotBeNull();

            var actual = content.Value as Wonder;
            actual.ShouldNotBeNull();
            actual.Title.ShouldBe("Wonder4");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            //Cleanup
            _server.Dispose();
        }
    }
}
