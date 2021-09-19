using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Models
{
    public class Colaborador
    {
        public Colaborador(string nome, string matricula, int id = 0)
        {
            Id = id;
            Nome = nome;
            Matricula = matricula;
        }

        public Colaborador()
        {
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public List<Ferias> Ferias { get; set; }

        public void CadastrarFerias(List<PeriodoDeFerias> periodosDeFerias)
        {
            if (Ferias.Any(x => x.AnoDeExercicio == DateTime.Now.Year))
            {
                Ferias.First(x => x.AnoDeExercicio == DateTime.Now.Year).AdicionarPeriodos(periodosDeFerias);
            }
            else
            {
                var feriasDoColaborador = new Ferias(this, DateTime.Now.Year);
                feriasDoColaborador.AdicionarPeriodos(periodosDeFerias);
                Ferias.Add(feriasDoColaborador);
            }

        }
    }
}