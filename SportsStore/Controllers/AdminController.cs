using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: AdminController
        public ViewResult Index()
        {
            return View(repository.Products);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ViewResult Edit(int productId)
        {
            return View(repository.Products.FirstOrDefault(p => p.ProductId == productId));
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var product = repository.DeleteProduct(productId);
            if (product != null)
            {
                TempData["message"] = $"{product.Name} was deleted";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
