using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MSFD_SkillSnap.Api.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        [ForeignKey("PortfolioUser")]
        public int PortfolioUserId { get; set; }

        [JsonIgnore]
        public PortfolioUser? PortfolioUser { get; set; }

        public List<Skill> Skills { get; set; } = new();
    }
}
