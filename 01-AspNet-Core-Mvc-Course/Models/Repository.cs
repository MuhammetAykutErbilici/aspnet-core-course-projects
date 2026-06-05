using System.Diagnostics;

namespace _1.Models
{
    public class Repository
    {
        // 1. Değişkeni static ve readonly olarak tanımlıyoruz.
        private static readonly List<Course> _courses;

        // 2. HATA BURADAYDI: Başına 'static' ekledik. 
        // Bu sayede readonly olan _courses alanına atama yapabiliriz.
        static Repository()
        {
            _courses = new List<Course>
            {
                new Course() { Id = 1, Title = "aspnet kursu", Description = "güzel bir kurs", Image = "1.webp", Tags = new string[] { "aspnet", "web geliştirme" },isActive = true, isHome = true },
                new Course() { Id = 2, Title = "php kursu", Description = "güzel bir kurs", Image = "2.webp", Tags = new string[] { "php", "web geliştirme" }, isActive = true, isHome = true },
                new Course() { Id = 3, Title = "django kursu", Description = "güzel bir kurs", Image = "3.webp",  isActive = true, isHome = true },
                new Course() { Id = 4, Title = "javascript kursu", Description = "güzel bir kurs", Image = "1.webp",  isActive = true, isHome = true },
            };
        }

        // 3. Kurs listesini dışarıya açan kapsülleme (Property)
        public static List<Course> Courses
        {
            get
            {
                return _courses;
            }
        }
        
        // 4. İleride tek bir kursu ID ile bulmak istersen bu metot çok işine yarayacak:
        public static Course? GetById(int id)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }

        
    }
}