namespace ClubAtlasAPI.Models
{
    public class Club
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Stub { get; set; }
        public required string Presentation { get; set; }
        public DateTime StartDate { get; set; }
        public required string Adress { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public string? Contact { get; set; }
        public string? InstagramUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? BlueskyUrl { get; set; }


    }
}
