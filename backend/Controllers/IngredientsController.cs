using AutoMapper;
using backend.Dtos.Ingredient;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;

        public IngredientsController(IBaseRepo baseRepo, IMapper mapper)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Ingredients.
        /// </summary>
        /// <response code="200">Returns all the Ingredients</response> 
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<IngredientReadDto>> GettAllIngredients()
        {
            var IngredientItems = _baseRepo.ingredient.GettAllIngredients();


            return Ok(_mapper.Map<IEnumerable<IngredientReadDto>>(IngredientItems));
        }

         /// <summary>
        /// Get a specific Ingredient by id(Guid).
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns an Ingredient</response>
        /// <response code="404">If the Ingredient is not found</response>   
        [Authorize]
        [HttpGet("{id:Guid}", Name = "GetIngredientById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IngredientReadDto> GetIngredientById(Guid id)
        {
            var IngredientItem = _baseRepo.ingredient.GetIngredientById(id);

            if (IngredientItem != null)
            {
                return Ok(_mapper.Map<IngredientReadDto>(IngredientItem));
            }

            return NotFound();
        }

        /// <summary>
        /// Creates a Ingredient.
        /// </summary>
        /// <param name="ingredientCreateDto"></param>  
        /// <response code="201">Returns the ingredient when created successfully</response>
        /// <response code="400">If the item is null</response> 
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IngredientReadDto> CreateIngredient(IngredientCreateDto ingredientCreateDto)
        {
            var ingredientModel = _mapper.Map<Ingredient>(ingredientCreateDto);
            _baseRepo.ingredient.CreateIngredient(ingredientModel);

            _baseRepo.SaveChanges();

            var ingredientReadDto = _mapper.Map<IngredientReadDto>(ingredientModel);

            return CreatedAtRoute(nameof(GetIngredientById), new { Id = ingredientReadDto.IngredientId }, ingredientReadDto);
        }

        /// <summary>
        /// Updates a specific Ingredient, id is a Guid.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ingredientUpdateDto"></param>  
        /// <response code="204">Returns no Content when updated successfully</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="404">If the Ingredient is not found</response>      
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateIngredient(Guid id, IngredientUpdateDto ingredientUpdateDto)
        {
            var IngredientItem = _baseRepo.ingredient.GetIngredientById(id);

            if (IngredientItem == null)
            {
                return NotFound();
            }

            _mapper.Map(ingredientUpdateDto, IngredientItem);
            _baseRepo.ingredient.UpdateIngredient(IngredientItem);
            _baseRepo.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Ingredient, id is a Guid.
        /// </summary>
        /// <param name="id"></param>  
        /// <response code="204">Returns no Content when deleted successfully</response>
        /// <response code="401">Returns unauthorized when the user doesn't have the role Admin</response>
        /// <response code="404">If the Ingredient is not found</response>   
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteIngredient(Guid id)
        {
            var userLoginRole = HttpContext.GetUserLoginRole();
            if (userLoginRole != "Admin")
            {
                return Unauthorized();
            }

            var IngredientItem = _baseRepo.ingredient.GetIngredientById(id);

            if (IngredientItem == null)
            {
                return NotFound();
            }

            _baseRepo.ingredient.DeleteIngredient(IngredientItem);

            _baseRepo.SaveChanges();

            return NoContent();
        }
    }
}