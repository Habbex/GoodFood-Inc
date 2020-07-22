using AutoMapper;
using backend.Models;
using backend.Dtos.Recipe;
using System.Linq;
using backend.Dtos.Ingredient;

namespace backend.Profiles
{
    public class ProfileHelper : Profile
    {
        public ProfileHelper()
        {
            // CreateMap<Recipe, RecipeReadDto>().ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
            // .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
            // .AfterMap((src, dest) =>{
            //     foreach(var b in dest.Ingredients)
            //     {
            //         b.RecipeId = src.RecipeId;
            //     }
            // }).ReverseMap();
            //  CreateMap<Ingredient, RecipeIngredients>()
            // .ForMember(dest=>dest.IngredientId,opt=>opt.MapFrom(src=>src.IngredientId))
            // .ForMember(dest=>dest.Ingredient,opt=>opt.MapFrom(src=>src.Recipes));

            CreateMap<Recipe, RecipeReadDto>().ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients));
     
            CreateMap<Ingredient, IngredientReadDto>();

        }
    }
}