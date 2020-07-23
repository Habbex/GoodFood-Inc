using System;
using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class RecipeIngredients
    {
        public Guid RecipeId{get; set;}
        public Guid IngredientId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        [MaxLength(20)]
        public string Amount { get; set; }

    }
}