using Cloud_APIDemo.Hubs;
using Cloud_APIDemo.Models;
using Cloud_APIDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_APIDemo.Controllers
{
    //[Authorize("connected")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        private readonly MovieHub _movieHub;
        public MovieController(MovieService movieService, MovieHub movieHub)
        {
            _movieService = movieService;
            _movieHub = movieHub;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_movieService.GetAll());
        }
        //[Authorize("adminPolicy")]

        [HttpGet("getById/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_movieService.GetById(id));
        }

        //[Authorize("adminPolicy")]
        [HttpPost]
        public IActionResult Ajout(Movie m)
        {
            _movieService.Add(m);
            _movieHub.NewMovie();
            return Ok();
        }

        //[HttpPatch]
        //public IActionResult Update(int id, string titre)
        //{
        //    list.FirstOrDefault(m => m.Id == id).Title = titre;
        //    return Ok();
        //}

        [AllowAnonymous]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _movieService.Delete(id);
            return Ok();
        }
    }
}
