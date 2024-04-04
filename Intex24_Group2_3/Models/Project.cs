using System.ComponentModel.DataAnnotations;

namespace Intex24_Group2_3.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string  ProgramName { get; set; }
        public string? ProjectType { get; set; }
        public int ProjectImpact { get; set; }
        public DateTime ProjectInstallation { get; set; }
        public string ProjectPhase { get; set; }
    }
}
