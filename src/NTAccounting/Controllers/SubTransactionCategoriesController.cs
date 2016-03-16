using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using NTAccounting.Models;

namespace NTAccounting.Controllers
{
    public class SubTransactionCategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public SubTransactionCategoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: SubTransactionCategories
        public IActionResult Index()
        {
            var applicationDbContext = _context.SubTransactionCategory.Include(s => s.MainTransactionCategory);
            return View(applicationDbContext.ToList());
        }

        // GET: SubTransactionCategories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            SubTransactionCategory subTransactionCategory = _context.SubTransactionCategory.Single(m => m.ID == id);
            if (subTransactionCategory == null)
            {
                return HttpNotFound();
            }

            return View(subTransactionCategory);
        }

        // GET: SubTransactionCategories/Create
        public IActionResult Create()
        {
            ViewData["MainCategoryID"] = new SelectList(_context.MainTransactionCategory, "ID", "MainTransactionCategory");
            return View();
        }

        // POST: SubTransactionCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubTransactionCategory subTransactionCategory)
        {
            if (ModelState.IsValid)
            {
                _context.SubTransactionCategory.Add(subTransactionCategory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["MainCategoryID"] = new SelectList(_context.MainTransactionCategory, "ID", "MainTransactionCategory", subTransactionCategory.MainCategoryID);
            return View(subTransactionCategory);
        }

        // GET: SubTransactionCategories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            SubTransactionCategory subTransactionCategory = _context.SubTransactionCategory.Single(m => m.ID == id);
            if (subTransactionCategory == null)
            {
                return HttpNotFound();
            }
            ViewData["MainCategoryID"] = new SelectList(_context.MainTransactionCategory, "ID", "MainTransactionCategory", subTransactionCategory.MainCategoryID);
            return View(subTransactionCategory);
        }

        // POST: SubTransactionCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SubTransactionCategory subTransactionCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Update(subTransactionCategory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["MainCategoryID"] = new SelectList(_context.MainTransactionCategory, "ID", "MainTransactionCategory", subTransactionCategory.MainCategoryID);
            return View(subTransactionCategory);
        }

        // GET: SubTransactionCategories/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            SubTransactionCategory subTransactionCategory = _context.SubTransactionCategory.Single(m => m.ID == id);
            if (subTransactionCategory == null)
            {
                return HttpNotFound();
            }

            return View(subTransactionCategory);
        }

        // POST: SubTransactionCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            SubTransactionCategory subTransactionCategory = _context.SubTransactionCategory.Single(m => m.ID == id);
            _context.SubTransactionCategory.Remove(subTransactionCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
