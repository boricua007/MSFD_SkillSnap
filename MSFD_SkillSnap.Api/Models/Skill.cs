using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MSFD_SkillSnap.Api.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Level { get; set; }

        // Foreign key to PortfolioUser
        [ForeignKey("PortfolioUser")]
        public int PortfolioUserId { get; set; }

        [JsonIgnore]
        public PortfolioUser PortfolioUser { get; set; }

        [JsonIgnore]
        public List<Project> Projects { get; set; } = new();
    }
}