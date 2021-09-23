using App.Models;
using Dominio.Context;
using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public FeriasController(Contexto contexto, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
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
            var log = new Log(httpContextAccessor.HttpContext.Request.Path.ToString());

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

            log.FinalizarContagem();

            return RedirectToAction("Index", "Colaborador");
        }

        public async Task<IActionResult> MapaDeFerias()
        {
            var log = new Log(httpContextAccessor.HttpContext.Request.Path.ToString());
            
            var ferias = await contexto.Ferias
                                       .AsNoTracking()
                                       .Include(x => x.Colaborador)
                                       .Include(x => x.PeriodosDeFerias)
                                       .Include(x => x.Homologacao)
                                       .ToListAsync();

            log.FinalizarContagem();

            return View(ferias);
        }
    }
}