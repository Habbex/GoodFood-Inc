using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Recipe
{
    public class RecipeBulkDeleteDto
    {   
        [Required]
        public IEnumerable<Guid> RecipeIds { get; set; }
    }
}