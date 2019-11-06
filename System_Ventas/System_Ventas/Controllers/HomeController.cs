using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System_Ventas.Areas.Principal.Controllers;
using System_Ventas.Library;
using System_Ventas.Models;

namespace System_Ventas.Controllers
{
    public class HomeController : Controller
    {
        private LUsuarios _usuarios;
        private SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _usuarios = new LUsuarios(userManager, signInManager, roleManager);
        }
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(PrincipalController.Index), "Principal");
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                List<object[]> listObject = await _usuarios.userLogin(model.Input.Email, model.Input.Password);
                object[] objects = listObject[0];
                var _identityError = (IdentityError)objects[0];
                model.ErrorMessage = _identityError.Description;
                if (model.ErrorMessage.Equals("True"))
                {
                    var data = JsonConvert.SerializeObject(objects[1]);
                    HttpContext.Session.SetString("User", data);
                    return RedirectToAction(nameof(PrincipalController.Index), "Principal", new { Area = "Principal" });
                }
                else
                {
                    View(model);
                }
            }
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            String mensaje;
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                String[] rolesName = { "Admin", "User" };
                foreach (var item in rolesName)
                {
                    var roleExist = await roleManager.RoleExistsAsync(item);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(item));
                    }

                }
                var user = await userManager.FindByIdAsync("7817d2b3-2de3-4e28-bafa-ad9f09a0b584");
                await userManager.AddToRoleAsync(user, "Admin");
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

        }

        private async Task ejecutarTareaAsync()
        {
            var data = await Tareas();
            String tarea = "Ahora ejecutaremos las demas lineas de codigo porque la tarea a finalizado";

        }

        private async Task<String> Tareas()
        {
            Thread.Sleep(20 * 1000);
            String tarea = "Tarea finalizada";
            return tarea;
        }


    }
}
