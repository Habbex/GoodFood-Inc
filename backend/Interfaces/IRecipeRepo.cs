using System;
using System.Collections.Generic;
using backend.Models;

namespace backend.Interfaces
{
    public interface IRecipeRepo
    {
        Recipe GetRecipeById(Guid id);
        IEnumerable<Recipe> GetAllRecipes();
        IEnumerable<Recipe> GetRecipesByCategory(string category);

        void CreateRecipe(Recipe recipe);

        void UpdateRecipe(Recipe recipe);

        void DeleteRecipe(Recipe recipe);
    }
}