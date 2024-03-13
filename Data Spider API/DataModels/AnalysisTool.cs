using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Spider_API.DataModels
{
    public class AnalysisTool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ToolID { get; set; }
        public string ToolName { get; set; } = string.Empty;
        public string ToolEffect { get; set; } = string.Empty;
        public double PipRange { get; set; }
    }
}
