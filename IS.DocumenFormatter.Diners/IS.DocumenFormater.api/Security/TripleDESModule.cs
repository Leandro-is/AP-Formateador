using Microsoft.Extensions.DependencyInjection;

namespace IS.DocumenFormater.api.Security
{
    public static class TripleDESModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ITripleDESServices, TripleDESServices>();
        }
    }
}
