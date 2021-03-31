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
                var songs = _context.Songs.Select(s => new SongDTO() { Id = s.Id, Title = s.Title, Artist = s.Artist, Album = s.Album, ReleaseDate = s.ReleaseDate, Likes = s.Likes});
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
            try
            {
                var song = _context.Songs.Where(s => s.Id == id)
                    .Select(s => new SongDTO() { Id = s.Id, Title = s.Title, Artist = s.Artist, Album = s.Album, ReleaseDate = s.ReleaseDate, Likes = s.Likes })
                    .FirstOrDefault();
                if (song == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    return Ok(song);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<MusicController>
        [HttpPost]
        public IActionResult Post([FromBody] SongDTO song)
        {
            try
            {
                Song newSong = new Song() {Title = song.Title, Album = song.Album, Artist = song.Artist, ReleaseDate = song.ReleaseDate };
                newSong.Likes = 0;
                _context.Add(newSong);
                _context.SaveChanges();
                song.Id = newSong.Id;
                song.Likes = 0;
                return StatusCode(201, song);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SongDTO song)
        {
            try
            {
                var songInDb = _context.Songs.Where(s => s.Id == id).FirstOrDefault();
                if (songInDb == null)
                {
                    return BadRequest();
                }
                else
                {
                    songInDb.Album = song.Album;
                    songInDb.Title = song.Title;
                    songInDb.ReleaseDate = song.ReleaseDate;
                    songInDb.Artist = song.Artist;
                    song.Likes = songInDb.Likes;
                    _context.Update(songInDb);
                    _context.SaveChanges();
                    return StatusCode(200, song);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var songToDelete = _context.Songs.Where(s => s.Id == id).FirstOrDefault();
                if (songToDelete != null)
                {
                    SongDTO copyOfSong = new SongDTO { Id = songToDelete.Id, Album = songToDelete.Album, Artist = songToDelete.Artist, Title = songToDelete.Title };
                    _context.Remove(songToDelete);
                    _context.SaveChanges();
                    return StatusCode(200, copyOfSong);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        // PUT api/<MusicController>/5
        [HttpPut("{id}/LikeSong")]
        public IActionResult AddLike(int id)
        {
            try
            {
                Song song = _context.Songs.Where(s => s.Id == id).FirstOrDefault();
                if (song == null)
                {
                    return BadRequest();
                }
                else
                {
                    song.Likes += 1;
                    _context.Update(song);
                    _context.SaveChanges();
                    SongDTO returnVal = new SongDTO() { Title = song.Title, Album = song.Album, Artist = song.Artist, ReleaseDate = song.ReleaseDate, Likes = song.Likes };
                    return StatusCode(200, returnVal);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
