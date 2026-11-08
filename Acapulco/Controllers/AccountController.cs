using Acapulco.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Acapulco.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Muestra el formulario de registro vacío (cuando entrás a /Account/Registro)
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // Recibe el formulario cuando el usuario toca "Registrarme"
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = new IdentityUser { UserName = modelo.Email, Email = modelo.Email };
            var resultado = await _userManager.CreateAsync(usuario, modelo.Password);

            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, "Cliente");
                await _signInManager.SignInAsync(usuario, isPersistent: false);
                return RedirectToAction("Index", "Productos");
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(modelo);
        }

        // Muestra el formulario de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Recibe el formulario cuando el usuario toca "Ingresar"
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await _signInManager.PasswordSignInAsync(
                modelo.Email, modelo.Password, modelo.Recordarme, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Productos");
            }

            ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
            return View(modelo);
        }
        // Cierra la sesión
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Productos");
        }
    }
}   