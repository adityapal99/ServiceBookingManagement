using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationMicroservice.Tests
{
    class AuthneticationRepository
    {

        IAuthenticationService authService;
        AppUser user = new AppUser
        {
            Id = 4,
            Name = "Ayush Raj Vaish",
            Email = "ayush.vaish@cognizant.com",
            Mobile = "9876543210",
            Role = Models.Enums.UserRole.USER,
            RegistrationDate = DateTime.UtcNow
        };


        [SetUp]
        public void Setup()
        {
            var authRepoMoq = new Mock<IAuthenticationService>();
            var loggerMoq = new Mock<ILogger<IAuthenticationService>>();

            authRepoMoq.Setup(x => x.GenerateJwtToken(user))
                .Returns("a");
        }

    public string GenerateJwtToken(AppUser user)
    {
        return "1234";
    }

    [Test()]
    public void Test1()
    {
            var response = authService.GetAuthToken(user);

            Assert.AreEqual(response.Value, );
    }

}
}
}
