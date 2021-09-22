using App.Models;
using Dominio.Context;
using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> CriarParaMultiplosColaboradores(List<int> idsDosColaboradores, List<PeriodoDeFeriasViewModel> periodos)
        {
            List<Ferias> feriasDosColaboradores = new List<Ferias>();

            var colaboradores = await contexto.Colaborador
                                              .Include(x => x.Ferias)
                                                  .ThenInclude(y => y.PeriodosDeFerias)
                                              .Where(x => idsDosColaboradores.Contains(x.Id))
                                              .ToListAsync();

            var periodosDeFerias = periodos.Select(x => new PeriodoDeFerias(x.DataInicial, x.QuantidadeDeDias, TipoDePeriodoDeFerias.FeriasRegulares)).ToList();

            foreach (var colaborador in colaboradores)
            {
                colaborador.CadastrarFerias(periodosDeFerias);
            }

            contexto.UpdateRange(colaboradores);
            await contexto.SaveChangesAsync();

            return RedirectToAction("Index", "Colaborador");
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

        public async Task<IActionResult> HomologarMultiplasFerias(List<int> feriasIds, SituacaoDasFerias situacao)
        {
            var homologacoes = new List<HomologacaoDeFerias>();
            foreach (var id in feriasIds)
            {
                var homologacao = new HomologacaoDeFerias(id, situacao);
                homologacoes.Add(homologacao);
            }

            await contexto.AddRangeAsync(homologacoes);
            await contexto.SaveChangesAsync();

            return RedirectToAction(nameof(MapaDeFerias));
        }
    }
}