using System.Collections.Generic;
using backend.DataAccess.Models;

namespace backend.DataAccess.Repositories
{
    public interface IIngredientsRepo : IBaseRepo
    {
        Ingredient GetIngredientById(int id);
        IEnumerable<Ingredient> GettAllIngredients();

        void CreateIngredient (Ingredient ingredient);

        void UpdateIngredient (Ingredient ingredient);

        void DeleteIngredient (Ingredient ingredient);

    }
}