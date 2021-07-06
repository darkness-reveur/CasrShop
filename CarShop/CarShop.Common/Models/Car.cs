using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShop.Common.Models
{
    public class Car
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public ushort ReleaseYear { get; set; }

        [Required]
        public double EngineVolume { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int VehicleMileage { get; set; }

        public string Description { get; set; }

        [Required]
        public int CarModelId { get; set; }

        [ForeignKey("CarModelId")]
        public virtual CarModel CarModel { get; set; }

        public int? OrderId { get; set; } // Положил ли кто то машину в карзину

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public int? UserId { get; set; } 

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public enum EngineType
        {
            PetrolEngine,
            DieselEngine,
            ElectroEngine6
        }

        public enum WheelDrive
        {
            FrontWheelDrive,
            RealWheelDrive,
            AllWheelDrive,
            PlugInAllWheelDrive
        }
    }
}
