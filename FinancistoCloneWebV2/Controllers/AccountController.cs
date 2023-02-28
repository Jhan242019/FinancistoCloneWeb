using FinancistoCloneWebV2.Models;
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

namespace FinancistoCloneWebV2.Controllers
{
    public class AccountController : Controller
    {
        private FinancistoContext _context;
        private IHostEnvironment _hostEnv;
        public AccountController(FinancistoContext context, IHostEnvironment hostEnv)
        {
            _context = context;
            _hostEnv = hostEnv;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var account = _context.Accounts
                .Include(o => o.Type)
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
            if (ModelState.IsValid)
            {
                // Guardar archivo en el servidor

                if (image != null && image.Length > 0)
                {
                    var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                    var ruta = @"\files\" + image.FileName;

                    using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                    {
                        image.CopyTo(strem);
                        account.ImagePath = ruta;
                    }

                }
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
    }
}
