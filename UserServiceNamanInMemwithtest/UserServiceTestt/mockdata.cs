using System;
using System.Collections.Generic;
using System.Text;
using UserMicroservice.Models;

namespace UserServiceTestt
{
    class mockdata
    {
        public static List<AppUser> GetAppUsers()
        {
            return new List<AppUser>{
             new AppUser{
                 Id = 1,
                 Name = "Need To Go Shopping",
                 Email = "naman",
                 Mobile=909009,
                 RegistrationDate=DateTime.Now
             },
             new AppUser{
                 Id = 2,
                 Name = "adii",
                 Email = "adi@",
                 Mobile=909009,
                 RegistrationDate=DateTime.Now
             },
             new AppUser{
              Id = 3,
                 Name = "dfd",
                 Email = "adi@sd",
                 Mobile=909009,
                 RegistrationDate=DateTime.Now
             }
         };
        }
    }
}