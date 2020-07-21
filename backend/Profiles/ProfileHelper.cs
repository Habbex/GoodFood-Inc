using AutoMapper;
using backend.Models;
using backend.Dtos.Recipe;

namespace backend.Profiles
{
    public class ProfileHelper :Profile
    {
        public ProfileHelper()
        {
            CreateMap<Recipe, RecipeReadDto>();

            CreateMap<RecipeCreateDto, Recipe>();
        }
    }
}