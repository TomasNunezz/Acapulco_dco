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
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Precisión explícita del precio (esto también elimina el warning de "decimal" que vimos)
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Sillas" },
                new Categoria { Id = 2, Nombre = "Mesas" },
                new Categoria { Id = 3, Nombre = "Sillones" }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Silla reclinable", Descripcion = "Silla reclinable tapizada. Medidas: 60 x 70 x 105 cm.", Precio = 85000m, Stock = 10, ImagenUrl = "https://placehold.co/400x300?text=Silla+reclinable", CategoriaId = 1 },
                new Producto { Id = 2, Nombre = "Silla de comedor", Descripcion = "Silla de madera maciza. Medidas: 45 x 50 x 90 cm.", Precio = 42000m, Stock = 24, ImagenUrl = "https://placehold.co/400x300?text=Silla+de+comedor", CategoriaId = 1 },
                new Producto { Id = 3, Nombre = "Mesa de madera de abeto", Descripcion = "Mesa de comedor para 6 personas. Medidas: 160 x 90 x 75 cm.", Precio = 210000m, Stock = 5, ImagenUrl = "https://placehold.co/400x300?text=Mesa+de+abeto", CategoriaId = 2 },
                new Producto { Id = 4, Nombre = "Mesa ratona", Descripcion = "Mesa ratona de living. Medidas: 90 x 50 x 40 cm.", Precio = 68000m, Stock = 8, ImagenUrl = "https://placehold.co/400x300?text=Mesa+ratona", CategoriaId = 2 },
                new Producto { Id = 5, Nombre = "Sillón de tres cuerpos", Descripcion = "Sillón de tres cuerpos en tela gris. Medidas: 210 x 90 x 85 cm.", Precio = 450000m, Stock = 3, ImagenUrl = "https://placehold.co/400x300?text=Sillon+3+cuerpos", CategoriaId = 3 }
            );
        }
    }
}
