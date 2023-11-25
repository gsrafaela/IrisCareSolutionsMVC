using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            ViewBag.tutelado = _context.Tutelados.Find(id);
            return View();
        }

        [HttpPost]
        public IActionResult adicionar(TuteladoLembrete tuteladoLembrete)
        {
            _context.TuteladosLembretes.Add(tuteladoLembrete);
            _context.SaveChanges();
            TempData["msg"] = "Lembrete associado";
            return RedirectToAction("Lembretes", new { id = tuteladoLembrete.TuteladoId });
        }

        [HttpGet]
        public IActionResult Lembretes(int id)
        {
            var lembretesAssociados = _context.TuteladosLembretes
                .Where(p => p.TuteladoId == id)
                .Select(m => m.Lembrete)
                .ToList();

            ViewBag.lembretes = lembretesAssociados;

            var todosLembretes = _context.Lembretes.ToList();

            var lembretesFiltrados = todosLembretes
                .Where(m => !lembretesAssociados.Contains(m));

            ViewBag.select = new SelectList(lembretesFiltrados, "LembreteId", "Nome");

            ViewBag.tutelado = _context.Tutelados.Find(id);

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
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Tutelado tutelado)
        {
            //Cadastrar no banco de dados
            _context.Tutelados.Add(tutelado);
            _context.SaveChanges();
            //Mensagem de sucesso
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