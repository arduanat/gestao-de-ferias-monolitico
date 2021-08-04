using Dominio.ValueObjects.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Models
{
    public class PeriodoDeFerias
    {
        public PeriodoDeFerias(int feriasId, DateTime dataInicial, DateTime dataFinal, TipoDePeriodoDeFerias tipoDePeriodoDeFerias, int id = 0)
        {
            Id = id;
            FeriasId = feriasId;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            TipoDePeriodoDeFerias = tipoDePeriodoDeFerias;
        }

        public int Id { get; private set; }
        public int FeriasId { get; private set; }
        public DateTime DataInicial { get; private set; }
        public DateTime DataFinal { get; private set; }
        public TipoDePeriodoDeFerias TipoDePeriodoDeFerias { get; private set; }
        public Ferias Ferias { get; private set; }

        [NotMapped]
        public int QuantidadeDeDias
        {
            get { return (DataFinal - DataInicial).Days ; }
        }

        public bool QuantidadeDeDiasDeAbonoPecuniarioDiferenteDeDez()
        {
            return TipoDePeriodoDeFerias == TipoDePeriodoDeFerias.AbonoPecuniario && QuantidadeDeDias != 10;
        }
    }
}
