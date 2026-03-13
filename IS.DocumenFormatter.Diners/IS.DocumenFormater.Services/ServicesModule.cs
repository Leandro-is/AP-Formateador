using IS.DocumenFormater.Services.Exchange;
using Microsoft.Extensions.DependencyInjection;

namespace IS.DocumenFormater.Services
{
    public static class ServicesModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ITransaccionalDocumentFormaterService, TransaccionalDocumentFormaterService>();
            services.AddTransient<IEventLogService, EventLogService>();
        }
    }
}
