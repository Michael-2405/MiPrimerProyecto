using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimerProyecto.Clases;

namespace MiPrimerProyecto.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet(Name = "GetUser")]
        
        public User[] Get ()
        {
            return [new User {idUsuario = 1, nombres = "alex", apellidos = "rosa", correo = "aRosa@gmail.com", username = "pambe", password = "1234" }];
        }
    }
}
