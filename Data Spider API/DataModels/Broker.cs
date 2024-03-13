using System.ComponentModel.DataAnnotations;

namespace Data_Spider_API.DataModels
{
    public class Broker
    {
        public int BrokerID { get; set; }
        [Required]
        public string BrokerName { get; set;} = string.Empty;
    }
}
