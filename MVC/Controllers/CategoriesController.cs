#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        [Authorize]
        public IActionResult Index()
        {
            List<CategoryModel> categoryList = _categoryService.GetList();
            return View(categoryList);
        }

        // GET: Categories/Details/5
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            CategoryModel category = _categoryService.GetItem(id);
            if (category == null)
            {
                return View("_Error", "Category not found!");
            }
            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Add(category);
                if (result.IsSuccessful)
					return RedirectToAction(nameof(Details), new { id = category.Id });
				ModelState.AddModelError("", result.Message);
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
			CategoryModel category = _categoryService.GetItem(id);
			if (category == null)
			{
				return View("_Error", "Category not found!");
			}
			return View(category);
		}

        // POST: Categories/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CategoryModel category)
        {
			if (ModelState.IsValid)
			{
				var result = _categoryService.Update(category);
				if (result.IsSuccessful)
					return RedirectToAction(nameof(Details), new { id = category.Id });
				ModelState.AddModelError("", result.Message);
			}
			return View(category);
		}

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
			CategoryModel category = _categoryService.GetItem(id);
			if (category == null)
			{
				return View("_Error", "Category not found!");
			}
			return View(category);
		}

        // POST: Categories/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _categoryService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
