using IS.DocumenFormater.api.ContractFormats;
using IS.DocumenFormater.api.Factories;
using IS.DocumenFormater.Repository;
using IS.DocumenFormater.Services;
using IS.DocumenFormater.utilities.pdf;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IS.DocumenFormater.api
{
    public class Startup
    {
        private IHostingEnvironment _environment { get; }
        private IConfiguration _configuration { get; }

        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            RepositoryModule.Register(services, _configuration.GetConnectionString(Constants.ConnectionStringName), GetType().Assembly.FullName);
            ServicesModule.Register(services);
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IPdfFormats>(sp => { return _configuration.GetSection("PDFFormats").Get<PdfFormats>(); });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }*/
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            RotativaConfiguration.Setup(env);
        }
    }
}
