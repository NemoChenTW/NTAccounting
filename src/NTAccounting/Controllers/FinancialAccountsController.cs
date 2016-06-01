using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using NTAccounting.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace NTAccounting.Controllers
{
    public class FinancialAccountsController : Controller
    {
        private ApplicationDbContext _context;
        private UserGroupsController controllerUserGroups;

        public FinancialAccountsController(ApplicationDbContext context)
        {
            _context = context;
            controllerUserGroups = new UserGroupsController(_context);
        }

        [HttpGet]
        public SelectList GetFinancialAccountSelectList(int userGroupID = -1)
        {
            var accountQuary = from account in _context.FinancialAccount
                               .AsEnumerable()
                               where (account.UserGroupID == userGroupID)
                               orderby account.ID
                               select account;

            var AccountSelectList = new SelectList(accountQuary, "ID", "Name");

            return AccountSelectList;
        }

        [HttpGet]
        public JsonResult GetFinancialAccountJson(int userGroupID = -1)
        {
            var AccountSelectList = GetFinancialAccountSelectList(userGroupID);

            return Json(AccountSelectList);
        }

        // GET: FinancialAccounts
        public IActionResult Index()
        {
            // 加入Foreign key FinancialAccountType的value
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
            

            var representGrpID = _context.Users.Single(u => u.Id == User.GetUserId()).RepresentativeGroupID;
            var userGroups = controllerUserGroups.GetAvailableUserGroup(User.GetUserId(), representGrpID);
            ViewData["UserGroupID"] = new SelectList(userGroups, "ID", "Name");

            return View();
        }

        // POST: FinancialAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FinancialAccount financialAccount)
        {
            if (ModelState.IsValid)
            {
                // 設定餘額為初始金額
                financialAccount.Amount = financialAccount.InitialAmount;

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
