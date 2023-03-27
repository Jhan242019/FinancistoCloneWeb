using FinancistoCloneWebV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace FinancistoCloneWebV2.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private FinancistoContext _context;
        private IHostEnvironment _hostEnv;
        public AccountController(FinancistoContext context, IHostEnvironment hostEnv) : base(context)
        {
            _context = context;
            _hostEnv = hostEnv;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var account = _context.Accounts
                .Where(o => o.UserId == LoggedUser().Id) //para traer las cuentas del usuario logueado
                .Include(o => o.Type)
                .OrderByDescending(o => o.Id)
                .ToList();

            return View("Index", account);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Types = _context.Types.ToList();
            return View("Create", new Account());
        }

        [HttpPost]
        public ActionResult Create(Account account, IFormFile image)
        {
            account.UserId = LoggedUser().Id;

            if (ModelState.IsValid)
            {
                account.ImagePath = SaveImage(image);

                account.Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        FechaHora = DateTime.Now,
                        Tipo = "Ingreso",
                        Monto = account.Amount,
                        Motivo = "Monto Inicial"
                    }
                };
                _context.Accounts.Add(account);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Types = _context.Types.ToList();
            return View("Create", account);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Types = _context.Types.ToList();
            var account = _context.Accounts
                .Where(o => o.Id == id)
                .FirstOrDefault();
            return View("Edit", account);
        }

        [HttpPost]
        public ActionResult Edit(Account account, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (SaveImage(image) != null)
                {
                    account.ImagePath = SaveImage(image);
                }

                account.UserId = LoggedUser().Id;
                _context.Accounts.Update(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Types = _context.Types.ToList();
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

        private string SaveImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                var ruta = @"\files\" + image.FileName;

                using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                {
                    image.CopyTo(strem);
                    return ruta;
                }
            }
            return null;
        }
    }
}
