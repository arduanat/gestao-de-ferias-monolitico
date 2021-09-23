using App.Models;
using App.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.Middlewares
{
    public class Cronometro
    {
        private readonly RequestDelegate next;
        private readonly LogService logService;
        public Cronometro(RequestDelegate next, LogService logService)
        {
            this.next = next;
            this.logService = logService;
        }

        public Task InvokeAsync(HttpContext contexto)
        {
            var cronometro = Stopwatch.StartNew();
            var tempoDeResposta = 0;

            contexto.Response.OnStarting(() => {
                cronometro.Stop();
                tempoDeResposta = (int)cronometro.ElapsedMilliseconds;
                return Task.CompletedTask;
            });

            var log = new 
            {
                Tempo = tempoDeResposta,
                Rota = contexto.Request.Path,
                Data = DateTime.Now.ToString()
            };

            logService.SalvarLog(log);

            return this.next(contexto);
        }
    }
}
