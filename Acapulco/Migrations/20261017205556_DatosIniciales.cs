using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acapulco.Migrations
{
    /// <inheritdoc />
    public partial class DatosIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Sillas" },
                    { 2, "Mesas" },
                    { 3, "Sillones" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CategoriaId", "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Silla reclinable tapizada. Medidas: 60 x 70 x 105 cm.", "https://placehold.co/400x300?text=Silla+reclinable", "Silla reclinable", 85000m, 10 },
                    { 2, 1, "Silla de madera maciza. Medidas: 45 x 50 x 90 cm.", "https://placehold.co/400x300?text=Silla+de+comedor", "Silla de comedor", 42000m, 24 },
                    { 3, 2, "Mesa de comedor para 6 personas. Medidas: 160 x 90 x 75 cm.", "https://placehold.co/400x300?text=Mesa+de+abeto", "Mesa de madera de abeto", 210000m, 5 },
                    { 4, 2, "Mesa ratona de living. Medidas: 90 x 50 x 40 cm.", "https://placehold.co/400x300?text=Mesa+ratona", "Mesa ratona", 68000m, 8 },
                    { 5, 3, "Sillón de tres cuerpos en tela gris. Medidas: 210 x 90 x 85 cm.", "https://placehold.co/400x300?text=Sillon+3+cuerpos", "Sillón de tres cuerpos", 450000m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
