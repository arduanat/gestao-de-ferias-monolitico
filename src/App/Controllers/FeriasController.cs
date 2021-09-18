using App.Models;
using Dominio.Context;
using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> CriarParaMultiplosColaboradores(List<int> colaboradores, List<PeriodoDeFeriasViewModel> periodos)
        {
            List<Ferias> feriasDosColaboradores = new List<Ferias>();
            foreach (var id in colaboradores)
            {
                var periodosDeFerias = periodos.Select(x => new PeriodoDeFerias(x.DataInicial, x.QuantidadeDeDias, TipoDePeriodoDeFerias.FeriasRegulares)).ToList();
                var feriasDoColaborador = new Ferias(id, DateTime.Now.Year, periodosDeFerias);
                feriasDosColaboradores.Add(feriasDoColaborador);
            }

            await contexto.AddRangeAsync(feriasDosColaboradores);
            await contexto.SaveChangesAsync();

            return RedirectToAction("", "Colaborador");
        }
    }
}
