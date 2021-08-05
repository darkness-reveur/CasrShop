using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Common.Models
{
    public class CartCar
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; }


        public int CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

    }
}
