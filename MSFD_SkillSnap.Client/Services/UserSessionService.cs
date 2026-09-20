using MSFD_SkillSnap.Client.Models;

namespace MSFD_SkillSnap.Client.Services
{
    public class UserSessionService
    {
        public string UserId { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public Project? CurrentProject { get; set; }
    }
}
