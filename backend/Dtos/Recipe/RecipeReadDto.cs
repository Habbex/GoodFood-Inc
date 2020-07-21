namespace backend.Dtos.Recipe
{
    public class RecipeReadDto
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}