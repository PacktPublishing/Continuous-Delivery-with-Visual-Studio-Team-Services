using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wonders.Api.Controllers;
using Wonders.Api.Data;
using Wonders.Api.Models;

namespace Wonders.Api.Tests
{
    [TestClass]
    public class WondersControllerTests
    {
        private Mock<IWondersRepository> _repositoryMock;
        private WondersController _sut;

        [TestInitialize]
        public void Initialize()
        {
            _repositoryMock = new Mock<IWondersRepository>();

            _sut = new WondersController(_repositoryMock.Object);
        }

        [TestMethod]
        [TestCategory("Unit tests")]
        public void GetShouldReturnOkObjectResultIfQueryReturnsElements()
        {
            //Arrange
            var wonders = new List<Wonder>
            {
                new Wonder {Id = 1, Title = "Test1"},
                new Wonder {Id = 2, Title = "Test2"}
            };
            _repositoryMock
                .Setup(x => x.All())
                .Returns(Task.FromResult(wonders.AsEnumerable()));

            //Act
            var result = _sut.Get().Result;

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OkObjectResult>();
            var content = result as OkObjectResult;
            content.ShouldNotBeNull();
            content.Value.ShouldBe(wonders);
        }

        [TestMethod]
        [TestCategory("Unit tests")]
        public void GetByNameShouldReturnOkObjectResultIfQueryReturnsElements()
        {
            //Arrange
            var wonder = new Wonder
            {
                Id = 1,
                Title = "Test1"
            };
            _repositoryMock
                .Setup(x => x.One("Test1"))
                .Returns(Task.FromResult(wonder));

            //Act
            var result = _sut.Get("Test1").Result;

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OkObjectResult>();
            var content = result as OkObjectResult;
            content.ShouldNotBeNull();
            content.Value.ShouldBeOfType<Wonder>();
            content.Value.ShouldBe(wonder);
        }

        [TestMethod]
        [TestCategory("Unit tests")]
        public void GetShouldReturnEmptyContentResultIfQueryReturnsNoElements()
        {
            //Arrange
            _repositoryMock
                .Setup(x => x.All())
                .Returns(Task.FromResult(default(IEnumerable<Wonder>)));

            //Act
            var result = _sut.Get().Result;

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<NoContentResult>();
        }

        [TestMethod]
        [TestCategory("Unit tests")]
        public void GetByNameShouldReturnEmptyContentResultIfQueryReturnsNoElements()
        {
            //Arrange
            _repositoryMock
                .Setup(x => x.One(It.IsAny<string>()))
                .Returns(Task.FromResult(default(Wonder)));

            //Act
            var result = _sut.Get("Test3").Result;

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}
