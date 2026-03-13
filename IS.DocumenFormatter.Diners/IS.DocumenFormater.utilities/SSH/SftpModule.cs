using Microsoft.Extensions.DependencyInjection;

namespace IS.DocumenFormater.utilities.SSH
{
    public static class SftpModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ISftpServices, SftpServices>();
        }
    }
}
