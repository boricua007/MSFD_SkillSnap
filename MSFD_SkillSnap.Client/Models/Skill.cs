namespace MSFD_SkillSnap.Client.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;

        // Foreign key reference to PortfolioUser
        public int PortfolioUserId { get; set; }
    }
}
