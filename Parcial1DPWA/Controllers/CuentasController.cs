using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parcial1DPWA.Models;

namespace Parcial1DPWA.Controllers
{
    public class CuentasController : Controller
    {
        private readonly UserManager<UsuariosApp> _userManager;
        private readonly SignInManager<UsuariosApp> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager; // Agregado para manejar roles

        public CuentasController(UserManager<UsuariosApp> userMgr,
            SignInManager<UsuariosApp> signMgr,
            RoleManager<IdentityRole> roleMgr) // Inyectado en el constructor
        {
            _userManager = userMgr;
            _signInManager = signMgr;
            _roleManager = roleMgr;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // El tercer parámetro 'false' es para que no recuerde la sesión tras cerrar el navegador
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded) return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View();
        }

        public IActionResult Registro() => View();

        [HttpPost]
        public async Task<IActionResult> Registro(string email, string password, string nombre, string rol)
        {
            // Crea los roles en la base de datos si no existen aún
            if (!await _roleManager.RoleExistsAsync("Administrador"))
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));

            if (!await _roleManager.RoleExistsAsync("Usuario"))
                await _roleManager.CreateAsync(new IdentityRole("Usuario"));

            var user = new UsuariosApp { UserName = email, Email = email, NombreCompleto = nombre };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Asigna el rol seleccionado en el formulario
                await _userManager.AddToRoleAsync(user, rol);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Cuentas");
        }

        public IActionResult AccesoDenegado() => View();
    }
}