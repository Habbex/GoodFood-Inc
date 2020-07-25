using System;
using System.Collections.Generic;
using backend.Models;

namespace backend.Dtos.Recipe
{
    public class RecipeReadDto
    {
        public Guid RecipeId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

         public IEnumerable<RecipeIngredients> RecipeIngredients {get;set; }
    }
}