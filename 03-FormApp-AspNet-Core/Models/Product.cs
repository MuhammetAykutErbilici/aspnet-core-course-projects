using System.ComponentModel.DataAnnotations;

namespace _2___FormApp_Projesi.Models
{
    public class Product
    {
        [Display(Name = "Ürün Id")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Ürün Adı")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Ürün adı 3 ile 100 karakter arasında olmalıdır.")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0, 150000)]
        [Display(Name = "Fiyat")] 
        public decimal? Price { get; set; }
        
        [Display(Name = "Resim")] 
        public string Image { get; set; } = null;
        
        public bool IsActive { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        public int? CategoryId { get; set; }


        public IFormFile ImageFile { get; set; }
    }
}


