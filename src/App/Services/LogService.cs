using RestSharp;
using System;

namespace App.Services
{
    public class LogService
    {
        private readonly Uri uri;

        public LogService()
        {
            uri = new Uri("https://gestao-de-ferias-log.azurewebsites.net");
        }

        public void SalvarLog(object log)
        {
            var cliente = new RestClient(uri);

            var request = new RestRequest("/Log", Method.POST) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(log);

            cliente.Execute(request);
        }
    }
}
