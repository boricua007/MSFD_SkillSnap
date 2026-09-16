namespace MSFD_SkillSnap.Api.Models
{
    public class ProjectCreateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int PortfolioUserId { get; set; }
    }
}