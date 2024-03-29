﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Spider_API.DataModels
{
    public class Broker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrokerID { get; set; }
        [Required]
        public string BrokerName { get; set;} = string.Empty;
    }
}
