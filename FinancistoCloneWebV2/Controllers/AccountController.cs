using FinancistoCloneWebV2.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FinancistoCloneWebV2.Controllers
{
    public class AccountController : Controller
    {
        private FinancistoContext _context;
        public AccountController(FinancistoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var account = _context.Accounts.ToList();
            return View("Index", account);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create", new Account());
        }

        [HttpPost]
        public ActionResult Create(Account account)
        {
            // Es una forma de validar campos
            //if (account.Name =="" || account.Name==null)
            //    ModelState.AddModelError("Name", "El campo nombre es obligatorio");

            if (ModelState.IsValid)
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", account);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Types = new List<String> { "Efectivo", "Credito", "Debito", "Billetera" };
            var account = _context.Accounts.Where(o => o.Id == id).FirstOrDefault();
            return View("Edit", account);
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        { 
            // Cuando queremos actualizar solo algunos campos
            //var DBaccount = _context.Accounts.Where(o => o.Id == account.Id).FirstOrDefault();
            //DBaccount.Name = account.Name;
            //DBaccount.Amount = account.Amount;

            if (ModelState.IsValid)
            {
                _context.Accounts.Update(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", account);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var DBaccount = _context.Accounts.Where(o => o.Id == id).FirstOrDefault();
            _context.Accounts.Remove(DBaccount);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
