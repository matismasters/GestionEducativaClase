using GestionEducativa.Data;
using GestionEducativa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionEducativa.Controllers
{
    public class SemestresControladora : Controller
    {
        private readonly ApplicationDbContext _context;

        public SemestresControladora(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Semestres = _context.Semestres
                .Include(semestre => semestre.Carrera)
                .ToList();
            
            return View();
        }

        public IActionResult Nuevo()
        {
            ViewBag.Carreras = _context.Carreras.ToList();
            return View();
        }

        public IActionResult Crear(string nombre, int ordenNumero, int carreraId)
        {
            Semestre semestre = new Semestre(nombre, ordenNumero, carreraId);

            if (semestre.EsValido() == false)
            {
                ViewBag.Error = "Los datos ingresados no son válidos";
                ViewBag.Nombre = nombre;
                ViewBag.OrdenNumero = ordenNumero;

                return View("Nuevo");
            }

            _context.Semestres.Add(semestre);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Semestre semestre = _context.Semestres.Find(id);
            if (semestre == null)
            {
                return NotFound();
            }

            _context.Semestres.Remove(semestre);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Editando(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Semestre semestre = _context.Semestres.Find(id);
            if (semestre == null)
            {
                return NotFound();
            }

            ViewBag.Semestre = semestre;
            ViewBag.Carreras = _context.Carreras.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Editar(int id, string nombre, int ordenNumero, int carreraId)
        {
            Semestre semestre = _context.Semestres.Find(id);

            if (semestre == null)
            {
                return NotFound();
            }

            semestre.Nombre = nombre;
            semestre.OrdenNumero = ordenNumero;
            semestre.CarreraId = carreraId;

            if (semestre.EsValido())
            {
                _context.Semestres.Update(semestre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Editando");
        }

        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Semestre semestre = _context.Semestres.Find(id);
            if (semestre == null)
            {
                return NotFound();
            }

            ViewBag.Semestre = semestre;

            return View();
        }
    }
}
