using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Session;
namespace storexweb_task
{




    public class Startup
    {


        public IConfiguration Configuration { get; }
        public string enUSCulture { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // The next line to connectt the AppSettingsModel class in the core with the ApplicationSettings file in the front end.
            services.Configure<StorexWebCore.AppSettingsModel>(Configuration.GetSection("ApplicationSettings"));
            services.AddOptions();
            services.AddAntiforgery();
            services.AddMvc();
            //In-Memory
            services.AddDistributedMemoryCache();
            services.AddSession();
            // Add framework services.

            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";
            });

            services.Configure<CookieAuthenticationOptions>(options =>
            {
                options.AccessDeniedPath = new PathString("/NotAuthorized/Index");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var supportedCultures = new[]
            {
                new CultureInfo(enUSCulture),
                new CultureInfo("ar-SA")
            };

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //  app.UseHsts();
            }




            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });






            //  app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }


}
