using CarShop.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public UserRoles Role { get; set; }

        [Required]
        public string MobilePhoneNumber { get; set; }

        public virtual List<Order> Orders { get; set; }

        public int? CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        public int CartId { get; set; }

        [ForeignKey("CartId")]    
        public virtual Cart Cart { get; set; }
    }
}
