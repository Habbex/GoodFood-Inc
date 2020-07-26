using System;
using System.Collections.Generic;
using System.Linq;
using backend.DataAccess.Context;
using backend.Dtos.Recipe;
using backend.Interfaces;
using backend.Models;
using backend.Helpers;
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

        public void BulkDeleteRecipe(IEnumerable<Guid> recipeIds)
        {
            var recipesToBeRemoved = _context.FindMatches<Recipe>(recipeIds).ToList();
            _context.RemoveRange(recipesToBeRemoved);
        }

        public void CreateRecipe(Recipe recipe, int UserLoginId)
        {
            var dbRecipe = AddIngredients(recipe);
            _context.Add(dbRecipe);

            var userRow = _context.Users.FirstOrDefault(x => x.UserInformationId == UserLoginId);
            userRow.Recipes.Add(dbRecipe);
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
            return _context.Recipes.Include(x => x.RecipeIngredients).ThenInclude(x => x.Ingredient).FirstOrDefault(x => x.RecipeId == id);
        }

        public IEnumerable<Recipe> GetRecipesByCategory(string category)
        {
            return _context.Recipes.Where(x => x.Category == category).ToList();
        }

        public void UpdateRecipe(Recipe recipe, RecipeUpdateDto recipeUpdateDto, int UserLoginId)
        {
            var model = recipe;

            _context.TryUpdateManyToMany(model.RecipeIngredients, recipeUpdateDto.RecipeIngredients
            .Select(x => new RecipeIngredients
            {
                Amount = x.Amount,
                IngredientId = x.Ingredient.IngredientId,
                RecipeId = recipeUpdateDto.Recipe.RecipeId,
            }), x => x.IngredientId);

            foreach (var item in recipeUpdateDto.RecipeIngredients)
            {
                // Update the Amount of the ingredient in the bridge table RecipeIngredients
                var recipeIngredient = _context.RecipeIngredients.FirstOrDefault(x => x.IngredientId == item.Ingredient.IngredientId && x.RecipeId == recipe.RecipeId);
                if (recipeIngredient != null)
                {
                    recipeIngredient.Amount = item.Amount;
                }
            }   

            recipe.Title= recipeUpdateDto.Title;
            recipe.Slug= recipeUpdateDto.Slug;
            recipe.Description= recipeUpdateDto.Description;
            recipe.Category= recipeUpdateDto.Category;

            // Attach the recipe to the user.
            var userRow = _context.Users.FirstOrDefault(x => x.UserInformationId == UserLoginId);

            userRow.Recipes.Add(recipe);
     
        }

        private Recipe AddIngredients(Recipe recipe)
        {
            var dbRecipe = recipe.ToDbRecipe();
            if (recipe.RecipeIngredients != null && recipe.RecipeIngredients.Any())
            {
                foreach (var recipeIngredient in recipe.RecipeIngredients)
                {
                    if (recipeIngredient.Ingredient != null)
                    {
                        // If the ingredients are also new, create them too and attach them to the recipe.
                        // This for greater usability on the front end.
                        var existingIngredient = _context.Ingredients.SingleOrDefault(x => x.Title == recipeIngredient.Ingredient.Title);

                        if (existingIngredient != null)
                        {
                            var dbRecipeIngredient = new RecipeIngredients()
                            {                   
                                IngredientId = existingIngredient.ToDbIngredient().IngredientId,
                                Amount = recipeIngredient.Amount
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
                                Amount = recipeIngredient.Amount
                            };

                            dbRecipe.RecipeIngredients.Add(dbRecipeIngredient);
                        }
                    }
                }
            }
            return dbRecipe;
        }
    }
}