using System.Collections.Generic;
using backend.DataAccess.Models;

namespace backend.DataAccess.Repositories
{
    public class IngredientsRepoMock : IIngredientsRepo
    {
        public void CreateIngredient(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public Ingredient GetIngredientById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Ingredient> GettAllIngredients()
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }
    }
}