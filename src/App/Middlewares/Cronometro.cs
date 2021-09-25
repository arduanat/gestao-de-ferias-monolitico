using App.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.Middlewares
{
    public class Cronometro
    {
        private readonly RequestDelegate next;
        private readonly LogService logService;
        private readonly List<string> rotasDeLog;
        public Cronometro(RequestDelegate next, LogService logService)
        {
            this.next = next;
            this.logService = logService;
            rotasDeLog = ObterRotasDeLog();
        }

        public Task InvokeAsync(HttpContext contexto)
        
        {
            if (rotasDeLog.Contains(contexto.Request.Path))
            {
                var cronometro = Stopwatch.StartNew();
                var tempoDeResposta = 0;

                contexto.Response.OnStarting(() =>
                {
                    cronometro.Stop();
                    tempoDeResposta = (int)cronometro.ElapsedMilliseconds;

                    var log = new
                    {
                        Tempo = tempoDeResposta,
                        Rota = $"{contexto.Request.Host}{contexto.Request.Path}",
                        Data = DateTime.Now
                    };
                    logService.SalvarLog(log);

                    return Task.CompletedTask;
                });
            }
            return this.next(contexto);
        }

        private List<string> ObterRotasDeLog()
        {
            return new List<string>()
            {
                "/Ferias/Cadastrar",
                "/Ferias/Aprovar",
                "/Ferias/MapaDeFerias"
            };
        }
    }
}