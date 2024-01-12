#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StoresController : Controller
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: Stores
        public IActionResult Index()
        {
            List<StoreModel> storeList = _storeService.GetList();
            return View(storeList);
        }

        // GET: Stores/Details/5
        public IActionResult Details(int id)
        {
            StoreModel store = _storeService.GetItem(id);
            if (store == null)
            {
                return View("_Error", "Store not found!");
            }
            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                var result = _storeService.Add(store);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = store.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(store);
        }

        // GET: Stores/Edit/5
        public IActionResult Edit(int id)
        {
            StoreModel store = _storeService.GetItem(id);
            if (store == null)
            {
                return View("_Error", "Store not found!");
            }
            return View(store);
        }

        // POST: Stores/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                var result = _storeService.Update(store);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = store.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(store);
        }

        // GET: Stores/Delete/5
        public IActionResult Delete(int id)
        {
            StoreModel store = _storeService.GetItem(id);
            if (store == null)
            {
                return View("_Error", "Store not found!");
            }
            return View(store);
        }

        // POST: Stores/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _storeService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
