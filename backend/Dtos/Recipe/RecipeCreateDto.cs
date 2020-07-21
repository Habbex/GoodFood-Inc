using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.Dtos.Recipe
{
    public class RecipeCreateDto
    {
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

         public List<RecipeIngredients> Ingredients {get;set; }
    }
}