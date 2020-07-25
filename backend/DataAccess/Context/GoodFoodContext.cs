using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.DataAccess.Context
{
    public class GoodFoodContext : DbContext
    {
        public GoodFoodContext(DbContextOptions<GoodFoodContext> option) : base(option)
        {
            
        }

        public GoodFoodContext() { }   
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
             //Composite key with FluentAPI for RecipeIngredients
            builder.Entity<RecipeIngredients>().HasKey(rI=> new { rI.RecipeId, rI.IngredientId});

            builder.Entity<RecipeIngredients>().HasOne(rI => rI.Recipe).WithMany(rI => rI.RecipeIngredients).HasForeignKey(rI => rI.RecipeId);

            builder.Entity<RecipeIngredients>().HasOne(rI => rI.Ingredient).WithMany(rI => rI.RecipeIngredients).HasForeignKey(rI => rI.IngredientId);

            builder.Entity<UserLogin>().HasOne(uL => uL.userInformation).WithOne(ui =>ui.UserLogin).HasForeignKey<UserInformation>( ui => ui.UserLoginForeignKey);

        }

        public DbSet<UserLogin> UserLogins {get; set;}
        public DbSet<UserInformation> Users{ get; set;}

        public DbSet<Recipe> Recipes {get; set;}

        public DbSet<Ingredient> Ingredients {get; set;}

        public DbSet<RecipeIngredients> RecipeIngredients {get; set;}
    }
}