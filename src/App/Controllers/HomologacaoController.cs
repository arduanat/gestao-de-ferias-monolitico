using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Context;
using Dominio.Models;
using Dominio.ValueObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var feriasJahPossuiHomologacao = await PossuiHomologacao(id);

                if (!feriasJahPossuiHomologacao)
                {
                    var homologacao = new HomologacaoDeFerias(id, situacao);
                    homologacoes.Add(homologacao);
                }
            }

            await contexto.AddRangeAsync(homologacoes);
            await contexto.SaveChangesAsync();

            return RedirectToAction("MapaDeFerias", "Ferias");
        }

        private async Task<bool> PossuiHomologacao(int feriasId)
        {
            return await contexto.Ferias.AnyAsync(x => x.Id == feriasId && x.Homologacao != null);
        }
    }
}