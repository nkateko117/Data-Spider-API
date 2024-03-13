using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Spider_API.DataModels
{
    public class MarketType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarketTypeID { get; set; }
        public string MarketTypeName { get; set; } = string.Empty;
    }
}
