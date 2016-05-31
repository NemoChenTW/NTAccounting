using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NTAccounting.Controllers;
using NTAccounting.Models;
using NTAccounting.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NTAccounting.ViewComponents
{
    public class TransactionCreateViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext db;
        private readonly TransactionsController controllerTransactions;

        public TransactionCreateViewComponent(ApplicationDbContext context)
        {
            db = context;
            controllerTransactions = new TransactionsController(db);
        }

        public async Task<IViewComponentResult> InvokeAsync(TransactionCategory.TransactionCategoryType type)
        {
            var viewModel = controllerTransactions.GetTransactionsCreateViewModel(User.Identity.Name);

            viewModel.TransactionCategoryType = type;
            
            // 根據交易類型取得主交易類別
            var MainQuary = from mainTran in db.MainTransactionCategory
                            where mainTran.TransactionType == type
                            select mainTran;
            var MainSelectList = new SelectList(MainQuary, "ID", "Name");
            viewModel.MainTransactionCategoryCollection = MainSelectList;
            // 子交易類別
            int MainTransID;
            try
            {
                int.TryParse(viewModel.MainTransactionCategoryCollection.FirstOrDefault().Value, out MainTransID);
            }
            catch (System.Exception)
            {
                MainTransID = 0;
            }
            
            viewModel.SubTransactionCategoryCollection = controllerTransactions.GetSubTransactionCategory(MainTransID);

            return View(viewModel);
        }

    }
}