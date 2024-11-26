using Microsoft.AspNetCore.Mvc;
using GestionEducativa.Models;
using GestionEducativa.Data;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace GestionEducativa.Controllers
{
    public class CarrerasControladora : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrerasControladora(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Carreras = _context.Carreras.ToList();
            return View();
        }

        public IActionResult Nueva()
        {
            return View();
        }

        public IActionResult Crear(string nombre, string descripcion, string requerimientos)
        {
            Carrera carrera = new Carrera(nombre, descripcion, requerimientos);

            if (carrera.EsValido())
            {
                _context.Carreras.Add(carrera);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Los datos ingresados no son válidos";
            ViewBag.Nombre = nombre;
            ViewBag.Descripcion = descripcion;
            ViewBag.Requerimientos = requerimientos;

            return View("Nueva");
        }

        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Carrera carrera = _context.Carreras.Find(id);
            if (carrera == null) {
                return NotFound();
            }

            _context.Carreras.Remove(carrera);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Editando(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Carrera carrera = _context.Carreras.Find(id);
            if (carrera == null)
            {
                return NotFound();
            }

            ViewBag.Carrera = carrera;

            return View();
        }

        public IActionResult Editar(int id, string nombre, string descripcion, string requerimientos)
        {
            Carrera carrera = _context.Carreras.Find(id);
            if (carrera == null) {
                return NotFound();
            }

            carrera.Nombre = nombre;
            carrera.Descripcion = descripcion;
            carrera.Requerimientos = requerimientos;

            if (carrera.EsValido())
            {
                _context.Carreras.Update(carrera);
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

            // Buscamos la carrera asegurandonos de que tambien traiga los semestres

            Carrera carrera = _context.Carreras
                .Include(c => c.Semestres)
                .FirstOrDefault(c => c.Id == id);

            if (carrera == null)
            {
                return NotFound();
            }

            ViewBag.Carrera = carrera;

            return View();
        }
    }
}
