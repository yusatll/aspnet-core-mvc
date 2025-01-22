using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _servicemanager;

        public ProductController(IServiceManager manager)
        {
            _servicemanager = manager;
        }

        public IActionResult Index()
        {
            var model = _servicemanager.ProductService.GetAllProducts(false);
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoriesSelectList();
            // _servicemanager.CategoryService.GetAllCategories(false); // ViewBag ile Category'leri View'e gönderiyoruz
            return View();
        }

        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(
                _servicemanager.CategoryService.GetAllCategories(false),
                    "CategoryId", 
                    "CategoryName", 
                    "1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] ProductDtoForInsertion productDto)
        {
            if(ModelState.IsValid)
            {
                _servicemanager.ProductService.CreateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories = GetCategoriesSelectList();
            var model = _servicemanager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm]ProductDtoForUpdate product)
        {
            if (ModelState.IsValid)
            {
                _servicemanager.ProductService.UpdateOneProduct(product);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")]int id)
        {
            _servicemanager.ProductService.DeleteOneProduct(id);
            return RedirectToAction("Index");
        }
    }
}