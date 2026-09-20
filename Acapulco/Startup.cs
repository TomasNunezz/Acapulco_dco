namespace Acapulco
{
    public class Startup
    {
        // El constructor recibe la "Configuration" (o sea, todo lo que está en appsettings.json)
        // La guardamos en una propiedad para poder usarla más adelante (por ejemplo, cuando conectemos la base de datos)
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Acá se "registran" los servicios que la aplicación va a poder usar en cualquier parte del código
        // Por ahora solo tiene MVC (controladores + vistas). Más adelante acá vamos a sumar Entity Framework, Identity, etc.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // Acá se arma el "pipeline": el orden en que la app procesa cada pedido que llega (una petición HTTP)
        // El orden importa: por ejemplo, primero hay que redirigir a HTTPS, después servir archivos estáticos, etc.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}