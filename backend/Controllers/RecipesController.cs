using backend.DataAccess.Models;
using backend.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {

        private RecipesRepoMock  _repository = new RecipesRepoMock();
        public RecipesController()
        {
            
        }

        public ActionResult <IEnumerable<Recipe>> GetAllRecipes()
        {
            var recipeItems= _repository.GetAllRecipes();

            return Ok(recipeItems);
        }
    }
}