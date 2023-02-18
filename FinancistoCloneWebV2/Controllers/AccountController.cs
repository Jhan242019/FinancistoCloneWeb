using FinancistoCloneWebV2.Models;
using Microsoft.AspNetCore.Mvc;
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
            _context= context;
        }
        public ViewResult Index()
        {
            ViewBag.Accounts = _context.Accounts.ToList();
            return View("Index");
        }
        public ViewResult Create()
        { 
            return View("Create");
        }
        public string Save(string Name, string Type, string Currency, decimal Amount)
        {
            var account = new Account { Name = Name, Type = Type, Currency = Currency, Amount = Amount };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return Type + " "+ Name +" "+Currency+" "+Amount;            
        }
    }
}
