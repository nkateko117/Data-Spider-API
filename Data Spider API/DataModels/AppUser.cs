using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Data_Spider_API.DataModels;

namespace Data_Spider_API.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey("UserType")]
        public int UserTypeID { get; set; }
        public UserType? TimeZone { get; set; }

        [Required]
        [ForeignKey("UserTimeZone")]
        public int TimeZoneID { get; set; }
        public UserTimeZone? UserTimeZone { get; set; } //TimeZone refers to both DataModels.TimeZone and System.TimeZone
    }
}
