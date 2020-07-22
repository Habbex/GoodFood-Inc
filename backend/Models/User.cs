
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.Models{
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

        public virtual List<Recipe> Recipes {get; set;}

    }
}