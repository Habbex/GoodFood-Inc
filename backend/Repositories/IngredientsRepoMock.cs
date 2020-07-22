using System;
using System.Collections.Generic;
using backend.Interfaces;
using backend.Models;

namespace backend.Repositories
{
    public class IngredientsRepoMock : IIngredientRepo
    {

        public void CreateIngredient(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public Ingredient GetIngredientById(Guid id)
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