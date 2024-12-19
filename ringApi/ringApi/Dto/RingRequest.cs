using ringApi.Enums;
using ringApi.Model;
using System.ComponentModel.DataAnnotations;

namespace ringApi.Dto
{
    public class RingRequest
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Power { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public Races OwnerRace { get; set; }
        [Required]
        public string ForgedBy { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public Ring toModel() 
            => new Ring(Name, Power, OwnerName, OwnerRace, ForgedBy, ImageUrl);
    }
}
