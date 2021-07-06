using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Common.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int MobilePhoneNumber { get; set; }
        


        public enum Roles
        {
            Admin,
            AdminAssistant,
            AuthorizedUser,
            NotAuthorizedUser
        }
    }
}
