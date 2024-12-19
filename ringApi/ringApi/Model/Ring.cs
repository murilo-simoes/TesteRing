using ringApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace ringApi.Model
{
    public class Ring
    {

        public Ring(string name, string power, string ownerName, Races ownerRace, string forgedBy, string imageUrl) 
        {
            Name= name;
            Power= power;
            OwnerName= ownerName;
            OwnerRace = ownerRace;
            ForgedBy= forgedBy;
            ImageUrl= imageUrl;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Power { get; set; }
        public string OwnerName { get; set; }
        public Races OwnerRace { get; set; }
        public string ForgedBy  { get; set; }
        public string ImageUrl { get; set; }
    }
}
