using System.Collections.Generic;
using backend.Models;

namespace backend.Interfaces
{
    public interface IIngredientRepo
    {
        Ingredient GetIngredientById(int id);
        IEnumerable<Ingredient> GettAllIngredients();

        void CreateIngredient (Ingredient ingredient);

        void UpdateIngredient (Ingredient ingredient);

        void DeleteIngredient (Ingredient ingredient);

    }
}