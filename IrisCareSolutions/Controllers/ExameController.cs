using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExameId,Nome,Descricao,Data,TuteladoId,ResultadoData")] Exame exame, IFormFile ResultadoFile)
        {
            if (ModelState.IsValid)
            {
                if (ResultadoFile != null && ResultadoFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(ResultadoFile.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ResultadoFile.CopyToAsync(fileStream);
                    }

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

            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, "application/octet-stream", $"{exame.Nome}_Resultado.pdf");
            }
            else
            {
                return NotFound();
            }
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
