using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }
        
        public int OgrenciId { get; set; } 
        // 'ogrenci' yerine 'Ogrenci' yapıyoruz
        public Ogrenci Ogrenci { get; set; } = null!;

        public int KursId { get; set; }
        // 'kurs' yerine 'Kurs' yapıyoruz
        public Kurs Kurs { get; set; } = null!;
        
        public DateTime KayitTarihi { get; set; }
    } 
}