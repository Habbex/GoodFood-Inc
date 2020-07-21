using AutoMapper;
using backend.Dtos.Ingredient;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;

        public IngredientsController(IBaseRepo baseRepo, IMapper mapper)
        {
            _baseRepo= baseRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<IngredientReadDto>> GetAllRecipes()
        {
            var recipeItems= _baseRepo.recipe.GetAllRecipes();


            return Ok(_mapper.Map<IEnumerable<IngredientReadDto>>(recipeItems));
        }

        [HttpGet("{id:int}", Name="GetRecipeById")]
        public ActionResult <IngredientReadDto> GetRecipeById(int id)
        {
            var recipeItem= _baseRepo.recipe.GetRecipeById(id);

            if(recipeItem !=null)
            {
                return Ok(_mapper.Map<IngredientReadDto>(recipeItem));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<IngredientReadDto> CreateRecipe(Recipe recipeCreateDto)
        {
          var recipeModel= _mapper.Map<Recipe>(recipeCreateDto);
          _baseRepo.recipe.CreateRecipe(recipeModel);

          _baseRepo.SaveChanges();

          var recipeReadDto= _mapper.Map<IngredientReadDto>(recipeModel);

          return CreatedAtRoute(nameof(GetRecipeById), new {Id = recipeReadDto.RecipeId},recipeReadDto);
        } 
    }
}