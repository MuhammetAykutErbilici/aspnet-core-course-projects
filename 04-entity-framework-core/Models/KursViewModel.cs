using EntityFramework.Data;
namespace EntityFramework.Models
{
    public class KursViewModel
    {
        
        public int KursId { get; set; } 
        public string? Baslik { get; set; }
        public int? OgretmenId { get; set; }
        
        public IEnumerable<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

        
    }
}