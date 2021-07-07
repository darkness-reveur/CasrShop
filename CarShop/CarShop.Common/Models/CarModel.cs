using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Common.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CarBrandId { get; set; }
        
        [ForeignKey("CarBrandId")]
        public virtual CarBrand CarBrand { get; set; }

        public virtual List<Car> Cars { get; set; }
    }
}
