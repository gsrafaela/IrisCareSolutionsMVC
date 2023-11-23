using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;

namespace IrisCareSolutions.Controllers
{
    public class ExameController : Controller
    {
        private ICSolutionsContext _context;

        //Recebe o DbContext por injeção de dependência
        public ExameController(ICSolutionsContext context)
        {
            _context = context;
        }

        // GET: Exame
        public async Task<IActionResult> Index()
        {
            var exames = await _context.Exames.Include(e => e.Tutelado).ToListAsync();
            return View(exames);
        }

        // GET: Exame/Create
        public IActionResult Create()
        {
            ViewData["TuteladoId"] = new SelectList(_context.Tutelados, "TuteladoId", "Nome");
            return View();
        }

        // POST: Exame/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExameId,Nome,Descricao,Data,TuteladoId,ResultadoFile")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                if (exame.ResultadoFile != null && exame.ResultadoFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await exame.ResultadoFile.CopyToAsync(memoryStream);
                        exame.ResultadoData = memoryStream.ToArray();
                    }
                }

                _context.Add(exame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TuteladoId"] = new SelectList(_context.Tutelados, "TuteladoId", "Nome", exame.TuteladoId);
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

            ViewData["TuteladoId"] = new SelectList(_context.Tutelados, "TuteladoId", "Nome", exame.TuteladoId);
            return View(exame);
        }

        // POST: Exame/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExameId,Nome,Descricao,Data,TuteladoId,ResultadoData")] Exame exame)
        {
            if (id != exame.ExameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingExame = await _context.Exames.FindAsync(id);

                    if (exame.ResultadoFile != null && exame.ResultadoFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await exame.ResultadoFile.CopyToAsync(memoryStream);
                            existingExame.ResultadoData = memoryStream.ToArray();
                        }
                    }

                    existingExame.Nome = exame.Nome;
                    existingExame.Descricao = exame.Descricao;
                    existingExame.Data = exame.Data;
                    existingExame.TuteladoId = exame.TuteladoId;

                    _context.Update(existingExame);
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

            ViewData["TuteladoId"] = new SelectList(_context.Tutelados, "TuteladoId", "Nome", exame.TuteladoId);
            return View(exame);
        }

        // GET: Exame/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames
                .Include(e => e.Tutelado)
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