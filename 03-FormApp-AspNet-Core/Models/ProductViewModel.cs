namespace _2___FormApp_Projesi.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = null!;
        public List<Category> Categories { get; set; }
        public string? SelectedCategory { get; set; }

    }
}