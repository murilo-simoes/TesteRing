using ringApi.Enums;
using ringApi.Model;

namespace ringApi.Dto
{
    public class RingResponse
    {
        public RingResponse(Ring ring)
        {
            Name = ring.Name;
            Power = ring.Power;
            OwnerName = ring.OwnerName;
            OwnerRace = ring.OwnerRace;
            ForgedBy = ring.ForgedBy;
            ImageUrl = ring.ImageUrl;
        }

        public string Name { get; }
        public string Power { get; }
        public string OwnerName { get; }
        public Races OwnerRace { get; }
        public string ForgedBy { get; }
        public string ImageUrl { get; }
    }
}
