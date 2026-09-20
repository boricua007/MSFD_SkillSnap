using MSFD_SkillSnap.Client.Models;

namespace MSFD_SkillSnap.Client.Services
{
    public class UserSessionService
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public Project CurrentProject { get; set; }
    }
}
