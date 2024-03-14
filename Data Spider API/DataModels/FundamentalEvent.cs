using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data_Spider_API.DataModels
{
    public class FundamentalEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Importance { get; set; } = string.Empty;
        public double Previous {  get; set; }
        public double Forcast { get; set; }
        public double Actual { get; set; }
        public DateTime StartTime { get; set; }
    }
}
