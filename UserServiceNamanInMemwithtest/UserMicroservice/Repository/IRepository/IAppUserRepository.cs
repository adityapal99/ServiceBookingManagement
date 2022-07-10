using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.Repository.IRepository
{
    public interface IAppUserRepository
    {
      /*  ICollection<AppUser> GetUsers();
        AppUser GetUser(int userId);

        bool UserExists(string email);
        bool UserExistsId(int id);
        bool CreateUser(AppUser user);
        bool UpdateUser(AppUser user);
        bool DeleteUser(AppUser user);

        
        bool Save();*/
        public Task<AppUser> CreateUser(AppUser user);
        public Task<AppUser> DeleteUserbyId(int id);
        public Task<AppUser> GetUserById(int id);
        public Task<IEnumerable<AppUser>> GetUsers();
        public Task<AppUser> UpdateUser(int id,AppUser user);

    }
}
