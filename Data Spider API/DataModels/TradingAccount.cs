using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Spider_API.DataModels
{
    public class TradingAccount
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TradingAccountID { get; set; }
        [Required]
        public double Leverage {  get; set; }
        public double Balance { get; set; }

        [Required]
        [ForeignKey("AppUser")]
        public int UserID { get; set; }
        public UserType? User { get; set; }

        [Required]
        [ForeignKey("Broker")]
        public int BrokerID { get; set; }
        public Broker? Broker { get; set; }
    }
}
