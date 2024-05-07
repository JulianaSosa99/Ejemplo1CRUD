using Ejemplo1.Data;
using Ejemplo1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Ejemplo1.Controllers
{
    public class InicioController : Controller

    { 
        //Arreglar el editar por que se me deben cargar los datos
        //comento por que aun no hago login solo CRUD
        //private readonly ILogger<InicioController> _logger;
        private readonly ApplicationDbContext _contexto;
        public InicioController(ApplicationDbContext contexto)//ILogger<InicioController> logger)
        {
          _contexto= contexto;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Contacto.ToListAsync());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                //Agregar fecha y hora actual
                contacto.FechaCreacion = DateTime.Now;

                _contexto.Contacto.Add(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contacto.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }
                return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        [HttpGet]
        public IActionResult Detalle(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contacto.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }
        [HttpGet]
        public IActionResult Borrar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contacto.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrar(int? id)
        {
            var contacto = await _contexto.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return View();
            }
             //BORRADO
             _contexto.Contacto.Remove(contacto);
            await _contexto.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
