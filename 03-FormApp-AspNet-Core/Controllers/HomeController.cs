using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _2___FormApp_Projesi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _2___FormApp_Projesi.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Products;

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            ViewBag.SearchString = searchString;
            products = products.Where(p =>
                p.Name != null &&
                p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
        if (!string.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }
        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

        if (imageFile != null)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Sadece .jpg, .jpeg, .png ve .gif uzantılı dosyalar yüklenebilir.");
            }
            else
            {
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
            }
        }
        if (ModelState.IsValid)
        {
            model.ProductId = Repository.Products.Count + 1;
            Repository.CreateProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound();
        }
        if (imageFile == null)
        {
            ModelState.Remove("imageFile");
            ModelState.Remove("ImageFile");
        }
        if (ModelState.IsValid)
        {
            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName).ToLower();
                var randomFileName = string.Format($"{Guid.NewGuid()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
            }
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();

        var product = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var product = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (product != null)
        {
            Repository.DeleteProduct(product);
        }
        return RedirectToAction("Index");
    }
}