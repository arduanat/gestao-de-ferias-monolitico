using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Context;
using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class HomologacaoController : Controller
    {
        private readonly Contexto contexto;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HomologacaoController(Contexto contexto, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> AprovarOuReprovarMultiplasFerias(List<int> feriasIds, SituacaoDasFerias situacao)
        {
            var log = new Log(httpContextAccessor.HttpContext.Request.Path.ToString());

            var homologacoes = new List<HomologacaoDeFerias>();
            foreach (var id in feriasIds)
            {
                var homologacao = new HomologacaoDeFerias(id, situacao);
                homologacoes.Add(homologacao);
            }

            await contexto.AddRangeAsync(homologacoes);
            await contexto.SaveChangesAsync();

            log.FinalizarContagem();

            return RedirectToAction("MapaDeFerias", "Ferias");
        }
    }
}
