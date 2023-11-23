﻿using IrisCareSolutions.Models;
using IrisCareSolutions.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IrisCareSolutions.Controllers
{
    public class TuteladoController : Controller
    {
        private ICSolutionsContext _context;

        //Recebe o DbContext por injeção de dependência
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
            var lembretesAssociados = _context.TuteladosLembretes
                .Where(p => p.TuteladoId == id)
                .Select(m => m.Lembrete)
                .ToList();

            ViewBag.medicamentos = lembretesAssociados;

            var todosLembretes = _context.Lembretes.ToList();

            var lembretesFiltrados = todosLembretes
                .Where(m => !lembretesAssociados.Contains(m));

            //Enviar o selectlist para preencher o select na tela
            ViewBag.select = new SelectList(lembretesFiltrados, "LembreteId", "Nome");

            //Pesquisar o paciente para enviar para view
            ViewBag.paciente = _context.Tutelados.Find(id);
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
            var tutelado = _context.Tutelados
                .Include(p => p.Endereco).First(p => p.TuteladoId == id);
            return View(tutelado);
        }

        //Criar o cadastro de paciente (Criar o método GET para abrir a página com o form HTML)
        //Criar uma página separada com o formulário HTML (cadastrar e no editar)
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
                .Include(p => p.Endereco)
                .ToList();
            return View(lista);
        }
    }
}