using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.User
{
    [Table("UserProfile")]
    public class UserInformation
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}