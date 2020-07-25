using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.Dtos.Ingredient;
using backend.Models;

namespace backend.Dtos.Recipe
{
    public class  RecipeIngredientsReadDto
    {
        public Guid IngredientId { get; set; }
        public  IngredientReadDto Ingredient { get; set; }

        [MaxLength(20)]
        public string Amount { get; set; }

    }
}