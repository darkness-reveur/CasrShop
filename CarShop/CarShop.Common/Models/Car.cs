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

        //[Required]
        //public string[] PictureLinks { get; set; }

        [Required]
        public WheelDrives WheelDrive { get; set; }

        [Required]
        public EngineTypes EngineType { get; set; }

        [Required]
        public int CarModelId { get; set; }

        [ForeignKey("CarModelId")]
        public virtual CarModel CarModel { get; set; }

        public int? CartId { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        public int UserId { get; set; } 

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public enum EngineTypes
        {
            PetrolEngine,
            DieselEngine,
            ElectroEngine
        }

        public enum WheelDrives
        {
            FrontWheelDrive,
            RealWheelDrive,
            AllWheelDrive,
            PlugInAllWheelDrive
        }
    }
}
