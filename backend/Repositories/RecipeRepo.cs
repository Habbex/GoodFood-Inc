using System.Collections.Generic;
using System.Linq;
using backend.DataAccess.Context;
using backend.Interfaces;
using backend.Models;

namespace backend.Repositories
{
    public class RecipeRepo : IRecipeRepo
    {
        private readonly GoodFoodContext _context;

        public RecipeRepo(GoodFoodContext context)
        {
            _context= context;
        }
        public void CreateRecipe(Recipe recipe)
        {
           _context.Add(recipe);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
           return _context.Recipes.ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            return _context.Recipes.FirstOrDefault(x=> x.RecipeId== id);
        }

        public IEnumerable<Recipe> GetRecipesByCategory(string category)
        {
            return _context.Recipes.Where(x=>x.Category== category).ToList();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }
    }
}