using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

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
            // Enviar a lista de exames do tutelado para a view
            ViewBag.Exames = _context.Exames
                .Where(e => e.TuteladoId == id)
                .ToList();

            return View();
        }

        // GET: Exames/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ExameId, Nome, Descricao, Data, TuteladoId, ResultadoFileName")] Exame exame, IFormFile resultadoFile)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (resultadoFile != null && resultadoFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + resultadoFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        resultadoFile.CopyTo(stream);
                    }

                    // Save the file path in the database
                    exame.ResultadoPath = "/uploads/" + uniqueFileName;
                }

                _context.Exames.Add(exame);
                _context.SaveChanges();

                TempData["msg"] = "Pedido Enviado!";
                return RedirectToAction("Index", new { id = exame.TuteladoId });
            }

            // If the model is not valid, return to the view with errors
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
                return RedirectToAction("Index", new { id = exame.TuteladoId });
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
