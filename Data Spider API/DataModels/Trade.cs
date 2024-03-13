using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data_Spider_API.DataModels
{
    public class Trade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TradeID { get; set; }
        public double OpeningPrice { get; set; }
        public double ClosingPrice { get; set; }
        public DateTime Time { get; set; }
        public double Profit { get; set; }
        public double Change { get; set; }
        public double LotSize { get; set; }
        public double? Spread { get; set;}

        [Required]
        [ForeignKey("TradingInstrument")]
        public int InstrumentID { get; set; }
        public TradingInstrument? TradingInstrument { get; set; }
    }
}
