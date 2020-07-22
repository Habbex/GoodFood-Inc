using AutoMapper;
using backend.Dtos.Recipe;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;

        public RecipesController(IBaseRepo baseRepo, IMapper mapper)
        {
            _baseRepo= baseRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<RecipeReadDto>> GetAllRecipes()
        {
            var recipeItems= _baseRepo.recipe.GetAllRecipes();

            return Ok(_mapper.Map<IEnumerable<RecipeReadDto>>(recipeItems));
        }

        [HttpGet("{id:Guid}", Name="GetRecipeById")]
        public ActionResult <RecipeReadDto> GetRecipeById(Guid id)
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
            var newRecipeId= Guid.NewGuid();
           Recipe recipe= new Recipe
           {
               RecipeId= newRecipeId,
               Title= recipeCreateDto.Title,
               Slug= recipeCreateDto.Slug,
               Category= recipeCreateDto.Category,
               Description= recipeCreateDto.Description,
               RecipeIngredients = recipeCreateDto.Ingredients.Select(x=> new RecipeIngredients{RecipeId= newRecipeId, Ingredient = x.Ingredient, Amount= x.Amount}).ToList()
           };   

          _baseRepo.recipe.CreateRecipe(recipe);

          _baseRepo.SaveChanges();

          var recipeReadDto=  _mapper.Map<RecipeReadDto>(recipe);

          return CreatedAtRoute(nameof(GetRecipeById), new {Id = recipeReadDto.RecipeId.ToString()},recipeReadDto);
        } 
    }
}