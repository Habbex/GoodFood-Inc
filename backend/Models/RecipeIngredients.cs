using System.ComponentModel.DataAnnotations;
using backend.Models;
namespace backend.Models
{
    public class RecipeIngredients
    {
        public int RecipeId{get; set;}
        public int IngredientId { get; set; }
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
        
        [MaxLength(20)]
        public string Amount { get; set; }
    }
}