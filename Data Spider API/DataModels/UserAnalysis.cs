using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Data_Spider_API.Models;

namespace Data_Spider_API.DataModels
{
    public class UserAnalysis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserAnalysisID { get; set; }
        public double Price { get; set; }
        public double CustomPipRange { get; set; }
        public DateTime DateAdded { get; set; }

        [Required]
        [ForeignKey("AnalysisTool")]
        public int ToolID { get; set; }
        public AnalysisTool? AnalysisTool { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public AppUser? User { get; set; }

        [Required]
        [ForeignKey("TradingInstrument")]
        public int InstrumentID { get; set; }
        public TradingInstrument? TradingInstrument { get; set; }
    }
}
