using App.Models;
using Dominio.Context;
using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class FeriasController : Controller
    {
        private readonly Contexto contexto;

        public FeriasController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            var ferias = await contexto.Ferias
                                       .AsNoTracking()
                                       .Include(x => x.Colaborador)
                                       .Include(x => x.PeriodosDeFerias)
                                       .Include(x => x.Homologacao)
                                       .ToListAsync();

            return View(ferias);
        }

        public async Task<IActionResult> CadastrarFeriasParaMultiplosColaboradores(List<int> idsDosColaboradores, List<PeriodoDeFeriasViewModel> periodos)
        {
            var colaboradores = await contexto.Colaborador
                                              .Include(x => x.Ferias)
                                                  .ThenInclude(y => y.PeriodosDeFerias)
                                              .Where(x => idsDosColaboradores.Contains(x.Id))
                                              .ToListAsync();

            var periodosDeFerias = periodos.Select(x => new PeriodoDeFerias(x.DataInicial, x.QuantidadeDeDias, TipoDePeriodoDeFerias.FeriasRegulares)).ToList();

            foreach (var colaborador in colaboradores)
            {
                var colaboradorPossuiFeriasCadastradasParaOAnoDeExercicioAtual = colaborador.Ferias.Any(x => x.AnoDeExercicio == DateTime.Now.Year);

                if (!colaboradorPossuiFeriasCadastradasParaOAnoDeExercicioAtual)
                    colaborador.CadastrarFerias(periodosDeFerias);
            }

            contexto.UpdateRange(colaboradores);
            await contexto.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MapaDeFerias()
        {
            var ferias = await contexto.Ferias
                                       .AsNoTracking()
                                       .Include(x => x.Colaborador)
                                       .Include(x => x.PeriodosDeFerias)
                                       .Include(x => x.Homologacao)
                                       .ToListAsync();

            return View(ferias);
        }
    }
}