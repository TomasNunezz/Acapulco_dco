using Microsoft.EntityFrameworkCore;
using Acapulco.Models;

namespace Acapulco.Data
{
    public class AcapulcoDbContext : DbContext
    {
        // El constructor recibe las "opciones" de configuración (por ejemplo, la cadena de conexión a SQL Server)
        // y se las pasa a la clase base DbContext, que es la que hace todo el trabajo pesado por detrás
        public AcapulcoDbContext(DbContextOptions<AcapulcoDbContext> options) : base(options)
        {
        }

        // Cada "DbSet" representa una tabla en la base de datos
        // DbSet<Producto> = va a existir una tabla llamada "Productos"
        public DbSet<Producto> Productos { get; set; }

        // DbSet<Categoria> = va a existir una tabla llamada "Categorias"
        public DbSet<Categoria> Categorias { get; set; }
    }
}