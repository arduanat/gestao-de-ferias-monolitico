
using Dominio.ValueObjects.Enums;

namespace Dominio.Models
{
    public class HomologacaoDeFerias
    {
        public HomologacaoDeFerias(int feriasId, string cpfDoHomologador, SituacaoDasFerias situacaoDasFerias, int id = 0)
        {
            Id = id;
            FeriasId = feriasId;
            CpfDoHomologador = cpfDoHomologador;
            SituacaoDasFerias = situacaoDasFerias;
        }

        public HomologacaoDeFerias()
        {
        }
        public int Id { get; private set; }
        public int FeriasId { get; private set; }
        public string CpfDoHomologador { get; set; }
        public SituacaoDasFerias SituacaoDasFerias { get; private set; }
        public Ferias Ferias { get; private set; }
    }
}
