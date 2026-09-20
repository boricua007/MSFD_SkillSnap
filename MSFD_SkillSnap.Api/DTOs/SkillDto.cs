namespace MSFD_SkillSnap.Api.DTOs
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public int PortfolioUserId { get; set; }
    }
}
