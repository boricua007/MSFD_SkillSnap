using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MSFD_SkillSnap.Api.Models
{
    public class PortfolioUser
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = string.Empty;

        // Navigation properties
        [JsonIgnore]
        public List<Project> Projects { get; set; } = new();

        [JsonIgnore]
        public List<Skill> Skills { get; set; } = new();
    }
}