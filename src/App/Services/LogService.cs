using RestSharp;
using System;

namespace App.Services
{
    public class LogService
    {
        private readonly Uri uri;

        public LogService()
        {
            uri = new Uri("");
        }

        public void SalvarLog(object log)
        {
            var cliente = new RestClient(uri);

            var request = new RestRequest("/Log", Method.POST) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(log);

            var response = cliente.Execute(request);
        }
    }
}