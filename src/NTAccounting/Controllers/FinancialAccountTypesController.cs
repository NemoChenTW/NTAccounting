using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using NTAccounting.Models;

namespace NTAccounting.Controllers
{
    public class FinancialAccountTypesController : Controller
    {
        private ApplicationDbContext _context;

        public FinancialAccountTypesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FinancialAccountTypes
        public IActionResult Index()
        {
            return View(_context.FinancialAccountType.ToList());
        }

        // GET: FinancialAccountTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FinancialAccountType financialAccountType = _context.FinancialAccountType.Single(m => m.ID == id);
            if (financialAccountType == null)
            {
                return HttpNotFound();
            }

            return View(financialAccountType);
        }

        // GET: FinancialAccountTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FinancialAccountTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FinancialAccountType financialAccountType)
        {
            if (ModelState.IsValid)
            {
                _context.FinancialAccountType.Add(financialAccountType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(financialAccountType);
        }

        // GET: FinancialAccountTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FinancialAccountType financialAccountType = _context.FinancialAccountType.Single(m => m.ID == id);
            if (financialAccountType == null)
            {
                return HttpNotFound();
            }
            return View(financialAccountType);
        }

        // POST: FinancialAccountTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FinancialAccountType financialAccountType)
        {
            if (ModelState.IsValid)
            {
                _context.Update(financialAccountType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(financialAccountType);
        }

        // GET: FinancialAccountTypes/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FinancialAccountType financialAccountType = _context.FinancialAccountType.Single(m => m.ID == id);
            if (financialAccountType == null)
            {
                return HttpNotFound();
            }

            return View(financialAccountType);
        }

        // POST: FinancialAccountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            FinancialAccountType financialAccountType = _context.FinancialAccountType.Single(m => m.ID == id);
            _context.FinancialAccountType.Remove(financialAccountType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
