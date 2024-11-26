using Microsoft.AspNetCore.Mvc;
using GestionEducativa.Models;
using GestionEducativa.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionEducativa.Controllers
{
    public class MateriasControladora : Controller
    {
        private readonly ApplicationDbContext _context;

        public MateriasControladora(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Carreras = _context.Carreras
                .Include(carrera => carrera.Semestres)
                .ThenInclude(semestre => semestre.Materias)
                .ToList();

            return View();
        }
        public IActionResult Nueva()
        {
            ViewBag.Semestres = _context.Semestres
                .Include(semestre => semestre.Carrera)
                .ToList();

            return View();
        }

        public IActionResult Crear(string nombre, string descripcion, int semestreId)
        {
            Materia materia = new Materia(nombre, descripcion, semestreId);

            if (materia.EsValido() == false)
            {
                ViewBag.Error = "Los datos ingresados no son válidos";
                ViewBag.Nombre = nombre;
                ViewBag.Descripcion = descripcion;
                ViewBag.SemestreId = semestreId;

                ViewBag.Semestres = _context.Semestres
                    .Include(semestre => semestre.Carrera)
                    .ToList();
                return View("Nueva");
            }

            _context.Materias.Add(materia);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Materia materia = _context.Materias.Find(id);
            if (materia == null)
            {
                return NotFound();
            }
            _context.Materias.Remove(materia);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Editando(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Materia materia = _context.Materias.Find(id);
            if (materia == null)
            {
                return NotFound();
            }

            ViewBag.Id = materia.Id;
            ViewBag.Nombre = materia.Nombre;
            ViewBag.Descripcion = materia.Descripcion;
            ViewBag.SemestreId = materia.SemestreId;

            ViewBag.Semestres = _context.Semestres
                .Include(semestre => semestre.Carrera)
                .ToList();

            return View();
        }

        public IActionResult Editar(int id, string nombre, string descripcion, int semestreId)
        {
            Materia materia = _context.Materias.Find(id);
            if (materia == null)
            {
                return NotFound();
            }

            materia.Nombre = nombre;
            materia.Descripcion = descripcion;
            materia.SemestreId = semestreId;

            if (materia.EsValido() == false)
            {
                ViewBag.Error = "Los datos ingresados no son válidos";
                ViewBag.Nombre = nombre;
                ViewBag.Descripcion = descripcion;
                ViewBag.SemestreId = semestreId;
                ViewBag.Semestres = _context.Semestres
                    .Include(semestre => semestre.Carrera)
                    .ToList();
                return View("Editando");
            }

            _context.Materias.Update(materia);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Materia materia = _context.Materias
                .Include(materia => materia.Semestre)
                .ThenInclude(semestre => semestre.Carrera)
                .FirstOrDefault(materia => materia.Id == id);

            if (materia == null)
            {
                return NotFound();
            }

            ViewBag.Materia = materia;
            return View();
        }
    }
}
