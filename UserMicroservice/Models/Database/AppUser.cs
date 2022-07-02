using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class AppUser
    {
        public AppUser()
        {
        }

        public AppUser(string name, string email, string password, string mobile, DateTime? registrationDate)
        {
            Name = name;
            Email = email;
            Password = password;
            Mobile = mobile;
            RegistrationDate = registrationDate ?? DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime RegistrationDate { get; set; }


    }
}
