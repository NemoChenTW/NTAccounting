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
        private FinancialAccountsController controllerFinancialAccounts;
        private UserGroupsController controllerUserGroups;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
            controllerFinancialAccounts = new FinancialAccountsController(_context);
            controllerUserGroups = new UserGroupsController(_context);
        }


        public TransactionsCreateViewModel GetTransactionsCreateViewModel(string userName = null)
        {
            string USERID = null;

            if (userName == null)
            {
                USERID = User.GetUserId();
            }
            else
            {
                USERID = _context.Users.SingleOrDefault(u => u.NormalizedUserName == userName.ToUpper()).Id;
            }
            

            TransactionsCreateViewModel transactionCreateViewModel = new TransactionsCreateViewModel();

            // 取得主交易類別
            transactionCreateViewModel.MainTransactionCategoryCollection = GetMainTransactionCategory();

            // 取得子交易類別
            int MainTransID;
            int.TryParse(transactionCreateViewModel.MainTransactionCategoryCollection.First().Value, out MainTransID);
            transactionCreateViewModel.SubTransactionCategoryCollection = GetSubTransactionCategory(MainTransID);

            // 預設群組
            var representGrpID = _context.Users.Single(u => u.Id == USERID).RepresentativeGroupID;
            
            // 產生UserGroup 的 SelectList
            transactionCreateViewModel.UserGroupCollection = new SelectList(controllerUserGroups.GetAvailableUserGroup(USERID, representGrpID), "ID", "Name");


            // 取得UserGroup的DisplayName
            MemberInfo property = typeof(UserGroup).GetProperty("Name");
            var displayNameObj = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            transactionCreateViewModel.UserGroupDisplayName = displayNameObj.Name;

            // 取得預設UserGroup
            transactionCreateViewModel.FinancialAccountCollection = controllerFinancialAccounts.GetFinancialAccountSelectList(representGrpID);


            // 預設交易時間
            var transaction = new Transaction();
            transaction.Time = DateTime.Today;
            transactionCreateViewModel.Transaction = transaction;

            return transactionCreateViewModel;
        }

        // GET: Transactions
        public IActionResult Index(int? id)
        {
            var applicationDbContext = _context.Transaction.Include(t => t.SubTransactionCategory)
                                                           .Include(t => t.FinancialAccount)
                                                           .Include(t => t.TargetFinancialAccount);

            TransactionsIndexViewModel transactionIndexViewModel = new TransactionsIndexViewModel();

            IQueryable<Transaction> transactionIndexList;
            if (id != null)
            {
                transactionIndexList = from transaction in applicationDbContext
                                       where transaction.FinancialAccountID == id
                                       orderby transaction.Time descending
                                       select transaction;
            }
            else
            {
                transactionIndexList = from transaction in applicationDbContext
                                       orderby transaction.Time descending
                                       select transaction;
            }

            // 取得不重複的交易帳戶名稱
            var acIDAuqry = (from tran in transactionIndexList
                             select tran.FinancialAccount.Name)
                            .Distinct();
            transactionIndexViewModel.FinacialAccountNameList = acIDAuqry.ToList();

            ViewBag.viewModel = transactionIndexViewModel;

            ViewBag.viewModelCreate = GetTransactionsCreateViewModel();

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


        protected SelectList GetMainTransactionCategory()
        {
            var MainQuary = from tranCategory in _context.MainTransactionCategory
                            .AsEnumerable()
                            orderby tranCategory.ID
                            select tranCategory;

            var MainSelectList = new SelectList(MainQuary, "ID", "Name");

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

        // GET: Transactions/Create
        public IActionResult Create()
        {
            TransactionsCreateViewModel transactionCreateView = GetTransactionsCreateViewModel();

            return View(transactionCreateView);
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                // 新增交易
                _context.Transaction.Add(transaction);

                // 修改帳戶餘額
                transaction.CreateTransaction(_context);

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

            TransactionsCreateViewModel transactionViewModel = new TransactionsCreateViewModel();

            // 取得主交易類別
            var MainTransID = _context.SubTransactionCategory.Single(s => s.ID == transaction.SubTransactionCategoryID).MainCategoryID;
            transactionViewModel.MainTransactionCategoryCollection = GetMainTransactionCategory();

            // 取得子交易類別
            transactionViewModel.SubTransactionCategoryCollection = GetSubTransactionCategory(MainTransID);

            // 產生UserGroup 的 SelectList
            var transactionGroupID = _context.FinancialAccount.Single(f => f.ID == transaction.FinancialAccountID).UserGroupID;
            transactionViewModel.UserGroupCollection = new SelectList(controllerUserGroups.GetAvailableUserGroup(User.GetUserId(), transactionGroupID), "ID", "Name");

            // 取得UserGroup的DisplayName
            MemberInfo property = typeof(UserGroup).GetProperty("Name");
            var displayNameObj = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            transactionViewModel.UserGroupDisplayName = displayNameObj.Name;

            // 取得預設交易帳戶
            var grpID = _context.UserGroupApplicationUser.FirstOrDefault(grp => grp.ApplicationUserID == User.GetUserId()).UserGroupID;
            transactionViewModel.FinancialAccountCollection = controllerFinancialAccounts.GetFinancialAccountSelectList(transactionGroupID);

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
                // 更新帳戶餘額
                transaction.UpdateTransaction(_context);

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

            // 回復帳戶餘額
            transaction.DeleteTransaction(_context);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
