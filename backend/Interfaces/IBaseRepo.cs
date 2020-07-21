namespace backend.Interfaces
{
    public interface IBaseRepo
    {
        IIngredientRepo ingredient {get;}
        IRecipeRepo recipe {get;}
        void SaveChanges ();
    }
}