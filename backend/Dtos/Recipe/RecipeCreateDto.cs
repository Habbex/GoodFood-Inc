using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Recipe
{
    public class RecipeCreateDto
    {
        [Required]
        [MaxLength(90)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Slug { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }
    }
}