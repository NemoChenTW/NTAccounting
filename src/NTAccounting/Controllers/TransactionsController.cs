using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using NTAccounting.Models;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using NTAccounting.ViewModels.Transactions;

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
            var applicationDbContext = _context.Transaction.Include(t => t.SubTransactionCategory);

            var transactionIndexList = from transaction in applicationDbContext
                                  orderby transaction.Time
                                       select transaction;

            return View(transactionIndexList.ToList());
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


        protected SelectList GetMainTransactionCategory(int selectedID = -1)
        {
            var MainQuary = from tranCategory in _context.MainTransactionCategory
                            .AsEnumerable()
                            orderby tranCategory.ID
                            select tranCategory;
            SelectList MainSelectList;
            if (selectedID != -1)
                MainSelectList = new SelectList(MainQuary, "ID", "Name", selectedID);
            else
                MainSelectList = new SelectList(MainQuary, "ID", "Name");


            return MainSelectList;
        }
        [HttpGet]
        public SelectList GetSubTransactionCategory(int id = -1)
        {
            var SubQuary = from tranCategory in _context.SubTransactionCategory
                            .AsEnumerable()
                           where (tranCategory.MainCategoryID == id)
                           orderby tranCategory.ID
                           select tranCategory;

            var SubSelectList = new SelectList(SubQuary, "ID", "Name");

            return SubSelectList;
        }

        [HttpGet]
        public JsonResult GetSubTransactionCategoryJson(int id = -1)
        {
            var SubSelectList = GetSubTransactionCategory(id);

            return Json(SubSelectList);
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

        // GET: Transactions/Create
        public IActionResult Create()
        {
            TransactionsViewModel transactionViewModel = new TransactionsViewModel();

            transactionViewModel.MainTransactionCategoryCollection = GetMainTransactionCategory();

            // ����UserGroup �� SelectList
            UserGroupsController controllerUserGroup = new UserGroupsController(_context);
            transactionViewModel.UserGroupCollection = new SelectList(controllerUserGroup.GetAvailableUserGroup(User.GetUserId(), true), "ID", "Name");


            // ���oUserGroup��DisplayName
            MemberInfo property = typeof(UserGroup).GetProperty("Name");
            var displayNameObj = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            transactionViewModel.UserGroupDisplayName = displayNameObj.Name;

            // ���o�w�]UserGroup
            var grpID = _context.UserGroupApplicationUser.FirstOrDefault(grp => grp.ApplicationUserID == User.GetUserId()).UserGroupID;
            transactionViewModel.FinancialAccountCollection = GetFinancialAccountSelectList(grpID);

            ViewBag.viewModel = transactionViewModel;

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
                // �s�W���
                _context.Transaction.Add(transaction);

                // �b�ᦩ��
                var account = _context.FinancialAccount.SingleOrDefault(ac => ac.ID == transaction.FinancialAccountID);
                account.Amount = account.Amount - transaction.Amount;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["SubTransactionCategoryID"] = new SelectList(_context.SubTransactionCategory, "ID", "SubTransactionCategory", transaction.SubTransactionCategoryID);
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "Name");
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

            TransactionsViewModel transactionViewModel = new TransactionsViewModel();

            // ���o�D������O
            var MainTransID = _context.SubTransactionCategory.Single(s => s.ID == transaction.SubTransactionCategoryID).MainCategoryID;
            transactionViewModel.MainTransactionCategoryCollection = GetMainTransactionCategory(MainTransID);

            transactionViewModel.SubTransactionCategoryCollection = GetSubTransactionCategory(MainTransID);

            // ����UserGroup �� SelectList
            UserGroupsController controllerUserGroup = new UserGroupsController(_context);
            transactionViewModel.UserGroupCollection = new SelectList(controllerUserGroup.GetAvailableUserGroup(User.GetUserId(), true), "ID", "Name");

            // ���oUserGroup��DisplayName
            MemberInfo property = typeof(UserGroup).GetProperty("Name");
            var displayNameObj = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            transactionViewModel.UserGroupDisplayName = displayNameObj.Name;

            // ���o�w�]UserGroup
            var grpID = _context.UserGroupApplicationUser.FirstOrDefault(grp => grp.ApplicationUserID == User.GetUserId()).UserGroupID;
            transactionViewModel.FinancialAccountCollection = GetFinancialAccountSelectList(grpID);

            ViewBag.viewModel = transactionViewModel;

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
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "Name");
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

            // �^�_�b��l�B
            var account = _context.FinancialAccount.SingleOrDefault(ac => ac.ID == transaction.FinancialAccountID);
            account.Amount = account.Amount + transaction.Amount;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
