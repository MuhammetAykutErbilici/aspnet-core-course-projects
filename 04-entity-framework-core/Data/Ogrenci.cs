using System.ComponentModel.DataAnnotations;
namespace EntityFramework.Data
{
    public class Ogrenci
    {
        [Key] //primary key olduğunu belirtmek için kullanılır
        public int OgrenciId { get; set; } //primary key s
        public string? OgrenciAd { get; set; } 
        public string? OgrenciSoyad { get; set; }
        public string AdSoyad {
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            } }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}