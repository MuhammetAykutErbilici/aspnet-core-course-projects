using _1.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1.Controllers;


public class CourseController : Controller
{

     public IActionResult Details(int id)
    {
       var kurs = Repository.GetById(id);

        return View(kurs);
    }

      public IActionResult list()
    {
        return View("CourseList", Repository.Courses);
    }
}

