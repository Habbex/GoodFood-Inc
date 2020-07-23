using AutoMapper;
using backend.DataAccess.Context;
using backend.Dtos.Recipe;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly GoodFoodContext _context;

        public RecipesController(IBaseRepo baseRepo, IMapper mapper, GoodFoodContext context)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<RecipeReadDto>> GetAllRecipes()
        {
            var recipeItems = _baseRepo.recipe.GetAllRecipes();

            return Ok(_mapper.Map<IEnumerable<RecipeReadDto>>(recipeItems));
        }

        [Authorize]
        [HttpGet("{id:Guid}", Name = "GetRecipeById")]
        public ActionResult<RecipeReadDto> GetRecipeById(Guid id)
        {
            var recipeItem = _baseRepo.recipe.GetRecipeById(id);

            if (recipeItem != null)
            {
                return Ok(_mapper.Map<RecipeReadDto>(recipeItem));
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public ActionResult<RecipeReadDto> CreateRecipe(RecipeCreateDto recipeCreateDto)
        {
            var newRecipeId = Guid.NewGuid();
            var userLoginId = HttpContext.GetUserLoginId();
            Recipe recipe = new Recipe
            {
                RecipeId = newRecipeId,
                Title = recipeCreateDto.Title,
                Slug = recipeCreateDto.Slug,
                Category = recipeCreateDto.Category,
                Description = recipeCreateDto.Description,
                RecipeIngredients = recipeCreateDto.RecipeIngredients.Select(x => new RecipeIngredients { RecipeId = newRecipeId, IngredientId = x.IngredientId, Ingredient = x.Ingredient, Amount = x.Amount }).ToList()
            };

            _baseRepo.recipe.CreateRecipe(recipe, userLoginId);

            _baseRepo.SaveChanges();

            var recipeReadDto = _mapper.Map<RecipeReadDto>(recipe);

            return CreatedAtRoute(nameof(GetRecipeById), new { Id = recipeReadDto.RecipeId.ToString() }, recipeReadDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateRecipe(Guid id, RecipeUpdateDto recipeUpdateDto)
        {
            var userLoginId = HttpContext.GetUserLoginId();
            var recipeItem = _baseRepo.recipe.GetRecipeById(id);
            if (recipeItem == null)
            {
                return NotFound();
            }

            recipeUpdateDto.RecipeId = id;
            recipeUpdateDto.Recipe = recipeItem;
            _baseRepo.recipe.UpdateRecipe(recipeItem, recipeUpdateDto, userLoginId);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteRecipe(Guid id)
        {
            var recipeItem = _baseRepo.recipe.GetRecipeById(id);
            if (recipeItem == null)
            {
                return NotFound();
            }

            _baseRepo.recipe.DeleteRecipe(recipeItem);
            _baseRepo.SaveChanges();

            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        public ActionResult BulkDeleteRecipe(RecipeBulkDeleteDto recipeBulkDeleteDto)
        {
            _baseRepo.recipe.BulkDeleteRecipe(recipeBulkDeleteDto.RecipeIds);
            _baseRepo.SaveChanges();

            return NoContent();
        }
    }
}