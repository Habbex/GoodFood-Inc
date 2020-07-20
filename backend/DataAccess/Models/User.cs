
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.DataAccess.Models;

namespace backend.DataAccess.Models{
    public class User
    {
        [Key]
        public int UserId {get; set;}
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

        public List<Recipe> Recipes {get; set;} = new List<Recipe>();

    }
}