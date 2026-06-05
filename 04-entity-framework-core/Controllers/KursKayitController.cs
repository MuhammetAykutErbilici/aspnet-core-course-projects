using Microsoft.AspNetCore.Mvc;
using EntityFramework.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Controllers
{
    public class KursKayitController : Controller
    {
        private readonly DataContext _context;

        public KursKayitController(DataContext context)
        {
            _context = context;
        }

        // KURS KAYITLARINI LİSTELEME
        public async Task<IActionResult> Index()
        {
            // Include kullanarak Öğrenci ve Kurs bilgilerini beraber çekiyoruz
            var kursKayitlari = await _context.KursKayitlari
                                              .Include(x => x.Ogrenci)
                                              .Include(x => x.Kurs)
                                              .ToListAsync();
            
            return View(kursKayitlari); 
        }

        // YENİ KAYIT SAYFASI (Giriş Ekranı)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Dropdown listeler için verileri hazırlıyoruz
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            
            return View();
        }

        // YENİ KAYIT KAYDETME (Veritabanına Yazma)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {
            // Kayıt tarihini o anki zaman olarak ayarla
            model.KayitTarihi = DateTime.Now;

            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync(); 
            
            // DÜZELTME: Kayıttan sonra seni doğruca Kurs Kayıt Listesi'ne atar
            return RedirectToAction("Index", "KursKayit"); 
        }
    }
}