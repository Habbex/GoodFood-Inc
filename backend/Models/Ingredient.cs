
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        [MaxLength(90)]
        public string Title {get; set;}

        [MaxLength(4000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Slug {get; set;}
        
        public ICollection<RecipeIngredients> RecipeIngredients {get;set; }

    }
}