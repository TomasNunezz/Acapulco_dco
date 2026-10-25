using Microsoft.AspNetCore.Identity;

namespace Acapulco.Data
{
    public static class IdentitySeeder
    {
        // Datos fijos del administrador (los que pediste: usuario y clave precargados)
        private const string AdminEmail = "admin@acapulco.com";
        private const string AdminPassword = "Admin123!";

        public static async Task InicializarAsync(IServiceProvider servicios)
        {
            var roleManager = servicios.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = servicios.GetRequiredService<UserManager<IdentityUser>>();

            // 1) Crear los roles si no existen
            foreach (var rol in new[] { "Admin", "Cliente" })
            {
                if (!await roleManager.RoleExistsAsync(rol))
                {
                    await roleManager.CreateAsync(new IdentityRole(rol));
                }
            }

            // 2) Crear el usuario Admin si no existe
            var admin = await userManager.FindByEmailAsync(AdminEmail);
            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = AdminEmail,
                    Email = AdminEmail,
                    EmailConfirmed = true
                };

                var resultado = await userManager.CreateAsync(admin, AdminPassword);
                if (resultado.Succeeded)
                {
                    // 3) Darle el rol Admin
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}