using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = controllerTransactions.GetTransactionsCreateViewModel(User.Identity.Name);

            return View(viewModel);
        }

    }
}