using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role {get ; set;}

        public UserInformation userInformation {get; set;}

        [JsonIgnore]
        public string Password { get; set; }
    }
}