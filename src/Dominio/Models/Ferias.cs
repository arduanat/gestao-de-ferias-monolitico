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

        public int Id { get; private set; }
        public int ColaboradorId { get; private set; }
        public int AnoDeExercicio { get; private set; }
        public Colaborador Colaborador { get; private set; }
        public HomologacaoDeFerias Homologacao { get; private set; }
        public List<PeriodoDeFerias> PeriodosDeFerias { get; private set; }

        public void Homologar(string cpf, SituacaoDasFerias situacaoDasFerias)
        {
            Homologacao = new HomologacaoDeFerias(this.Id, cpf, situacaoDasFerias);
        }
    }
}