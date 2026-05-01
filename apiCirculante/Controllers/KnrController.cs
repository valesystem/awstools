using apiCirculante.Application;
using apiCirculante.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiCirculante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnrController : ControllerBase
    {
        // GET: api/<knr>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<knr>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<knr>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<mdKnr01> Knrs)
        {
            RotinasKnr rtKnr = new RotinasKnr();
            await rtKnr.AtualizarKnr(Knrs);
            return Ok("");
        }

        // PUT api/<knr>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<knr>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
