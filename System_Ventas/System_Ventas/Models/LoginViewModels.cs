using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace System_Ventas.Models
{
    public class LoginViewModels
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "<font color='red'>El campo Correo Electronico es obligatorio.</font>")]
            [EmailAddress(ErrorMessage = "<font color='red'>El campo Correo Electronico no es una direccion de correo electronico valida.</font>")]
            public string Email { get; set; }

            [Required(ErrorMessage = "<font color='red'>El campo contraseña es obligatorio.</font>")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "<font color='red'>El numero de caracteres del {0} debe ser al menos {2}.</font>", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
