using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private SongsContext? _db;
        public SongsController(SongsContext songsContext)

        {
            _db = songsContext;
        }

        // GET: api/<SongsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> Get()
        {
            return await _db.Song.ToListAsync(); ;
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> Get(int id)
        {
            Song song = await _db.Song.FirstOrDefaultAsync(x => x.Id == id);
            if (song == null)
                return NotFound();
            return new ObjectResult(song);
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<ActionResult<Song>> Post(Song song)
        {
            if (song == null)
            {
                return BadRequest();
            }
            _db.Song.Add(song);
            await _db.SaveChangesAsync();
            return Ok(song);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Song>> Put(Song song)
        {
            if (song == null)
            {
                return BadRequest();
            }
            if (!_db.Song.Any(x => x.Id == song.Id))
            {
                return NotFound();
            }
            _db.Update(song);
            await _db.SaveChangesAsync();
            return Ok(song);
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> Delete(int id)
        {
            Song song = _db.Song.FirstOrDefault(x => x.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            _db.Song.Remove(song);
            await _db.SaveChangesAsync();
            return Ok(song); ;
        }
    }
}
