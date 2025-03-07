﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string description { get; set; }
        public int rate { get; set; }
        public string imageUrl { get; set; }
        public string amenities { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate{ get; set; }
    }
}
