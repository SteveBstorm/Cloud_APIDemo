using BLL.Forms;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /*
         Règles Rest API

        1) Utiliser le bon verbe pour la bonne action
        2) Chaque ressource doit être identifiée de manière unique
        3) Un seul end-point par ressource (end-point = nom du controlleur)
         */

        private readonly UserService _userService;

        public UserController([FromBody]UserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Permet l'enregistrement d'un utilisateur
        /// </summary>
        /// <param name="form">RegisterForm</param>
        /// <response code="400">Erreur dans le formulaire</response>
        /// <returns></returns>
        /// <remarks>Toto fait du vélo</remarks>
        [HttpPost("register")]
        public IActionResult Create([FromBody] RegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _userService.Create(form);
            return Ok("Enregistrement effectué");
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPatch("updatePwd/{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody]UpdatePasswordForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            form.Id = id;
            _userService.UpdatePassword(form);
            return Ok("update success");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id) { 
            return Ok(_userService.GetById(id));
        }

        [HttpGet("getUserByEmail/{email}")]
        public IActionResult Get([FromRoute] string email)
        {
            return Ok(_userService.GetByMail(email));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
