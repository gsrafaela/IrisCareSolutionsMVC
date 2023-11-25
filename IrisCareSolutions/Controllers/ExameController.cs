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

        // GET: Exame/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exame/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Descricao, Data, ResultadoFile")] Exame exame, IFormFile ResultadoFile)
        {
            if (ModelState.IsValid)
            {
                if (ResultadoFile != null && ResultadoFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        ResultadoFile.CopyTo(ms);
                        exame.ResultadoData = ms.ToArray();
                        exame.ResultadoFileName = ResultadoFile.FileName;
                    }
                }

                _context.Add(exame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exame);
        }

        // GET: Exame/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames.FindAsync(id);
            if (exame == null)
            {
                return NotFound();
            }
            return View(exame);
        }

        // POST: Exame/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExameId,Nome,Descricao,Data,ResultadoData,ResultadoFileName")] Exame exame)
        {
            if (id != exame.ExameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExameExists(exame.ExameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exame);
        }

        // GET: Exame/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames.FirstOrDefaultAsync(m => m.ExameId == id);

            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        // GET: Exame/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames.FirstOrDefaultAsync(m => m.ExameId == id);

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
            _context.Exames.Remove(exame);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExameExists(int id)
        {
            return _context.Exames.Any(e => e.ExameId == id);
        }
    }
}