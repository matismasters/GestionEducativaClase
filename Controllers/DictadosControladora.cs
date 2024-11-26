using GestionEducativa.Data;
using GestionEducativa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionEducativa.Controllers
{
    public class DictadosControladora : Controller
    {
        private readonly ApplicationDbContext _context;

        public DictadosControladora(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Dictado> dictados = _context.Dictados
                .Include(dictado => dictado.Materia)
                .Include(dictado => dictado.Profesor)
                .ToList();

            ViewBag.Dictados = dictados;

            return View();
        }

        public IActionResult Nuevo()
        {
            string[] institutos = new string[2];
            institutos[0] = "rosario";
            institutos[1] = "colonia";

            ViewBag.Institutos = institutos;
            ViewBag.Profesores = _context.Profesores.ToList();
            ViewBag.Materias = _context.Materias.ToList();

            return View();
        }

        public IActionResult Crear(int ano, string turno, string instituto, int profesorId, int materiaId)
        {
            Dictado dictado = new Dictado(
                ano,
                turno,
                instituto,
                profesorId,
                materiaId
            );

            if (dictado.EsValido() == false)
            {
                string[] institutos = new string[2];
                institutos[0] = "rosario";
                institutos[1] = "colonia";

                ViewBag.Institutos = institutos;
                ViewBag.Profesores = _context.Profesores.ToList();
                ViewBag.Materias = _context.Materias.ToList();

                ViewBag.Error = "Los datos ingresados no son válidos";
                ViewBag.Ano = ano;
                ViewBag.Turno = turno;
                ViewBag.Instituto = instituto;
                ViewBag.ProfesorId = profesorId;
                ViewBag.MateriaId = materiaId;

                return View("Nuevo");
            }

            _context.Dictados.Add(dictado);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dictado dictado = _context.Dictados.Find(id);

            if (dictado == null)
            {
                return NotFound();
            }

            _context.Dictados.Remove(dictado);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Editando(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dictado dictado = _context.Dictados.Find(id);

            if (dictado == null)
            {
                return NotFound();
            }

            string[] institutos = new string[2];
            institutos[0] = "rosario";
            institutos[1] = "colonia";

            ViewBag.Institutos = institutos;
            ViewBag.Profesores = _context.Profesores.ToList();
            ViewBag.Materias = _context.Materias
                .Include(materia => materia.Semestre)
                .ThenInclude(semestre => semestre.Carrera)
                .ToList();

            ViewBag.Id = id;
            ViewBag.Ano = dictado.Ano;
            ViewBag.Turno = dictado.Turno;
            ViewBag.Instituto = dictado.Instituto;
            ViewBag.ProfesorId = dictado.ProfesorId;
            ViewBag.MateriaId = dictado.MateriaId;

            return View("Editar");
        }

        public IActionResult Editar(int? id, int ano, string turno, string instituto, int materiaId, int profesorId)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dictado dictado = _context.Dictados.Find(id);

            if (dictado == null)
            {
                return NotFound();
            }

            dictado.Ano = ano;
            dictado.Turno = turno;
            dictado.Instituto = instituto;
            dictado.MateriaId = materiaId;
            dictado.ProfesorId = profesorId;

            if (dictado.EsValido() == false)
            {

                string[] institutos = new string[2];
                institutos[0] = "rosario";
                institutos[1] = "colonia";

                ViewBag.Institutos = institutos;
                ViewBag.Profesores = _context.Profesores.ToList();
                ViewBag.Materias = _context.Materias
                    .Include(materia => materia.Semestre)
                    .ThenInclude(semestre => semestre.Carrera)
                    .ToList();

                ViewBag.Id = dictado.Id;
                ViewBag.Ano = dictado.Ano;
                ViewBag.Turno = dictado.Turno;
                ViewBag.Instituto = dictado.Instituto;
                ViewBag.ProfesorId = dictado.ProfesorId;
                ViewBag.MateriaId = dictado.MateriaId;

                return View();
            }

            _context.Dictados.Update(dictado);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
