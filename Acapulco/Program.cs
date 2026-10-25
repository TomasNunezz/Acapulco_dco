using Acapulco.Data;


namespace Acapulco
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Antes de arrancar, creamos los roles y el usuario Admin (si no existen)
            using (var scope = host.Services.CreateScope())
            {
                await IdentitySeeder.InicializarAsync(scope.ServiceProvider);
            }

            host.Run();
        }

        // Acá le decimos a la aplicación: "para arrancar, usá la clase Startup"
        // Es el punto de conexión entre este archivo y Startup.cs
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}