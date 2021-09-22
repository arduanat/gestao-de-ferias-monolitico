using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dominio.Context;
using Dominio.Models;
using System.Collections.Generic;
using System;

namespace App.Controllers
{
    public class ColaboradorController : Controller
    {
        private readonly Contexto contexto;

        public ColaboradorController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            var colaboradores = await contexto.Colaborador.OrderBy(x => x.Nome).ToListAsync();
            return View(colaboradores);
        }

        public async Task<IActionResult> Criar(int quantidade = 0)
        {
            if(quantidade > 0)
            {
                await CriarMultiplos(quantidade);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                contexto.Add(colaborador);
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(colaborador);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
                return NotFound();

            var colaborador = await contexto.Colaborador.FindAsync(id);

            if (colaborador == null)
                return NotFound();

            return View(colaborador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Colaborador colaborador)
        {
            if (id != colaborador.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                contexto.Update(colaborador);
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(colaborador);
        }

        public async Task<IActionResult> Deletar(int id)
        {
            var colaborador = await contexto.Colaborador.FindAsync(id);

            if (colaborador == null)
                return NotFound();

            contexto.Colaborador.Remove(colaborador);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task CriarMultiplos(int quantidade)
        {
            List<Colaborador> novosColaboradores = new List<Colaborador>();

            for (int i = 0; i < quantidade; i++)
            {
                var matriculaAleatoria = $"1{new Random().Next(10000000, 99999999)}";

                var colaboradorAleatorio = new Colaborador(Guid.NewGuid().ToString(), matriculaAleatoria);
                novosColaboradores.Add(colaboradorAleatorio);
            }

            await contexto.AddRangeAsync(novosColaboradores);
            await contexto.SaveChangesAsync();
        }
    }
}