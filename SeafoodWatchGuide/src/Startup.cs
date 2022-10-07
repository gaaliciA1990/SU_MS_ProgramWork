using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ContosoCrafts.WebSite.RatingEnums;

namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// This is the class that enables startup for the website
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor for the startup process
        /// </summary>
        ///  <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Declares a getter for the Config on startup 
        /// </summary>
        public IConfiguration Configuration 
        { 
            get; 
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        ///  <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();
            services.AddTransient<JsonFileProductService>();
            services.AddTransient<JsonFileFeedbackService>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        ///  <param name="app"></param>
        ///  <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();

                // endpoints.MapGet("/products", (context) => 
                // {
                //     var products = app.ApplicationServices.GetService<JsonFileProductService>().GetProducts();
                //     var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
                //     return context.Response.WriteAsync(json);
                // });
            });
        }
    }
}