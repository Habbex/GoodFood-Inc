using System.ComponentModel.DataAnnotations;
using backend.DataAccess.Models;
namespace backend.DataAccess.Models
{
    public class RecipeIngredients
    {
        public int RecipeId{get; set;}
        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [MaxLength(20)]
        public string Amount { get; set; }
    }
}