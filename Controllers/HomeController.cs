using GestionEducativa.Data;
using GestionEducativa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GestionEducativa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Profesores = _context.Profesores;

            Materia? prog1 = _context.Materias
                .Where(materia => materia.Nombre.Contains("Programacion I"))
                .FirstOrDefault();

            Profesor? matias = _context.Profesores
                .Where(profesor => profesor.Nombre.Contains("Matias"))
                .FirstOrDefault();

            Dictado dictado = new Dictado();
            dictado.Ano = 2024;
            dictado.Instituto = "Colonia";
            dictado.Turno = "Matutino";
            dictado.Materia = prog1;
            dictado.Profesor = matias;

            _context.Dictados.Add(dictado);
            //_context.SaveChanges();

            ViewBag.Dictados = _context.Dictados.ToList();

            return View();
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
