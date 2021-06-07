using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioDb.Helpers;
using PortfolioDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;   

namespace PortfolioDb.Controllers
{
    [EnableCors("allowCorePolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {

        private readonly PortfolioDbContext _context;
        private readonly ContactHelper _contactHelper;

        public ContactController(PortfolioDbContext context,ContactHelper contactHelper)
        {
            _context = context;
            _contactHelper = contactHelper;
        }

        //GET api/contact
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _context.Contacts.ToListAsync();

            if (contacts.Count()>0)
                return Ok(new { status = 200, contacts });

            return NotFound();
        }

        //POST api/contact
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Contact contactToCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Contact contact = await _contactHelper.CreateAsync(contactToCreate);

            return CreatedAtAction("Post", new { status = 201, contact });
        }

        //DELETE api/contact?fullName=aaa
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Contact contact = await _contactHelper.DeleteByAsync(id);
            int lineRemoved = 1;

            if (contact != null)
                return Ok(new { status = 200, lineRemoved, contact });

            return NotFound();
        }

        //DELETE api/contact?fullName=aaa
        [HttpDelete]
        public async Task<IActionResult> Delete(string fullName)
        {
            List<Contact> contacts = null;
            int lineRemoved = 0;

            if (fullName != null)
            {
                contacts = await _contactHelper.DeleteByAsync(fullName);
                lineRemoved = contacts.Count();
            }     

            if (contacts != null)
                return Ok(new { status = 200, lineRemoved, contacts });

            return NotFound();
        }
    }
}
