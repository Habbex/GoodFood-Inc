using System;
using System.Collections.Generic;
using System.Linq;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public static class ApiObjectExtensions {

    public static Recipe ToDbRecipe(this Recipe recipe){
      var dBRecipe = new Recipe(){
        RecipeId = recipe.RecipeId,
        Title = recipe.Title,
        Slug= recipe.Slug,
        Category = recipe.Category,
        Description= recipe.Description,
        RecipeIngredients = new List<RecipeIngredients>()
      };
      return dBRecipe;
    }

    public static Ingredient ToDbIngredient(this Ingredient ingredient){
      return new Ingredient(){ 
        IngredientId = ingredient.IngredientId,
        Title= ingredient.Title,
        Description= ingredient.Description,
        Slug= ingredient.Slug,
        RecipeIngredients = new List<RecipeIngredients>()
      };    
    }

    public static IQueryable<T> FindMatches<T>(this DbContext db, IEnumerable<Guid> keys)
        where T : Recipe
        => db.Set<T>().Where(x => keys.Contains(x.RecipeId));
  }
}