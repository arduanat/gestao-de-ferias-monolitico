using Dominio.ValueObjects.Enums;
using System.Collections.Generic;

namespace Dominio.Models
{
    public class Ferias
    {
        public Ferias(int colaboradorId, int anoDeExercicio, List<PeriodoDeFerias> periodosDeFerias, int id = 0)
        {
            Id = id;
            ColaboradorId = colaboradorId;
            AnoDeExercicio = anoDeExercicio;
            PeriodosDeFerias = periodosDeFerias;
        }

        public Ferias()
        {
        }

        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public int AnoDeExercicio { get; set; }
        public Colaborador Colaborador { get; set; }
        public HomologacaoDeFerias Homologacao { get; set; }
        public List<PeriodoDeFerias> PeriodosDeFerias { get; set; }

        public void Homologar(string cpf, SituacaoDasFerias situacaoDasFerias)
        {
            Homologacao = new HomologacaoDeFerias(this.Id, cpf, situacaoDasFerias);
        }
    }
}