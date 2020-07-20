using System.Collections.Generic;
using backend.DataAccess.Models;

namespace backend.DataAccess.Repositories
{
    public class RecipesRepoMock : IRecipesRepo
    {
        public void CreateRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient{IngredientId= 0, Title="Pasta", Slug="/Pasta"},
                new Ingredient{IngredientId= 1, Title="Tomato Sacuse", Slug="/Tomato-Sacuse"},
                new Ingredient{IngredientId= 2, Title="Minced Minced", Slug="/Minced-Minced"}
            };

            var recipes = new List<Recipe>
            {
                new Recipe{RecipeId= 0, Title="Spaghetti", Slug="/Spaghetti", Category="Main course", Description="Spaghetti is easy to do, and tastes delicious!",RecipeIngredients=(ICollection<RecipeIngredients>)ingredients},
                new Recipe{RecipeId= 1, Title="Falafel", Slug="/Falafel", Category="Main course", Description="Falafel is easy to do, and tastes delicious!",RecipeIngredients=(ICollection<RecipeIngredients>)ingredients}
            };
            return recipes;
        }

        public Recipe GetRecipeById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Recipe> GetRecipesByCategory(string category)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
    }
}