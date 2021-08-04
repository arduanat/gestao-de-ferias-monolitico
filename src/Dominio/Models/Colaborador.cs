using System.Collections.Generic;

namespace Dominio.Models
{
    public class Colaborador
    {
        public Colaborador(int id, string nome, string matricula)
        {
            Id = id;
            Nome = nome;
            Matricula = matricula;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Matricula { get; private set; }
        public List<Ferias> Ferias { get; set; }
    }
}