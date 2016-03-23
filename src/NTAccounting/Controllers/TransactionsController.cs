using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using NTAccounting.Models;
using System;

namespace NTAccounting.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Transactions
        public IActionResult Index()
        {
            var applicationDbContext = _context.Transaction.Include(t => t.SubTransactionCategory).Include(t => t.UserGroup);
            return View(applicationDbContext.ToList());
        }

        // GET: Transactions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Transaction transaction = _context.Transaction.Single(m => m.ID == id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["SubTransactionCategoryID"] = new SelectList(_context.SubTransactionCategory, "ID", "SubTransactionCategory");
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "UserGroup");
            Transaction transaction = new Transaction();
            transaction.Time = DateTime.Today;
            return View(transaction);
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Transaction.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["SubTransactionCategoryID"] = new SelectList(_context.SubTransactionCategory, "ID", "SubTransactionCategory", transaction.SubTransactionCategoryID);
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "UserGroup", transaction.UserGroupID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Transaction transaction = _context.Transaction.Single(m => m.ID == id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewData["SubTransactionCategoryID"] = new SelectList(_context.SubTransactionCategory, "ID", "SubTransactionCategory", transaction.SubTransactionCategoryID);
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "UserGroup", transaction.UserGroupID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Update(transaction);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["SubTransactionCategoryID"] = new SelectList(_context.SubTransactionCategory, "ID", "SubTransactionCategory", transaction.SubTransactionCategoryID);
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "UserGroup", transaction.UserGroupID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Transaction transaction = _context.Transaction.Single(m => m.ID == id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = _context.Transaction.Single(m => m.ID == id);
            _context.Transaction.Remove(transaction);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
