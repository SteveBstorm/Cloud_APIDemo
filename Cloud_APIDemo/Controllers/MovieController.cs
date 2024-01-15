using Cloud_APIDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_APIDemo.Controllers
{
    [Authorize("connected")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        List<Movie> list;
        public MovieController()
        {
            list = new List<Movie>();
            list.Add(new Movie (1, "LOTR : The Two Towers", 1999));
            list.Add(new Movie( 2, "Star Wars : New Hope", 1977));
            list.Add(new Movie( 3, "Pacific Rim", 2013));
            list.Add(new Movie( 4, "Joker", 2021));
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(list);
        }
        [Authorize("adminPolicy")]

        [HttpGet("getById/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(list.FirstOrDefault(m => m.Id == id));
        }

        [Authorize("adminPolicy")]
        [HttpPost]
        public IActionResult Ajout(Movie m)
        {
            list.Add(m);
            return Ok();
        }

        [HttpPatch]
        public IActionResult Update(int id, string titre)
        {
            list.FirstOrDefault(m => m.Id == id).Title = titre;
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            list.RemoveAt(id);
            return Ok(list);
        }
    }
}
