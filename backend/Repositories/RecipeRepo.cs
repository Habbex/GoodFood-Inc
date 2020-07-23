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

        public void CreateRecipe(Recipe recipe, string UserLoginId)
        {
            var dbRecipe = AddIngredients(recipe);
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
            return _context.Recipes.Include(x => x.RecipeIngredients).ThenInclude(x => x.Ingredient).FirstOrDefault(x => x.RecipeId == id);
        }

        public IEnumerable<Recipe> GetRecipesByCategory(string category)
        {
            return _context.Recipes.Where(x => x.Category == category).ToList();
        }

        public void UpdateRecipe(Recipe recipe, RecipeUpdateDto recipeUpdateDto, string UserLoginId)
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
                var recipeIngredient = _context.RecipeIngredients.FirstOrDefault(x => x.IngredientId == item.Ingredient.IngredientId && x.RecipeId == recipe.RecipeId);
                if (recipeIngredient != null)
                {
                    recipeIngredient.Amount = item.Amount;
                }
            }
             Models.User User = new User{UserId= Int32.Parse(UserLoginId),Recipes= new List<Recipe>{recipe}};
            _context.Users.Add(User);

            _context.SaveChanges();
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
                        var existingIngredient = _context.Ingredients.SingleOrDefault(x => x.Title == recipeIngredient.Ingredient.Title);

                        if (existingIngredient != null)
                        {
                            var dbRecipeIngredient = new RecipeIngredients()
                            {
                                RecipeId = dbRecipe.RecipeId,
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