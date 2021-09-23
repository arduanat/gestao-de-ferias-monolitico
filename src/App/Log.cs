using System;

namespace App
{
    public class Log
    {
        public Log(string rota)
        {
            Rota = rota;
            IniciarContagem();
        }

        public long Tempo { get; private set; }
        public string Rota { get; private set; }

        private void IniciarContagem()
        {
            Tempo = DateTime.Now.Ticks;
        }

        public void FinalizarContagem()
        {
            Tempo = DateTime.Now.Ticks - Tempo;
        }
    }
}