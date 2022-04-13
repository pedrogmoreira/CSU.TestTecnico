using CSU.EtapaTecnica.Business.Services;
using CSU.EtapaTecnica.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CSU.EtapaTecnica.Business
{
    public static class Injector
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<INotaFiscalService, NotaFiscalService>();
        }
    }
}
