using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiNetCore.DataContext;
using WebApiNetCore.Models.MAINTE;
using Microsoft.EntityFrameworkCore;

namespace WebApiNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/CATEGORY")]
    //[ApiController]
    public class CATEGORYController : Controller
    {
        private readonly CATEGORY_CONTEXT _context;

        public CATEGORYController(CATEGORY_CONTEXT context)
        {
            _context=context;
        }
        // GET api/values
        
        [Route("~/api/GetAllCategory")]
        [HttpGet]
        public IEnumerable<CATEGORY> GetCATEGORYS()
        {
            return _context.CATEGORY;
        }

        // GET api/values/5
        
        [Route("~/api/GetCategory")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCATEGORY(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                //return Ok(_context.CATEGORY.ToList());
            }
            var mainte=await _context.CATEGORY.SingleOrDefaultAsync(m => m.Id==id);
            if(mainte==null)
            {
                return NotFound();
            }
            return Ok(mainte);
        }

        // POST api/values
        
        [Route("~/api/AddCategory")]
        [HttpPost]
        public async Task<IActionResult> PostCATEGORY([FromBody]CATEGORY category)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.CATEGORY.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id=category.Id }, category);
        }

        // PUT api/values/5
        
        [Route("~/api/UpdateCategory/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCATEGORY(int id, [FromBody] CATEGORY category)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id !=  category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!CATEGORYExists(id))
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
        
        [Route("~/api/DeleteCategory/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCATEGORY(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cate= await _context.CATEGORY.SingleOrDefaultAsync(m => m.Id ==id);
            if(cate==null)
            {
                return NotFound();
            }

            _context.CATEGORY.Remove(cate);
            await _context.SaveChangesAsync();

            return Ok(cate);
        }

        private bool CATEGORYExists(int id)
        {
            return _context.CATEGORY.Any(e => e.Id==id);
        }
    }
}
