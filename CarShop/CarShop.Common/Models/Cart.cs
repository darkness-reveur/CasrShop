using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Common.Models
{
    public class Cart
    {
        public int Id { get; set; }
        
        public virtual List<Car> Cars { get; set; }


        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
