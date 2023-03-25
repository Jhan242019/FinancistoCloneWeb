using FinancistoCloneWebV2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace FinancistoCloneWebV2.Controllers
{
    public class AuthController : BaseController
    {
        private FinancistoContext context;
        private IConfiguration configuration;

        public AuthController(FinancistoContext context, IConfiguration configuration):base(context)
        { 
            this.context = context;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            return View(new User());
        }
        [HttpPost]
        public IActionResult Create(User user, string ConfirmPassword)
        {
            user.Password = CreateHash(user.Password);
            if (user.Password != CreateHash(ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPass", "Las contraseñas no coinciden");
            }
            if (ModelState.IsValid) {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Login","Auth");
            }
            return View("Create", user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            /* validar si el usuario existe en la base de datos y 
             verificar que el password o usuario sean correctos
             */
            var user = context.Users
                .Where(o => o.Username == username && o.Password == CreateHash(password))
                .FirstOrDefault();

            if (user != null)
            {
                // Auntenticamos
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Login", "Usuario o Contraseña Incorrectos");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        private string CreateHash(string input) 
        { 
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}
