using Microsoft.Extensions.DependencyInjection;

namespace IS.DocumenFormater.utilities.Email
{
    public static class EmailModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
