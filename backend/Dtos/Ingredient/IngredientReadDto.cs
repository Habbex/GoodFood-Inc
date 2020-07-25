using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Ingredient
{
    public class IngredientReadDto
    {
        public Guid IngredientId { get; set; }
        public string Title {get; set;}
        public string Description { get; set; }
        public string Slug {get; set;}
        
    }
}