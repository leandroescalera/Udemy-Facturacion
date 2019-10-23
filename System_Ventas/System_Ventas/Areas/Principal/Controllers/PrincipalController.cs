using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace System_Ventas.Areas.Principal.Controllers
{
    [Authorize]
    [Area("Principal")]
    [Route("[controller]")]
    public class PrincipalController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}