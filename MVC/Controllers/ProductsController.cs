#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//Generated from Custom Template.
namespace MVC.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;

		public ProductsController(IProductService productService, ICategoryService categoryService, IStoreService storeService)
		{
			_productService = productService;
			_categoryService = categoryService;
			_storeService = storeService;
		}

        // GET: Products
        [AllowAnonymous]
		public IActionResult Index()
        {
            List<ProductModel> productList = _productService.Query().ToList();
            return View(productList);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            ProductModel product = _productService.Query().SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return View("_Error", "Product not found!");
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryService.Query().ToList(), "Id", "Name");
            ViewBag.Stores = new MultiSelectList(_storeService.Query().ToList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Add(product);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            ViewData["CategoryId"] = new SelectList(_categoryService.Query().ToList(), "Id", "Name");
            ViewBag.Stores = new MultiSelectList(_storeService.Query().ToList(), "Id", "Name");
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            ProductModel product = _productService.Query().SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return View("_Error", "Product not found!");
            }
            ViewData["CategoryId"] = new SelectList(_categoryService.Query().ToList(), "Id", "Name");
            ViewBag.Stores = new MultiSelectList(_storeService.Query().ToList(), "Id", "Name");
            return View(product);
        }

        // POST: Products/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProductModel product)
        {
			if (ModelState.IsValid)
			{
				var result = _productService.Update(product);
				if (result.IsSuccessful)
				{
					TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Details), new { id = product.Id });
				}
				ModelState.AddModelError("", result.Message);
			}
			ViewData["CategoryId"] = new SelectList(_categoryService.Query().ToList(), "Id", "Name");
			ViewBag.Stores = new MultiSelectList(_storeService.Query().ToList(), "Id", "Name");
			return View(product);
		}

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
