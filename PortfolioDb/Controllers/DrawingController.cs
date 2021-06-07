using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioDb.Helpers;
using PortfolioDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioDb.Controllers
{
    [EnableCors("allowCorePolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DrawingController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        private readonly DrawingHelper _drawingHelper;

        public DrawingController(PortfolioDbContext context, DrawingHelper drawingHelper)
        {
            _context = context;
            _drawingHelper = drawingHelper;
        }

        // GET: api/drawing
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drawings = await  _context.Drawings.ToListAsync();

            if (drawings.Count() == 0)
            {
                return NotFound();
            }
            return Ok(new { status = 200, drawings });
        }
        //POST: api/drawing
        [HttpPost]
        public async Task<IActionResult> Post(Drawing drawingToCreate)
        {
            drawingToCreate.DatePut = DateTime.Now;

            if (!ModelState.IsValid)
                return BadRequest();

            Drawing drawing = await _drawingHelper.CreateAsync(drawingToCreate);

            return CreatedAtAction("Post", new { status = 201, drawing });
        }

        // DELETE api/drawing
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var drawing = await _drawingHelper.DeleteByAsync(id);

            if (drawing == null)
                return NotFound();

            return Ok(new { status = 200, lineRemoved = 1, drawing });
        }
    }
}
