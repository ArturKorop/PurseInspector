using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.User
{
    /// <summary>
    /// Class that provides a description of user information in database and used for conection for Table "UserProfile"
    /// </summary>
    [Table("UserProfile")]
    public class UserInformation
    {
        /// <summary>
        /// User ID
        /// </summary>
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
    }
}