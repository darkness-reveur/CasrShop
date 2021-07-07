using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Common.Models
{
    public class CarBrand
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<CarModel> CarModels { get; set; }
    }
}
