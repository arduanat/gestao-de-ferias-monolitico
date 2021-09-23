using Microsoft.AspNetCore.Builder;

namespace App.Middlewares
{
    public static class ExtensaoCronometro
    {
        public static IApplicationBuilder UsarCronometro(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Cronometro>();
        }
    }
}
