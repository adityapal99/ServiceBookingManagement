using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Data;
using UserMicroservice.Models;
using UserMicroservice.Repository.IRepository;

namespace UserMicroservice.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _db;
        public AppUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /* public bool CreateUser(AppUser user)
         {
             _db.Users.Add(user);
             return Save();
         }

         public bool DeleteUser(AppUser user)
         {
             _db.Users.Remove(user);
             return Save();
         }

         public AppUser GetUser(int userId)
         {
             return _db.Users.FirstOrDefault(u => u.Id == userId);
         }

         public ICollection<AppUser> GetUsers()
         {
             return _db.Users.OrderBy(u => u.Id).ToList();
         }

         public bool Save()
         {
             return _db.SaveChanges() >= 0 ? true : false;
         }

         public bool UpdateUser(AppUser user)
         {
             _db.Users.Update(user);
             return Save();
         }

         public bool UserExists(string email )
         {

             bool val = _db.Users.Any(u => u.Email == email);
             return val;
         }

         public bool UserExistsId(int id)
         {

             bool val = _db.Users.Any(u => u.Id == id);
             return val;
         }
     }
 }

         */

        public async Task<AppUser> CreateUser(AppUser user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;

        }
        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }
        public async Task<AppUser> GetUserById(int id)
        {
            return await _db.Users.FindAsync(id);
        }
        public async Task<AppUser> DeleteUserbyId(int id)
        {
            AppUser AppUser = await _db.Users.FindAsync(id);
            if (AppUser == null)
            {
                return null;
            }
            _db.Users.Remove(AppUser);
            await _db.SaveChangesAsync();
            return AppUser;
        }
        public async Task<AppUser> UpdateUser(int id, AppUser AppUser)
        {
            _db.Entry(AppUser).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return AppUser;

        }

    }
}