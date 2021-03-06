
using Dominio.ValueObjects.Enums;

namespace Dominio.Models
{
    public class HomologacaoDeFerias
    {
        public HomologacaoDeFerias(int feriasId, SituacaoDasFerias situacaoDasFerias, int id = 0)
        {
            Id = id;
            FeriasId = feriasId;
            SituacaoDasFerias = situacaoDasFerias;
        }

        public HomologacaoDeFerias()
        {
        }

        public int Id { get; private set; }
        public int FeriasId { get; private set; }
        public SituacaoDasFerias SituacaoDasFerias { get; private set; }
        public Ferias Ferias { get; private set; }
    }
}