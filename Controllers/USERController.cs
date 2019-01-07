using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiNetCore.DataContext;
using WebApiNetCore.Models.User;
using Microsoft.EntityFrameworkCore;

namespace WebApiNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/USER")]
    //[ApiController]
    public class USERController : Controller
    {
        private readonly USER_CONTEXT _context;

        public USERController(USER_CONTEXT context)
        {
            _context=context;
        }
        // GET api/values
        
        [Route("~/api/GetAllUSER")]
        [HttpGet]
        public IEnumerable<USER> GetUSERS()
        {
            List<USER> lsUser=  _context.User.ToList();
            lsUser = (from user in lsUser
                        where user.IsActive.Equals(1)
                     select user).ToList();
            return lsUser;
        }

        // GET api/values/5
        
        [Route("~/api/GetUSER")]
        [HttpGet("{username,password}")]
        public async Task<IActionResult> GetUSER(string username, string password)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                //return Ok(_context.USER.ToList());
            }
            var user=await _context.User.SingleOrDefaultAsync(m => m.UserName==username && m.Password==password);
            if(user==null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/values
        
        [Route("~/api/AddUSER")]
        [HttpPost]
        public async Task<IActionResult> PostUSER([FromBody]USER USER)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.User.Add(USER);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUSER", new { username=USER.UserName, password=USER.Password }, USER);
        }

        // PUT api/values/5
        
        [Route("~/api/UpdateUSER/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUSER(int id, [FromBody] USER USER)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id !=  USER.Id)
            {
                return BadRequest();
            }

            _context.Entry(USER).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!USERExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE api/values/5
        
        [Route("~/api/DeleteUSER/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUSER(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            USER USER= await _context.User.SingleOrDefaultAsync(m => m.Id ==id);
            if(USER==null)
            {
                return NotFound();
            }
            USER.IsActive=0;
            _context.Entry(USER).State=EntityState.Modified;
            //_context.User.Remove(cate);
            //await _context.SaveChangesAsync();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!USERExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(USER);
        }

        private bool USERExists(int id)
        {
            return _context.User.Any(e => e.Id==id);
        }
    }
}
