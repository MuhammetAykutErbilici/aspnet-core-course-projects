using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _1.Models;

namespace _1.Controllers;

//localhost             => home/index
//localhost/home        => home/index
//localhost/home/index  => home/index

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(Repository.Courses);
    }

    public IActionResult Contact()
    {
        return View();
    }

    
}
