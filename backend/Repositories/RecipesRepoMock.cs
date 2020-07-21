using System.Collections.Generic;
using backend.Interfaces;
using backend.Models;

namespace backend.Repositories
{
    public class RecipesRepoMock : IRecipeRepo
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



             var recipes = new[]{
                new Recipe
                {
                    RecipeId = 0,
                    Title = "Spaghetti",
                    Slug = "/Slug",
                    Category = "Main course",
                    Description = "Spaghetti is easy to do, and tastes delicious!",
                    
                },
                new Recipe
                {
                    RecipeId = 1,
                    Title = "Lasagna",
                    Description = "I hate Mondays",
                    Slug = "/Lasagna",
                    Category = "Main course"
                }
            };

            // recipes[0].Ingredients.AddRange(
            //     new RecipeIngredients{ Ingredient= new Ingredient{IngredientId=0, Title="Pasta", Description="Never break the pasta!", Slug="/Pasta"}} ,
            //     new RecipeIngredients{ Ingredient=new Ingredient{IngredientId=1, Title="Tomato Sacuse", Description="Choose the best sacuse", Slug="/Tomato-Sacuse"}},
            //     new RecipeIngredients{ Ingredient=new Ingredient{IngredientId= 2, Title="Minced Minced", Description="80% meat and 20% fat",  Slug="/Minced-Minced"}}
            // );
            

            // recipes[1].Ingredients= new List<RecipeIngredients>
            // {
            //     new RecipeIngredients{ Ingredient= new Ingredient{IngredientId=0, Title="Pasta", Description="Never break the pasta!", Slug="/Pasta"}} ,
            //     new RecipeIngredients{ Ingredient=new Ingredient{IngredientId=1, Title="Tomato Sacuse", Description="Choose the best sacuse", Slug="/Tomato-Sacuse"}},
            //     new RecipeIngredients{ Ingredient=new Ingredient{IngredientId= 2, Title="Minced Minced", Description="80% meat and 20% fat",  Slug="/Minced-Minced"}}
            // };         

            return recipes;
        }

        public Recipe GetRecipeById(int id)
        {
            var recipes = new[]{
                new Recipe
                {
                    RecipeId = 0,
                    Title = "Spaghetti",
                    Slug = "/Slug",
                    Category = "Main course",
                    Description = "Spaghetti is easy to do, and tastes delicious!"
                },
                new Recipe
                {
                    RecipeId = 1,
                    Title = "Lasagna",
                    Description = "I hate Mondays",
                    Slug = "/Lasagna",
                    Category = "Main course"
                }
            };


            // var Ingredients = new[]
            // {
            //    new Ingredient{IngredientId=0, Title="Pasta", Description="Never break the pasta!", Slug="/Pasta"},
            //    new Ingredient{IngredientId=1, Title="Tomato Sacuse", Description="Choose the best sacuse", Slug="/Tomato-Sacuse"},
            //    new Ingredient{IngredientId= 2, Title="Minced Minced", Description="80% meat and 20% fat",  Slug="/Minced-Minced"}
            // };
            // recipes[0].RecipeIngredients.Add(new RecipeIngredients { Ingredient = Ingredients[0], Recipe = recipes[0], Amount = "200g" });
            // recipes[0].RecipeIngredients.Add(new RecipeIngredients { Ingredient = Ingredients[1], Recipe = recipes[0], Amount = "0.5L" });
            // recipes[0].RecipeIngredients.Add(new RecipeIngredients { Ingredient = Ingredients[2], Recipe = recipes[0], Amount = "200g" });

            return recipes[0];
        }

        public IEnumerable<Recipe> GetRecipesByCategory(string category)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
    }
}