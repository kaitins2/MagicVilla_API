
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string description { get; set; }
        [Required]
        public int rate { get; set; }
        public string imageUrl { get; set; }
        public string amenities { get; set; }
    }
}
