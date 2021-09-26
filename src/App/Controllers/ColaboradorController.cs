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
            var colaboradores = await contexto.Colaborador.Include(x => x.Ferias).OrderBy(x => x.Nome).ToListAsync();
            return View(colaboradores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(int quantidade)
        {
            if (quantidade > 0)
            {
                await CriarMultiplosAleatoriamente(quantidade);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        private async Task CriarMultiplosAleatoriamente(int quantidade)
        {
            var novosColaboradores = new List<Colaborador>();

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