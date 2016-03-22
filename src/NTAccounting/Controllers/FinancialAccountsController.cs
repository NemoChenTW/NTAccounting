using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using NTAccounting.Models;
using System.Collections.Generic;

namespace NTAccounting.Controllers
{
    public class FinancialAccountsController : Controller
    {
        private ApplicationDbContext _context;

        public FinancialAccountsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FinancialAccounts
        public IActionResult Index()
        {
            // ¥[¤JForeign key FinancialAccountTypeªºvalue
            var financialAccounts = _context.FinancialAccount.Include(f => f.FinancialAccountType);

            return View(financialAccounts.ToList());
        }

        // GET: FinancialAccounts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FinancialAccount financialAccount = _context.FinancialAccount.Single(m => m.ID == id);
            if (financialAccount == null)
            {
                return HttpNotFound();
            }

            return View(financialAccount);
        }

        // GET: FinancialAccounts/Create
        public IActionResult Create()
        {
            ViewData["TypeID"] = new SelectList(_context.FinancialAccountType, "ID", "Type");
            return View();
        }

        // POST: FinancialAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FinancialAccount financialAccount)
        {
            if (ModelState.IsValid)
            {
                _context.FinancialAccount.Add(financialAccount);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(financialAccount);
        }

        // GET: FinancialAccounts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FinancialAccount financialAccount = _context.FinancialAccount.Single(m => m.ID == id);
            if (financialAccount == null)
            {
                return HttpNotFound();
            }
            return View(financialAccount);
        }

        // POST: FinancialAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FinancialAccount financialAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Update(financialAccount);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(financialAccount);
        }

        // GET: FinancialAccounts/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FinancialAccount financialAccount = _context.FinancialAccount.Single(m => m.ID == id);
            if (financialAccount == null)
            {
                return HttpNotFound();
            }

            return View(financialAccount);
        }

        // POST: FinancialAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            FinancialAccount financialAccount = _context.FinancialAccount.Single(m => m.ID == id);
            _context.FinancialAccount.Remove(financialAccount);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
