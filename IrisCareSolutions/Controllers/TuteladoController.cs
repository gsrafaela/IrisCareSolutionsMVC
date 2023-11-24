using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IrisCareSolutions.Controllers
{
    public class TuteladoController : Controller
    {
        private ICSolutionsContext _context;

        // Recebe o DbContext por injeção de dependência
        public TuteladoController(ICSolutionsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Agendar(Exame exame)
        {
            _context.Exames.Add(exame);
            _context.SaveChanges();
            TempData["msg"] = "Exame agendado!";
            return RedirectToAction("Exames", new { id = exame.TuteladoId });
        }

        [HttpGet]
        public IActionResult Exames(int id)
        {
            ViewBag.exames = _context.Exames
                .Where(e => e.TuteladoId == id).ToList();

            ViewBag.paciente = _context.Tutelados.Find(id);
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(TuteladoLembrete tuteladoLembrete)
        {
            _context.TuteladosLembretes.Add(tuteladoLembrete);
            _context.SaveChanges();
            TempData["msg"] = "Lembrete Associado";
            return RedirectToAction("Lembretes", new { id = tuteladoLembrete.TuteladoId });
        }

        [HttpGet]
        public IActionResult Lembretes(int id)
        {
            // ... (unchanged)

            return View();
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            var tutelado = _context.Tutelados.Find(id);
            _context.Tutelados.Remove(tutelado);
            _context.SaveChanges();
            TempData["msg"] = "Tutelado removido!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Editar(Tutelado tutelado)
        {
            _context.Tutelados.Update(tutelado);
            _context.SaveChanges();
            TempData["msg"] = "Tutelado atualizado";
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Remove the inclusion of the address
            var tutelado = _context.Tutelados.First(p => p.TuteladoId == id);
            return View(tutelado);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Tutelado tutelado)
        {
            _context.Tutelados.Add(tutelado);
            _context.SaveChanges();
            TempData["msg"] = "Tutelado cadastrado!";
            return RedirectToAction("cadastrar");
        }

        public IActionResult Index(string filtro = "")
        {
            var lista = _context.Tutelados
                .Where(p => p.Nome.Contains(filtro) || string.IsNullOrEmpty(filtro))
                .ToList();
            return View(lista);
        }
    }
}
