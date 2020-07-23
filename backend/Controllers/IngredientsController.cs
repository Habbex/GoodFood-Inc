using AutoMapper;
using backend.Dtos.Ingredient;
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

        [HttpGet]
        public ActionResult <IEnumerable<IngredientReadDto>> GettAllIngredients()
        {
            var IngredientItems= _baseRepo.ingredient.GettAllIngredients();


            return Ok(_mapper.Map<IEnumerable<IngredientReadDto>>(IngredientItems));
        }

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

        [HttpPost]
        public ActionResult<IngredientReadDto> CreateIngredient(Ingredient ingredientCreateDto)
        {
          var ingredientModel= _mapper.Map<Ingredient>(ingredientCreateDto);
          _baseRepo.ingredient.CreateIngredient(ingredientModel);

          _baseRepo.SaveChanges();

          var ingredientReadDto= _mapper.Map<IngredientReadDto>(ingredientModel);

          return CreatedAtRoute(nameof(GetIngredientById), new {Id = ingredientReadDto.IngredientId},ingredientReadDto);
        } 

        [HttpDelete("{id}")]
        public ActionResult DeleteIngredient(Guid id)
        {
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