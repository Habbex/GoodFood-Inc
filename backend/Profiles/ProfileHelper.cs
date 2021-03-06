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
            CreateMap<Recipe, RecipeReadDto>().ForMember(dest => dest.RecipeIngredients, opt => opt.MapFrom(src => src.RecipeIngredients)).ReverseMap();
            CreateMap<RecipeIngredients, RecipeIngredientsReadDto>().ReverseMap();
            CreateMap<RecipeUpdateDto,Recipe>();

            CreateMap<Ingredient, IngredientReadDto>();
            CreateMap<IngredientCreateDto, Ingredient>();
            CreateMap<IngredientUpdateDto, Ingredient>();
            CreateMap<Ingredient, IngredientUpdateDto>();

        }
    }
}