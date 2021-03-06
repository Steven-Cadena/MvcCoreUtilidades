using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCoreUtilidades.Helpers;
using MvcCoreUtilidades.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreUtilidades
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*HABILITAR CACHE + NUGET INSTALAR*/
            services.AddResponseCaching();
            /*HABILITAR LA MEMORIA DE CACHE*/
            services.AddMemoryCache();
            /* PARA LOS FICHEROS GUARDAR EN DIFERENTES RUTAS*******IMPORTANTE*/
            services.AddSingleton<PathProvider>();
            /*CONFIGURACION DEL CORREO EMAIL DE LA EMPRESA*/
            services.AddSingleton<IConfiguration>(this.Configuration);
            /*AGREGAR LOS HELPERS PARA QUE FUNCIONEN LOS METODOS de dentro*/
            services.AddSingleton<HelperMail>();
            services.AddSingleton<HelperUploadFiles>();
            /*********************************************************/

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            /***********************************IMPORTANTE PARA EL CACHE*/
            app.UseResponseCaching();
            /************************/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
