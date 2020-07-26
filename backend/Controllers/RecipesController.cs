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
    [Produces("application/json")]
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

        /// <summary>
        /// Gets All Recipes.
        /// </summary>
        /// <response code="200">Returns all the Recipes</response> 
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RecipeReadDto>> GetAllRecipes()
        {
            var recipeItems = _baseRepo.recipe.GetAllRecipes();

            return Ok(_mapper.Map<IEnumerable<RecipeReadDto>>(recipeItems));
        }

        /// <summary>
        /// Get a specific Recipe by id(Guid).
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns an Recipe</response>
        /// <response code="404">If the Recipe is not found</response> 
        [Authorize]
        [HttpGet("{id:Guid}", Name = "GetRecipeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecipeReadDto> GetRecipeById(Guid id)
        {
            var recipeItem = _baseRepo.recipe.GetRecipeById(id);

            if (recipeItem != null)
            {
                return Ok(_mapper.Map<RecipeReadDto>(recipeItem));
            }

            return NotFound();
        }
        /// <summary>
        /// Creates a Recipe.
        /// </summary>
        ///<remarks>
        ///     Sample request (Note the final } at the bottom):
        ///
        ///     {
        ///     "title": "Kebab sandwich",
        ///     "slug": "/Kebab-sandwich",
        ///     "category": "Main course",
        ///     "description": "Kebab sandwich is easy to do, and tastes delicious!",
        ///     "recipeIngredients": [
        ///         {
        ///             Creates a new ingredient
        ///             "ingredient": {
        ///                 "title": "Bread",
        ///                 "description": "Bread",
        ///                 "Slug": "/Bread"
        ///
        ///             },
        ///             "amount": "1 slice"
        ///         },
        ///           {
        ///             Uses an already created ingredient    
        ///            "ingredientId": "2dc8c206-782a-4bed-7cd1-08d8316e8438",
        ///             "ingredient": {
        ///              "ingredientId": "2dc8c206-782a-4bed-7cd1-08d8316e8438",
        ///                 "title": "Kebab",
        ///                 "description": "Kebab",
        ///                 "Slug" : "/Kebab"
        ///                
        ///             },
        ///             "amount": "100g"
        ///         }
        ///     ]
        /// }
        ///</remarks>
        /// <param name="recipeCreateDto"></param>  
        /// <response code="201">Returns the Recipe when created successfully</response>
        /// <response code="400">If the item is null</response> 
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Updates a specific Recipe, id is a Guid.
        /// </summary>
        ///<remarks>
        ///     Sample request (Note the final } at the bottom):
        ///
        ///     {
        ///     "title": "Kebab sandwich",
        ///     "slug": "/Kebab-sandwich",
        ///     "category": "Main course",
        ///     "description": "Kebab sandwich is easy to do, and tastes delicious!",
        ///     "recipeIngredients": [
        ///         {
        ///             Uses an already created ingredient  
        ///             "ingredientId": "85F00200-AFF8-46EF-7CFF-08D830B74407",
        ///             "ingredient": {
        ///                 "ingredientId": "85F00200-AFF8-46EF-7CFF-08D830B74407",
        ///                 "title": "Bread",
        ///                 "description": "Bread",
        ///                 "Slug": "/Bread"
        ///             },
        ///             "amount": "1 slice"
        ///         },
        ///           {
        ///             Uses an already created ingredient    
        ///            "ingredientId": "2dc8c206-782a-4bed-7cd1-08d8316e8438",
        ///             "ingredient": {
        ///              "ingredientId": "2dc8c206-782a-4bed-7cd1-08d8316e8438",
        ///                 "title": "Kebab",
        ///                 "description": "Kebab",
        ///                 "Slug" : "/Kebab"        
        ///             },
        ///             "amount": "100g"
        ///         }
        ///     ]
        /// }
        ///</remarks>
        /// <param name="id"></param>
        /// <param name="recipeUpdateDto"></param>  
        /// <response code="204">Returns no Content when updated successfully</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="404">If the Recipe is not found</response>    
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateRecipe(Guid id, RecipeUpdateDto recipeUpdateDto)
        {
            var userLoginId = HttpContext.GetUserLoginId();
            var recipeItem = _baseRepo.recipe.GetRecipeById(id);
            if (recipeItem == null)
            {
                return NotFound();
            }

            recipeUpdateDto.RecipeId = id;
            recipeUpdateDto.Recipe = _mapper.Map<RecipeReadDto>(recipeItem);
            
            _baseRepo.recipe.UpdateRecipe(recipeItem, recipeUpdateDto, userLoginId);
              _baseRepo.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Recipe, id is a Guid.
        /// </summary>
        /// <param name="id"></param>  
        /// <response code="204">Returns no Content when deleted successfully</response>
        /// <response code="404">If the Recipe is not found</response>  
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Deletes multiple specific recipes, ids in Guid.
        /// </summary>
        /// <param name="recipeBulkDeleteDto"></param>  
        /// <response code="204">Returns no Content when all deleted successfully</response>
         /// <response code="400">If the item is null</response> 
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult BulkDeleteRecipe(RecipeBulkDeleteDto recipeBulkDeleteDto)
        {
            _baseRepo.recipe.BulkDeleteRecipe(recipeBulkDeleteDto.RecipeIds);
            _baseRepo.SaveChanges();

            return NoContent();
        }
    }
}