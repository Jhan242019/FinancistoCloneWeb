using FinancistoCloneWebV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Security.Principal;

namespace FinancistoCloneWebV2.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly FinancistoContext context;

        public BaseController(FinancistoContext context)
        {
            this.context = context;
        }
        protected User LoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = context.Users.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }
    }
}
