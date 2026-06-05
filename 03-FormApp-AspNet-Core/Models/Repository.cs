namespace _2___FormApp_Projesi.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        static Repository()
        {
            _categories.Add(new Category { CategoryId = 1, Name = "Telefon" });
            _categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });
            _products.Add(new Product { ProductId = 1, Name = "Iphone 17 Pro Max", Price = 120000, IsActive = true, Image = "shopping (2).webp", CategoryId = 1 });
            _products.Add(new Product { ProductId = 2, Name = "Iphone 17 Pro ", Price = 100000, IsActive = true, Image = "shopping (1).webp", CategoryId = 1 });
            _products.Add(new Product { ProductId = 3, Name = "Iphone 16 ", Price = 60000, IsActive = true, Image = "shopping.webp", CategoryId = 1 });
            _products.Add(new Product { ProductId = 4, Name = "Iphone 16 Pro Max ", Price = 95000, IsActive = true, Image = "indir.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 5, Name = "MacBook Air ", Price = 50000, IsActive = true, Image = "1.jpg", CategoryId = 2 });
            _products.Add(new Product { ProductId = 6, Name = "MacBook Pro ", Price = 120000, IsActive = true, Image = "2.webp", CategoryId = 2 });

        }

        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }
        public static void CreateProduct(Product entity)
        {

            _products.Add(entity);
        }

        public static void EditProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (entity != null)
            {
                entity.Name = updatedProduct.Name;
                entity.Price = updatedProduct.Price;
                entity.Image = updatedProduct.Image;
                entity.CategoryId = updatedProduct.CategoryId;
                entity.IsActive = updatedProduct.IsActive;
            }
        }

        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }

        public static void DeleteProduct(Product entity)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == entity.ProductId);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}