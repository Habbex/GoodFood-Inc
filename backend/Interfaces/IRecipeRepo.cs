using System;
using System.Collections.Generic;
using backend.Dtos.Recipe;
using backend.Models;

namespace backend.Interfaces
{
    public interface IRecipeRepo
    {
        Recipe GetRecipeById(Guid id);
        IEnumerable<Recipe> GetAllRecipes();
        IEnumerable<Recipe> GetRecipesByCategory(string category);

        void CreateRecipe(Recipe recipe, int UserLoginId);

        void UpdateRecipe(Recipe recipe, RecipeUpdateDto recipeUpdateDto, int UserLoginId);

        void DeleteRecipe(Recipe recipe);

        void BulkDeleteRecipe(IEnumerable<Guid> RecipeIds);
    }
}