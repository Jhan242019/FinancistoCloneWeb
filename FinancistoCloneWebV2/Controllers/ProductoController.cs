using FinancistoCloneWebV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinancistoCloneWebV2.Controllers
{
    public class ProductoController : Controller
    {
        private FinancistoContext _context;

        public ProductoController(FinancistoContext context) { 
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var producto = _context.Productos.ToList();
            var categoria = _context.Productos.Include(o => o.Categoria).ToList(); // para que no mande NULL en la categoria

            return View(producto);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(new Producto());
        }
        [HttpPost]
        public ActionResult Create(Producto producto)
        {
            if (producto.Costo < 10) {
                ModelState.AddModelError("Costo","El costo debe ser mayor a S/. 10.00");
            }
            if (producto.IdCategoria == 0)
            {
                ModelState.AddModelError("Categoria", "Debe seleccionar una Categoria");
            }

            if (ModelState.IsValid) {
                _context.Productos.Add(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = _context.Categorias.ToList();
            return View("Create", producto);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //ViewBag.Categorias = new List<string> { "Laptops", "Impresoras", "Accesorios"};
            ViewBag.Categorias = _context.Categorias.ToList();
            var producto = _context.Productos.Where(o => o.Id == id).FirstOrDefault();
            return View(producto);  
        }
        [HttpPost]
        public ActionResult Edit(Producto producto) {
            if (producto.Costo < 10)
            {
                ModelState.AddModelError("Costo", "El costo debe ser mayor a S/. 10.00");
            }
            if (producto.IdCategoria == 0)
            {
                ModelState.AddModelError("Categoria", "Debe seleccionar una Categoria");
            }
            if (ModelState.IsValid) { 
                _context.Productos.Update(producto);
                _context.SaveChanges(); 
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = _context.Categorias.ToList();
            return View("Edit", producto);
        }

        [HttpGet]
        public ActionResult Delete(int id) {
            var DBproducto = _context.Productos.Where(o => o.Id == id).FirstOrDefault();
            _context.Productos.Remove(DBproducto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
