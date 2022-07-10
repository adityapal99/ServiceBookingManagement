using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Exceptions;
using UserMicroservice.Models;
using UserMicroservice.Models.Enums;
using UserMicroservice.Services;

namespace UserMicroservice.Repository
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly IAuthorizationService_Api _authApi;
        //public readonly InMemoryDatabase _db;

        private static int id = 5;

        public static List<AppUser> Users = new List<AppUser>
        {
            new AppUser() {Id=1, Name="AppUser1", Email="Make1", Password="Medel1", Mobile="9000000000", Role = UserRole.ADMIN, RegistrationDate = System.DateTime.Now},
            new AppUser() {Id=2, Name="AppUser2", Email="Make2", Password="Medel2", Mobile="8000000000", Role = UserRole.ADMIN, RegistrationDate = System.DateTime.Now},
            new AppUser() {Id=3, Name="AppUser3", Email="Make3", Password="Medel3", Mobile="7000000000", Role = UserRole.ADMIN, RegistrationDate = System.DateTime.Now},
            new AppUser() {Id=4, Name="AppUser4", Email="Make4", Password="Medel4", Mobile="6000000000", Role = UserRole.ADMIN, RegistrationDate = System.DateTime.Now},
        };


        public InMemoryUserRepository(/*InMemoryDatabase db, */IAuthorizationService_Api authApi)
        {
            _authApi = authApi;
            //_db = db;
        }

        public async Task<AppUser> InsertUser(AppUser user)
        {

            user.Id = id;
            id++;
            Users.Add(user);
            return await Task.FromResult(user);
        }

        public Task DeleteUser(int id)
        {
            AppUser user = Users.Find(x => x.Id == id);
            Users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<AppUser> GetAppUser(int id)
        {

            AppUser user = Users.Find(x => x.Id == id);
            return await Task.FromResult(user);
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            List<AppUser> users = Users;
            return await Task.FromResult(users);
        }

        public async Task<AppUser> UpdateUser(AppUser user)
        {
            AppUser Original = Users.Find(x => x.Id == user.Id);

            if (Original == null) throw new UserNotFoundException();

            Original.Name = user.Name;
            Original.Email = user.Email;
            Original.Password = user.Password;
            Original.RegistrationDate = user.RegistrationDate;
            return await Task.FromResult(user);
        }

        public async Task<AuthTokenPayload> LoginUser(LoginRequest request)
        {
            AppUser user = Users.Where(u => u.Email == request.Email).FirstOrDefault();

            if (user == default)
            {
                throw new UserNotFoundException();
            }

            //var hasher = new PasswordHasher<AppUser>();

            //var verify = hasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (user.Password != request.Password)
            {
                throw new UserNotFoundException(message: "Password doesnot match");
            }

            var payload = await _authApi.GetAuthTokenAsync(user);

            return payload;
        }
    }
}
