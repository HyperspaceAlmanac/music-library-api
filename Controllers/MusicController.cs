using Microsoft.AspNetCore.Mvc;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private ApplicationDbContext _context;
        private bool seedValue = false;

        public MusicController(ApplicationDbContext context)
        {
            _context = context;
            if (seedValue)
            {
                Song temp = new Song() { Title = "Something", Artist = "Popular Artist", Album = "Some Album", ReleaseDate = DateTime.Today };
                _context.Add(temp);
                _context.SaveChanges();
            }

        }
        // GET: api/<MusicController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var songs = _context.Songs.Select(s => s);
                return Ok(songs);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new {Id = "value", AnotherValue = "something else"});
        }

        // POST api/<MusicController>
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {
            try
            {
                _context.Add(song);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
            return NotFound();
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NotFound();
        }
    }
}
