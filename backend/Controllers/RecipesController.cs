using AutoMapper;
using backend.Dtos.Recipe;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {

         private RecipesRepoMock  _repository = new RecipesRepoMock();
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;

        public RecipesController(IBaseRepo baseRepo, IMapper mapper)
        {
            _baseRepo= baseRepo;
            _mapper = mapper;
        }


        // [HttpGet]
        // public ActionResult <IEnumerable<Recipe>> GetAllRecipes()
        // {
        //     var recipeItems= _repository.GetAllRecipes();


        //     return Ok(recipeItems);
        // }

        [HttpGet]
        public ActionResult <IEnumerable<RecipeReadDto>> GetAllRecipes()
        {
            var recipeItems= _baseRepo.recipe.GetAllRecipes();


            return Ok(_mapper.Map<IEnumerable<RecipeReadDto>>(recipeItems));
        }

        [HttpGet("{id:int}", Name="GetRecipeById")]
        public ActionResult <RecipeReadDto> GetRecipeById(int id)
        {
            var recipeItem= _baseRepo.recipe.GetRecipeById(id);

            if(recipeItem !=null)
            {
                return Ok(_mapper.Map<RecipeReadDto>(recipeItem));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<RecipeReadDto> CreateRecipe(RecipeCreateDto recipeCreateDto)
        {
          var recipeModel= _mapper.Map<Recipe>(recipeCreateDto);
        //    Recipe recipe= new Recipe
        //    {
        //        Title= recipeCreateDto.Title,
        //        Slug= recipeCreateDto.Slug,
        //        Category= recipeCreateDto.Category,
        //        Description= recipeCreateDto.Description
        //    };

        //     foreach (var ingredient in recipeCreateDto.RecipeIngredients)
        //     {
        //         recipe.RecipeIngredients.Add( new RecipeIngredients(){
        //             Ingredient= ingredient.Ingredient
        //         });
        //     }

          _baseRepo.recipe.CreateRecipe(recipeModel);

          _baseRepo.SaveChanges();

          var recipeReadDto= _mapper.Map<RecipeReadDto>(recipeModel);

          return CreatedAtRoute(nameof(GetRecipeById), new {Id = recipeReadDto.RecipeId},recipeReadDto);
        } 
    }
}