using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersistentUnreal.Data;
using PersistentUnreal.Helpers;
using PersistentUnreal.Mediators;

namespace PersistentUnreal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PUAccountDbContext>(opt => opt.UseInMemoryDatabase("Account"));

            services.AddDbContext<ItemContext>(opt => opt.UseInMemoryDatabase("Item"));
            services.AddMvc();

            services.AddScoped<IPUAccountMediator, PUAccountMediator>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandling();

            app.UseMvc();
        }
    }
}
