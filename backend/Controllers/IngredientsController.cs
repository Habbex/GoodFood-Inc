using AutoMapper;
using backend.Dtos.Ingredient;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [Authorize]
        [HttpGet]
        public ActionResult <IEnumerable<IngredientReadDto>> GettAllIngredients()
        {
            var IngredientItems= _baseRepo.ingredient.GettAllIngredients();


            return Ok(_mapper.Map<IEnumerable<IngredientReadDto>>(IngredientItems));
        }

        [Authorize]
        [HttpGet("{id:Guid}", Name="GetIngredientById")]
        public ActionResult <IngredientReadDto> GetIngredientById(Guid id)
        {
            var IngredientItem= _baseRepo.ingredient.GetIngredientById(id);

            if(IngredientItem !=null)
            {
                return Ok(_mapper.Map<IngredientReadDto>(IngredientItem));
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public ActionResult<IngredientReadDto> CreateIngredient(IngredientCreateDto ingredientCreateDto)
        {
          var ingredientModel= _mapper.Map<Ingredient>(ingredientCreateDto);
          _baseRepo.ingredient.CreateIngredient(ingredientModel);

          _baseRepo.SaveChanges();

          var ingredientReadDto= _mapper.Map<IngredientReadDto>(ingredientModel);

          return CreatedAtRoute(nameof(GetIngredientById), new {Id = ingredientReadDto.IngredientId},ingredientReadDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateIngredient(Guid id, IngredientUpdateDto ingredientUpdateDto)
        {
            var IngredientItem= _baseRepo.ingredient.GetIngredientById(id);

            if(IngredientItem ==null)
            {
                return NotFound();
            }

            _mapper.Map(ingredientUpdateDto, IngredientItem);
            _baseRepo.ingredient.UpdateIngredient(IngredientItem);
            _baseRepo.SaveChanges();

            return NoContent();
        } 
        
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteIngredient(Guid id)
        {
            var userLoginRole = HttpContext.GetUserLoginRole();
            if (userLoginRole != "Admin")
            {
                return Unauthorized();
            }   
            
            var IngredientItem= _baseRepo.ingredient.GetIngredientById(id);

            if(IngredientItem ==null)
            {
                return NotFound();
            }

            _baseRepo.ingredient.DeleteIngredient(IngredientItem);

            _baseRepo.SaveChanges();

            return NoContent();
        }
    }
}