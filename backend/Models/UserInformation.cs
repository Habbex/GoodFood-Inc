
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using backend.Models;

namespace backend.Models{
    public class UserInformation
    {
        public int UserInformationId {get; set;}

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(200)]
        public string WebsiteURL{get;set;}

        public virtual List<Recipe> Recipes {get; set;} = new List<Recipe>();

        public int UserLoginForeignKey {get; set;}
        public UserLogin UserLogin {get; set;}

    }
}