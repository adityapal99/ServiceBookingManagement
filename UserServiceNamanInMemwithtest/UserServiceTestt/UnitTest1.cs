using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UserMicroservice.Controllers;
using UserMicroservice.Repository.IRepository;

namespace UserServiceTestt
{
    public class Tests
    {

        private readonly IAppUserRepository _userRep;
        [Test]
       
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            /// Arrange
            var todoService = new Mock<IAppUserRepository>();
            todoService.Setup(_ => _.GetUsers()).ReturnsAsync(mockdata.GetAppUsers());
            var sut = new UsersController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await sut.GetUsers();

            // /// Assert
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}