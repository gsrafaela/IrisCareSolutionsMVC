using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IrisCareSolutions.Controllers
{
    public class ExameController : Controller
    {
        private readonly ICSolutionsContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExameController(ICSolutionsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Exames
        public IActionResult Index(int id)
        {
            //Enviar a lista de exames do tutelado para a view
            ViewBag.exames = _context.Exames
                .Where(e => e.TuteladoId == id).ToList();

            return View();
        }

        // GET: Exames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exame/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ExameId, Nome, Descricao, Data, TuteladoId, ResultadoFile")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (exame.ResultadoFile != null && exame.ResultadoFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + exame.ResultadoFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        exame.ResultadoFile.CopyTo(stream);
                    }

                    // Save the file path in the database
                    exame.ResultadoPath = "/uploads/" + uniqueFileName;
                }

                _context.Exames.Add(exame);
                _context.SaveChanges();

                TempData["msg"] = "Exame agendado!";
                return RedirectToAction("Exames", "Tutelado", new { id = exame.TuteladoId });
            }

            // Se o modelo não for válido, retorne para a view com os erros
            return View(exame);
        }


        // GET: Exames/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var exame = _context.Exames.Find(id);

            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        // POST: Exames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ExameId, Nome, Descricao, Data, TuteladoId")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(exame).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exame);
        }

        // GET: Exames/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var exame = _context.Exames.Find(id);

            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }
    }
}