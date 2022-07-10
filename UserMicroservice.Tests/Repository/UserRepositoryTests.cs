using NUnit.Framework;
using UserMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Services;
using UserMicroservice.Models;
using Moq;
using UserMicroservice.Tests;
using System.Threading.Tasks;
using UserMicroservice.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace UserMicroservice.Repository.Tests
{
    [TestFixture()]
    public class UserRepositoryTests
    {
        UserRepository _repo;

        [SetUp]
        public void Setup()
        {
            Database database = new Database(new DbContextOptions<Database>());
            var authorizationService_Api = new Mock<IAuthorizationService_Api>();

            authorizationService_Api.Setup(x => x.GetAuthTokenAsync(MockData.GetAppUsers()[0]))
                .ReturnsAsync(new AuthTokenPayload { 
                    accessToken = "123455464654asdfafaf"
                });

            _repo = new UserRepository(database, authorizationService_Api.Object);
        }

        [Test()]
        public void UserRepositoryTest()
        {

        }

        [Test()]
        public void DeleteUserTest()
        {

        }

        [Test()]
        public void GetAllUsersTest()
        {

        }

        [Test()]
        public void GetAppUserTest()
        {

        }

        [Test()]
        public async Task InsertUserTest()
        {
            var res = await _repo.InsertUser(new AppUser(
                name: "Naman Garg",
                email: "naman.garg@cognizant.com",
                password: "password123",
                mobile: "9876543210",
                role: UserRole.ADMIN,
                registrationDate: DateTime.UtcNow
            ));

            Assert.IsNotNull(res);
            Assert.AreEqual(res.Role, UserRole.ADMIN, "Role Not Verified");
            Assert.AreEqual(res.Email, "naman.garg@cognizant.com", "Email Not Verified");

            var hasher = new PasswordHasher<AppUser>();
            var verified = hasher.VerifyHashedPassword(res, res.Password, "password123");

            Assert.AreEqual(PasswordVerificationResult.Success, verified, "Password Not Verified");

        }

        [Test()]
        public async Task LoginUserTest()
        {
            var res = await _repo.LoginUser(new LoginRequest
            {
                Email = "aditya.pal@cognizant.com",
                Password = "password123"
            });

            Assert.IsNotNull(res);
            Assert.AreEqual("123455464654asdfafaf", res.accessToken);
        }

        [Test()]
        public async Task UpdateUserTest()
        {
            var user = new AppUser(
                name: "Naman Garg",
                email: "naman.garg@cognizant.com",
                password: "password123",
                mobile: "9876543210",
                role: UserRole.ADMIN,
                registrationDate: DateTime.UtcNow
            );

            user.Id = 2;
            var res = await _repo.UpdateUser(user);

            Assert.IsNotNull(res);
            Assert.AreEqual(res.Role, UserRole.ADMIN, "Role Not Verified");
            Assert.AreEqual(res.Email, "naman.garg@cognizant.com", "Email Not Verified");

            var hasher = new PasswordHasher<AppUser>();
            var verified = hasher.VerifyHashedPassword(res, res.Password, "password123");

            Assert.AreEqual(PasswordVerificationResult.Success, verified, "Password Not Verified");


            var userFromDb = await _repo.GetAppUser(user.Id);

            Assert.IsNotNull(userFromDb);
            Assert.AreEqual(user.Role, userFromDb.Role, "Role Not Updated");
            Assert.AreEqual(user.Email, userFromDb.Email, "Email Not Updated");

            verified = hasher.VerifyHashedPassword(user, user.Password, userFromDb.Password);

            Assert.AreEqual(PasswordVerificationResult.Success, verified, "Password Not Updated");
        }
    }
}