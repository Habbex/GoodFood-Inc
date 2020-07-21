using System.Collections.Generic;
using System.Linq;
using backend.DataAccess.Context;
using backend.Interfaces;
using backend.Models;

namespace backend.Repositories
{
    public class IngredientRepo : IIngredientRepo
    {    
        private readonly GoodFoodContext _context;

        public IngredientRepo(GoodFoodContext context)
        {
            _context= context;
        } 
        public void CreateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
           _context.Ingredients.Remove(ingredient);
        }

        public Ingredient GetIngredientById(int id)
        {
            return _context.Ingredients.FirstOrDefault(x=>x.IngredientId== id);
        }

        public IEnumerable<Ingredient> GettAllIngredients()
        {
            return _context.Ingredients.ToList();
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
        }
    }
}