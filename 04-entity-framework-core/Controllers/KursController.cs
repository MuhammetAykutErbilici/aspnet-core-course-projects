using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using EntityFramework.Models;
using System.Linq;

namespace EntityFramework.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;

        public KursController(DataContext context)
        {
            _context = context;
        }

        // KURS LİSTELEME
        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.Include(k => k.Ogretmen).ToListAsync();
            return View(kurslar);
        }

        // YENİ KURS EKLEME (SAYFA)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View();
        }

        // YENİ KURS EKLEME (KAYIT)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kurs model)
        {
            if (ModelState.IsValid)
            {
                _context.Kurslar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // KURS DÜZENLEME (SAYFA)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // 1. Veritabanından kursu ve ilişkili verileri çekiyoruz
            var kurs = await _context.Kurslar
                        .Include(k => k.KursKayitlari)
                        .ThenInclude(k => k.Ogrenci)
                        .Select(k => new KursViewModel
                        {
                            KursId=k.KursId,
                            Baslik = k.Baslik,
                            OgretmenId = k.OgretmenId,
                            KursKayitlari = k.KursKayitlari
                        })
                        .FirstOrDefaultAsync(k => k.KursId == id);

            if (kurs == null) return NotFound();
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");

            // 2. Çektiğimiz 'Kurs' modelini 'KursViewModel'e dönüştürüyoruz
            var viewModel = new KursViewModel
            {
                KursId = kurs.KursId,
                Baslik = kurs.Baslik,
                OgretmenId = kurs.OgretmenId,
                KursKayitlari = kurs.KursKayitlari // ViewModel'de ICollection<KursKayit> olmalı
            };

            // 3. View'a artık Kurs değil, ViewModel gönderiyoruz
            return View(viewModel);
        }

        // KURS DÜZENLEME (GÜNCELLEME)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KursViewModel model)
        {
            if (id != model.KursId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs() { KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // KURS SİLME (ONAY SAYFASI)
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null) return NotFound();

            return View(kurs);
        }

        // KURS SİLME (FİZİKSEL SİLME)
        // ActionName("Delete") kullanarak çakışmayı önledik
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null) return NotFound();

            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}