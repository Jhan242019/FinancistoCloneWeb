using FinancistoCloneWebV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinancistoCloneWebV2.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly FinancistoContext context;
        public TransactionController(FinancistoContext context) : base(context)
        {
            this.context = context;
        }

        public IActionResult Transactions(int cuentaId)
        {
            var transactions = context.Transactions.Where(o => o.CuentaId == cuentaId).ToList();
            ViewBag.Account = context.Accounts.First(o => o.Id == cuentaId);
            return View(transactions);
        }
        [HttpGet]
        public IActionResult Transaction_Create(int cuentaId)
        {
            ViewBag.Account = context.Accounts.First(o => o.Id == cuentaId);
            return View(new Transaction());
        }
        [HttpPost]
        public IActionResult Transaction_Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AccountId = transaction.CuentaId;
                return View(transaction);
            }
            //Guardar transacción
            context.Transactions.Add(transaction);
            context.SaveChanges();

            // Actualizar saldo de la cuenta
            var cuenta = context.Accounts.FirstOrDefault(o => o.Id == transaction.CuentaId);
            if (transaction.Tipo == "Gasto")
                cuenta.Amount -= transaction.Monto;
            else
                cuenta.Amount += transaction.Monto;

            context.SaveChanges();

            return RedirectToAction("Transactions", new { cuentaId = transaction.CuentaId });
        }
    }
}
