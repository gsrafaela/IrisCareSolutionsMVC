using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;
using Microsoft.AspNetCore.Mvc;

namespace IrisCareSolutions.Controllers
{
    public class LembreteController : Controller
    {

            private ICSolutionsContext _context;

            //Recebe o DbContext por injeção de dependência
            public LembreteController(ICSolutionsContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
                return View(_context.Lembretes.ToList());
            }

            [HttpGet]
            public IActionResult Cadastrar()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Cadastrar(Lembrete lembrete)
            {
                _context.Lembretes.Add(lembrete);
                _context.SaveChanges();
                TempData["msg"] = "Lembrete registrado";
                return RedirectToAction("Cadastrar");
            }
    }
}
