using Microsoft.AspNetCore.Mvc;
using Tienda_Mera.Datos;
using Tienda_Mera.ViewModel;

namespace Tienda_Mera.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Por favor complete todos los campos correctamente.";
                return View(modelo);
            }

            var usuario = _appDbContext.Usuarios
                .FirstOrDefault(u => u.Email == modelo.Email && u.Contraseña == modelo.Contraseña);

            if (usuario == null)
            {
                TempData["Error"] = "Email o contraseña incorrectos.";
                return View(modelo);
            }

            TempData["Success"] = "¡Bienvenido!";
            return RedirectToAction("Index", "Productoes");
        }
    }
}
