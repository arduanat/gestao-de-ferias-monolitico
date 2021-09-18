using System.Collections.Generic;

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
    }
}