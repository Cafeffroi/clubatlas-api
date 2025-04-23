namespace ClubAtlasAPI.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Change this line - initialize with empty collection
        public ICollection<ActivityByClub> ActivityByClubs { get; set; } = new List<ActivityByClub>();
    }
}
