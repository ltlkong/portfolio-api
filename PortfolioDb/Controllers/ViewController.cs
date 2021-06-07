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
    public class ViewController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        private readonly ViewHelper _viewHelper;
        public ViewController(PortfolioDbContext context, ViewHelper viewHelper)
        {
            _context = context;
            _viewHelper = viewHelper;
        }

        // GET api/view
        // GET api/view?name=kongi/home
        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            View view = null;
            List<View> views = null;

            if (name != null)
                view = await _viewHelper.GetByAsync(name);
            else
                views = await _context.Views.ToListAsync();

            if (view != null)
                return Ok(new { status = 200, view });
            else if(views != null && views.Count() > 0)
                return Ok(new { views, status = 200 });

            return NotFound();
        }

        //increase view by 1
        // PUT api/view?name=kongi/home
        [HttpPut]
        public async Task<IActionResult> Put(string name)
        {
            if (name == null)
                return BadRequest();

            View view = await _context.Views
                .FirstOrDefaultAsync(v => v.PageName.ToLower() == name.ToLower());

            if (view != null)
            {
                view.NumOfViews++;
                view.LastViewed = DateTime.Now;

                await _context.SaveChangesAsync();
                    
                return Ok(new { view, status = 200 });
            }
            else
            {
                //not exist in db create a new one
                View newView = new View
                {
                    PageName = name,
                    NumOfViews = 1,
                    LastViewed = DateTime.Now
                };

                view = await _viewHelper.CreateAsync(newView);

                return CreatedAtAction("Put",new { status=201, view});
            }
        }

        //DELETE api/view
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            int lineRemoved = _context.Views.Count();

            _context.Views.RemoveRange(_context.Views);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, lineRemoved });
        }
    }
}
