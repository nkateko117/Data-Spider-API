using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data_Spider_API.DataModels
{
    public class UserTimeZone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTimeZoneID { get; set; }
        public string UserTimeZoneName { get; set; } = string.Empty;
    }
}
