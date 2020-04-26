using System.Collections.Generic;
using BusinessLogic.src.models;
using BusinessLogic.src.repositories.pet_repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccessLibrary.Controllers
{
    [Route("api/[controller]")]
    public class PetController : Controller
    {
        private readonly IPetRepository _petRepository;
        public PetController(IPetRepository petRepository)
        {
            this._petRepository = petRepository;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>/Dog
        [HttpPost("Dog")]
        public IActionResult PostDog([FromBody]Dog value)
        {
            var result = this._petRepository.AddDog(value);
            if (result.success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
