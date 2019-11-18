using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System_Ventas.Controllers;
using System_Ventas.Library;

namespace System_Ventas.Areas.Usuarios.Controllers
{
    [Authorize]
    [Area("Usuarios")]
    public class UsuariosController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private LUsuarios _usuarios;
        public UsuariosController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _usuarios = new LUsuarios();
        }
        
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                ViewData["Roles"] = _usuarios.userData(HttpContext);
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        public async Task<IActionResult> SessionClose()
        {
            HttpContext.Session.Remove("User");
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "");
            
        }


    }
}