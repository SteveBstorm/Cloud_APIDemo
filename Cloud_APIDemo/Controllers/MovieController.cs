using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        List<string> list;
        public MovieController()
        {
            list = new List<string>();
            list.Add("LOTR : The Two Towers");
            list.Add("Star Wars : New Hope");
            list.Add("Pacific Rim");
            list.Add("Joker");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(list);
        }

        [HttpGet("getById/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(list[id]);
        }

        [HttpPost]
        public IActionResult Ajout(string titre)
        {
            list.Add(titre);
            return Ok(list);
        }

        [HttpPatch]
        public IActionResult Update(int id, string titre)
        {
            list[id] = titre;
            return Ok(list);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            list.RemoveAt(id);
            return Ok(list);
        }
    }
}
