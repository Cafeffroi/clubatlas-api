namespace ClubAtlasAPI.Models
{
    public class ActivityByClub
    {
        public int Id { get; set; }
        public required int ClubId { get; set; }
        public required int ActivityId { get; set; }
        public required bool Active { get; set; }

        // Navigation properties
        public Club? Club { get; set; }
        public Activity? Activity { get; set; }
    }
}
