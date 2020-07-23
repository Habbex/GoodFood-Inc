
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.Dtos.Ingredient
{
    public class IngredientUpdateDto
    {
        [Required]
        [MaxLength(90)]
        public string Title {get; set;}

        [MaxLength(4000)]
        public string Description { get; set; }


        [MaxLength(50)]
        public string Slug {get; set;}       
        
    }
}