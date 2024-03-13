using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data_Spider_API.DataModels
{
    public class TradingInstrument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; } = string.Empty;
        public string? BaseCurrency { get; set; }
        public string? QuoteCurrency { get; set; }

        [Required]
        [ForeignKey("MarketType")]
        public int MarketTypeID { get; set; }
        public MarketType? MarketType { get; set; }
    }
}
