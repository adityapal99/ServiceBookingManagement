using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;
using UserMicroservice.Repository.IRepository;

namespace UserMicroservice.Repository
{
    public class AppUserInmemRepository : IAppUserRepository
    {
        int id = 5;

        public List<AppUser> Users = new List<AppUser>
        {
            new AppUser() {Id=1,Name="AppUser1", Email="Make1", Password="Medel1", Mobile=1000, RegistrationDate = System.DateTime.Now},
            new AppUser() {Id=2,Name="AppUser2", Email="Make2", Password="Medel2", Mobile=2000, RegistrationDate = System.DateTime.Now},
            new AppUser() {Id=3,Name="AppUser3", Email="Make3", Password="Medel3", Mobile=3000, RegistrationDate = System.DateTime.Now},
            new AppUser() {Id=4,Name="AppUser4", Email="Make4", Password="Medel4", Mobile=4000, RegistrationDate = System.DateTime.Now},
        };
        public async Task<AppUser> CreateUser(AppUser user)
        {

            user.Id = id;
            id++;
            Users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<AppUser> DeleteUserbyId(int id)
        {
            AppUser user = Users.Find(x => x.Id == id);
            Users.Remove(user);
            return await Task.FromResult(user);
        }

        public async Task<AppUser> GetUserById(int id)
        {

            AppUser user = Users.Find(x => x.Id == id);
            return await Task.FromResult(user);
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            IEnumerable<AppUser> users = Users;
            return await Task.FromResult(users);
        }

        public async Task<AppUser> UpdateUser(int id, AppUser user)
        {
            AppUser Original = Users.Find(x => x.Id == id);
            Original.Name = user.Name;
            Original.Email = user.Email;
            Original.Password = user.Password;
            Original.RegistrationDate = user.RegistrationDate;
            return await Task.FromResult(user);
        }
    }
}
