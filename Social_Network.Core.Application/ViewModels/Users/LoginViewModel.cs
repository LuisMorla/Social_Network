using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.ViewModels.Users
{
    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe Ingresar el nombre de usuario")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe Ingresar la contraseña")]
        public string Password { get; set; }

    }
}
