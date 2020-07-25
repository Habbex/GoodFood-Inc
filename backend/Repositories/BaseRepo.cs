using backend.DataAccess.Context;
using backend.Interfaces;

namespace backend.Repositories
{
    public class BaseRepo : IBaseRepo
    {
        private readonly GoodFoodContext _context;
        private IngredientRepo _ingredientRepo;
        private RecipeRepo _recipeRepo;

        public BaseRepo(GoodFoodContext context)
        {
            _context = context;
        }
        public IIngredientRepo ingredient
        {
            get
            {
                return _ingredientRepo = _ingredientRepo ?? new IngredientRepo(_context);
            }
        }

        public IRecipeRepo recipe
        {
            get
            {
                return _recipeRepo = _recipeRepo ?? new RecipeRepo(_context);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}