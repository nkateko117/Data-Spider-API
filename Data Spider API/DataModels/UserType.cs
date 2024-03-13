using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data_Spider_API.DataModels
{
    public class UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; } = string.Empty;
    }
}