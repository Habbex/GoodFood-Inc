using System.Collections.Generic;
using backend.DataAccess.Models;

namespace backend.DataAccess.Repositories
{
    public interface IRecipesRepo :IBaseRepo
    {
        Recipe GetRecipeById(int id);
        IEnumerable<Recipe> GetAllRecipes();
        IEnumerable<Recipe> GetRecipesByCategory(string category);

        void CreateRecipe(Recipe recipe);

        void UpdateRecipe(Recipe recipe);

        void DeleteRecipe(Recipe recipe);
    }
}