using System;
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

        // POST api/<controller>/Dog
        [HttpPost("Dog")]
        public IActionResult PostDog([FromBody]Dog value)
        {
            var result = this._petRepository.AddDog(value);
            if (result.success)
            {
                return Created(this.HttpContext.GetEndpoint().ToString(), result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("Cat")]
        public IActionResult PostCat([FromBody]Cat value)
        {
            var result = this._petRepository.AddCat(value);
            if (result.success)
            {
                return Created(this.HttpContext.GetEndpoint().ToString(), result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
