using IS.DocumenFormater.Repository.Exchange;
using Microsoft.Extensions.DependencyInjection;

namespace IS.DocumenFormater.Repository
{
    public static class RepositoryModule
    {
        public static void Register(IServiceCollection services, string connection, string migrationsAssembly)
        {
            services.AddTransient<ITransaccionalDocumentFormaterRepository, TransaccionalDocumentFormaterRepository>();
            services.AddTransient<IEventLogRepository, EventLogRepository>();
        }
    }
}
