using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kundeinformasjonstjenester.DataAccessLayer.ScreenHub;
using Kundeinformasjonstjenester.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kundeinformasjonstjenester.SignalR
{
    public class Startup
    {

        private IScreenConnectionRepository repository = new ScreenConnectionRepository();
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<IScreenConnectionRepository>(repository);
            services.AddSignalR(options => {
                options.EnableDetailedErrors = true;
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                           name: "default",
                           pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<ScreenHub>("/screen");
            });
        }
    }
}
