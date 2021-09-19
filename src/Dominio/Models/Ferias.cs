using Dominio.ValueObjects.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Models
{
    public class Ferias
    {
        public Ferias(Colaborador colaborador, int anoDeExercicio, int id = 0)
        {
            Id = id;
            ColaboradorId = colaborador.Id;
            Colaborador = colaborador;
            AnoDeExercicio = anoDeExercicio;
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

        public void AdicionarPeriodos(List<PeriodoDeFerias> periodosDeFerias)
        {
            if (!JaPossuiQuantidadeLimiteDeDiasCadastrado() || !NovosPeriodosUltrapassamQuantidadeLimiteDeDiasCadastrado(periodosDeFerias))
            {
                foreach (var periodo in periodosDeFerias)
                {
                    var novoPeriodo = new PeriodoDeFerias(this, periodo.DataInicial, periodo.DataFinal, periodo.TipoDePeriodoDeFerias);
                    PeriodosDeFerias ??= new List<PeriodoDeFerias>();
                    PeriodosDeFerias.Add(novoPeriodo);
                }
            }
        }

        public void Homologar(string cpf, SituacaoDasFerias situacaoDasFerias)
        {
            Homologacao = new HomologacaoDeFerias(this.Id, cpf, situacaoDasFerias);
        }

        private bool JaPossuiQuantidadeLimiteDeDiasCadastrado()
        {
            return PeriodosDeFerias?.Sum(x => x.ObterQuantidadeDeDias()) == 30;
        }

        private bool NovosPeriodosUltrapassamQuantidadeLimiteDeDiasCadastrado(List<PeriodoDeFerias> periodosDeFerias)
        {
            return PeriodosDeFerias?.Sum(x => x.ObterQuantidadeDeDias()) + periodosDeFerias.Sum(x => x.ObterQuantidadeDeDias()) > 30;
        }
    }
}