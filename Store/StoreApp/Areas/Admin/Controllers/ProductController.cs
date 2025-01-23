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

        // GET: Admin/Product/Create
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

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file)
        {
            if(ModelState.IsValid)
            {
                // file operations
                // bize dosya olarak gelecek ama bize url gerekiyor.
                string path = Path.Combine(Directory.GetCurrentDirectory(), 
                    "wwwroot", "images", file.FileName); // a/b/c

                using (var stream = new FileStream(path, FileMode.Create)) // maliyetli işlemler using içinde tanımlanabilir. {} dışında değerler silindiği için
                {
                    await file.CopyToAsync(stream); // dosyayı stream'e kopyala
                }

                productDto.ImageUrl = String.Concat("/images/", file.FileName);
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
        public async Task<IActionResult> Update([FromForm]ProductDtoForUpdate productDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // file operations
                // bize dosya olarak gelecek ama bize url gerekiyor.
                string path = Path.Combine(Directory.GetCurrentDirectory(), 
                    "wwwroot", "images", file.FileName); // a/b/c

                using (var stream = new FileStream(path, FileMode.Create)) // maliyetli işlemler using içinde tanımlanabilir. {} dışında değerler silindiği için
                {
                    await file.CopyToAsync(stream); // dosyayı stream'e kopyala
                }

                productDto.ImageUrl = String.Concat("/images/", file.FileName);
                _servicemanager.ProductService.UpdateOneProduct(productDto);
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