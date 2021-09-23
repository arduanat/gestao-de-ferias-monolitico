using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Context;
using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class HomologacaoController : Controller
    {
        private readonly Contexto contexto;

        public HomologacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IActionResult> AprovarOuReprovarMultiplasFerias(List<int> feriasIds, SituacaoDasFerias situacao)
        {
            var homologacoes = new List<HomologacaoDeFerias>();
            foreach (var id in feriasIds)
            {
                var homologacao = new HomologacaoDeFerias(id, situacao);
                homologacoes.Add(homologacao);
            }

            await contexto.AddRangeAsync(homologacoes);
            await contexto.SaveChangesAsync();

            return RedirectToAction("MapaDeFerias", "Ferias");
        }
    }
}
