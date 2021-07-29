using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShop.Common.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public OrderStatuses OrderStatus { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual List<Car> Cars { get; set; }

        public enum OrderStatuses
        {
            InProgress,
            Paid
        }
    }
}
