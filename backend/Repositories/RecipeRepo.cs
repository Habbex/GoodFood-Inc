using System;
using System.Collections.Generic;
using System.Linq;
using backend.DataAccess.Context;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class RecipeRepo : IRecipeRepo
    {
        private readonly GoodFoodContext _context;

        public RecipeRepo(GoodFoodContext context)
        {
            _context = context;
        }
        public void CreateRecipe(Recipe recipe)
        {
            var dbRecipe = recipe.ToDbRecipe();
            if (recipe.RecipeIngredients != null && recipe.RecipeIngredients.Any())
            {
                foreach (var recipeIngredient in recipe.RecipeIngredients)
                {
                    if (recipeIngredient.Ingredient != null)
                    {                
                        var existingIngredient = _context.Ingredients.SingleOrDefault(x => x.Title == recipeIngredient.Ingredient.Title);

                        if (existingIngredient != null)
                        {
                            existingIngredient.ToDbIngredient();
                            var dbRecipeIngredient = new RecipeIngredients()
                            {
                                Recipe = dbRecipe,
                                IngredientId = existingIngredient.IngredientId,
                                Amount= recipeIngredient.Amount
                            };
                            dbRecipe.RecipeIngredients.Add(dbRecipeIngredient);
                        }

                        else
                        {
                            var dbIngredient = recipeIngredient.Ingredient.ToDbIngredient();
                            var dbRecipeIngredient = new RecipeIngredients()
                            {
                                Recipe = dbRecipe,
                                Ingredient = dbIngredient,
                                Amount= recipeIngredient.Amount
                            };
                            dbRecipe.RecipeIngredients.Add(dbRecipeIngredient);
                        }
                    }
                }
            }
            _context.Add(dbRecipe);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            var queryable = _context.Recipes.AsQueryable();
            queryable.Include(x => x.RecipeIngredients).ThenInclude(x => x.Ingredient).ToList();
            return queryable;
        }

        public Recipe GetRecipeById(Guid id)
        {
            return _context.Recipes.FirstOrDefault(x => x.RecipeId == id);
        }

        public IEnumerable<Recipe> GetRecipesByCategory(string category)
        {
            return _context.Recipes.Where(x => x.Category == category).ToList();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }

        // private void AddNewIngredients(Recipe recipe)
        // {
        //     foreach (var ingredient in recipe.Ingredients)
        //     {

        //         var existingIngredient = _context.Ingredients.SingleOrDefault(x=> x.Title == ingredient.Ingredient.Title);
        //         if (existingIngredient !=null)
        //             continue;


        //     }

        // }
    }
}