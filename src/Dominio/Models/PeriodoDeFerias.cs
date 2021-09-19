using Dominio.ValueObjects.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Models
{
    public class PeriodoDeFerias
    {
        public PeriodoDeFerias(Ferias ferias, DateTime dataInicial, DateTime dataFinal, TipoDePeriodoDeFerias tipoDePeriodoDeFerias, int id = 0)
        {
            Id = id;
            Ferias = ferias;
            FeriasId = ferias.Id;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            TipoDePeriodoDeFerias = tipoDePeriodoDeFerias;
        }

        public PeriodoDeFerias(DateTime dataInicial, int quantidadeDeDias, TipoDePeriodoDeFerias tipoDePeriodoDeFerias, int id = 0)
        {
            DataInicial = dataInicial;
            DataFinal = CalcularDataFinal(quantidadeDeDias);
            TipoDePeriodoDeFerias = tipoDePeriodoDeFerias;
        }

        public PeriodoDeFerias()
        {
        }

        public int Id { get; set; }
        public int FeriasId { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public TipoDePeriodoDeFerias TipoDePeriodoDeFerias { get; set; }
        public Ferias Ferias { get; set; }

        [NotMapped]
        public int QuantidadeDeDias
        {
            get { return (DataFinal - DataInicial).Days ; }
        }

        public bool QuantidadeDeDiasDeAbonoPecuniarioDiferenteDeDez()
        {
            return TipoDePeriodoDeFerias == TipoDePeriodoDeFerias.AbonoPecuniario && QuantidadeDeDias != 10;
        }

        public int ObterQuantidadeDeDias()
        {
            return (DataFinal - DataInicial).Days;
        }

        private DateTime CalcularDataFinal(int quantidadeDeDias)
        {
            var dataFinal = DataInicial.AddDays(quantidadeDeDias-1);
            return dataFinal;
        }
    }
}
