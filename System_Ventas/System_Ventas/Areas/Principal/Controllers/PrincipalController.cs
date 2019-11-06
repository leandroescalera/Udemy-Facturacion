using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System_Ventas.Library;

namespace System_Ventas.Areas.Principal.Controllers
{
    [Authorize]
    [Area("Principal")]
    //[Route("[controller]")]
    public class PrincipalController : Controller
    {
        
        private LUsuarios _usuarios;

        public PrincipalController()
        {
            _usuarios = new LUsuarios();

        }
        public IActionResult Index()
        {
            ViewData["Roles"] = _usuarios.userData(HttpContext);
            return View();
        }
    }
}