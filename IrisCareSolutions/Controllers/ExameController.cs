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
        public async Task<IActionResult> CreateAsync([Bind("ExameId,Nome,Descricao,Data,TuteladoId,ResultadoData")] Exame exame, IFormFile ResultadoFile)
        {
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    // Handle exceptions (log or display an error message)
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(int id)
        {
            var exame = await _context.Exames.FindAsync(id);

            if (exame != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", exame.ResultadoFileName);
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (log or display an error message)
                        ModelState.AddModelError(string.Empty, "An error occurred while deleting the file.");
                    }
                }

                _context.Exames.Remove(exame);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
