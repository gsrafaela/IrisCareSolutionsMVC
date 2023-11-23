using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IrisCareSolutions.Controllers
{
    public class ExameController : Controller
    {
        private readonly ICSolutionsContext _context;

        // Recebe o DbContext por injeção de dependência
        public ExameController(ICSolutionsContext context)
        {
            _context = context;
        }

        // GET: Exame
        public async Task<IActionResult> Index()
        {
            var exames = await _context.Exames.ToListAsync();
            return View(exames);
        }

        // GET: Exame/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames
                .FirstOrDefaultAsync(m => m.ExameId == id);
            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        // GET: Exame/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExameId,Nome,Descricao,Data,TuteladoId,ResultadoFile")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                if (exame.ResultadoFile != null && exame.ResultadoFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(exame.ResultadoFile.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await exame.ResultadoFile.CopyToAsync(fileStream);
                    }

                    exame.ResultadoData = null; // Não precisamos mais dos dados no modelo
                    exame.ResultadoFileName = fileName;
                }

                _context.Add(exame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exame);
        }

        // GET: Exame/Download/5
        public IActionResult Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame = _context.Exames.Find(id);

            if (exame == null || string.IsNullOrEmpty(exame.ResultadoFileName))
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", exame.ResultadoFileName);

            return PhysicalFile(filePath, "application/octet-stream", $"{exame.Nome}_Resultado.pdf");
        }

        // GET: Exame/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames
                .FirstOrDefaultAsync(m => m.ExameId == id);
            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        // POST: Exame/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exame = await _context.Exames.FindAsync(id);

            if (exame != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", exame.ResultadoFileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.Exames.Remove(exame);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ExameExists(int id)
        {
            return _context.Exames.Any(e => e.ExameId == id);
        }
    }
}
