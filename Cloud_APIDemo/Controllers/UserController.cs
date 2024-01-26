using BLL.Forms;
using BLL.Services;
using Cloud_APIDemo.Hubs;
using Cloud_APIDemo.Models;
using Cloud_APIDemo.Tools;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

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
        private readonly ChatHub _chatHub;

        public UserController(UserService userService, ChatHub chatHub)
        {
            _userService = userService;
            _chatHub = chatHub;
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
        public IActionResult Login([FromBody]LoginForm form)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            User connectedUser = _userService.Login(form.Email, form.Password);
            

            if (connectedUser == null)
                return BadRequest("Utilisateur inexistant");

            TokenManager manager = new TokenManager();

            _chatHub.SendMessage($"L'utilisateur {connectedUser} vient de se connecter");

            return Ok(manager.GenerateToken(connectedUser));
        }

        [HttpPatch("updatePwd/{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody]UpdatePasswordForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            form.Id = id;
            _userService.UpdatePassword(form);
            return Ok("update success");
        }

        [Authorize("connected")]
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

        [Authorize("adminPolicy")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
