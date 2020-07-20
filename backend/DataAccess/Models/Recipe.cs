using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.DataAccess.Models;

// nvarchar(4000) is used instead of nvarchar(MAX) for description.
// Reason for this is that it will allocate too much in memory and will become a performance issue when the solution grows. 
// You can read more about it here : https://www.sqlservercentral.com/forums/topic/nvarchar4000-and-performance
namespace backend.DataAccess.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required]
        [MaxLength(90)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Slug { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }

        public ICollection<RecipeIngredients> RecipeIngredients {get; set;}
    }
}