using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using NTAccounting.Models;

namespace NTAccounting.Controllers
{
    public class MainTransactionCategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public MainTransactionCategoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: MainTransactionCategories
        public IActionResult Index()
        {
            return View(_context.MainTransactionCategory.ToList());
        }

        // GET: MainTransactionCategories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            MainTransactionCategory mainTransactionCategory = _context.MainTransactionCategory.Single(m => m.ID == id);
            if (mainTransactionCategory == null)
            {
                return HttpNotFound();
            }

            return View(mainTransactionCategory);
        }

        // GET: MainTransactionCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainTransactionCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MainTransactionCategory mainTransactionCategory)
        {
            if (ModelState.IsValid)
            {
                _context.MainTransactionCategory.Add(mainTransactionCategory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mainTransactionCategory);
        }

        // GET: MainTransactionCategories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            MainTransactionCategory mainTransactionCategory = _context.MainTransactionCategory.Single(m => m.ID == id);
            if (mainTransactionCategory == null)
            {
                return HttpNotFound();
            }
            return View(mainTransactionCategory);
        }

        // POST: MainTransactionCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MainTransactionCategory mainTransactionCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Update(mainTransactionCategory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mainTransactionCategory);
        }

        // GET: MainTransactionCategories/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            MainTransactionCategory mainTransactionCategory = _context.MainTransactionCategory.Single(m => m.ID == id);
            if (mainTransactionCategory == null)
            {
                return HttpNotFound();
            }

            return View(mainTransactionCategory);
        }

        // POST: MainTransactionCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            MainTransactionCategory mainTransactionCategory = _context.MainTransactionCategory.Single(m => m.ID == id);
            _context.MainTransactionCategory.Remove(mainTransactionCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
