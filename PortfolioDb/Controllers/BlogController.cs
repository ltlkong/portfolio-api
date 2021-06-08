using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioDb.Helpers;
using PortfolioDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Controllers
{
    [EnableCors("allowCorePolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly PortfolioDbContext _context;
        private readonly BlogHelper _blogHelper;

        public BlogController(PortfolioDbContext context, BlogHelper blogHelper)
        {
            _context = context;
            _blogHelper = blogHelper;
        }
        //get specific blog
        //GET api/blog/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {       
            var blog = await _context.Blogs.FindAsync(id);

            if (blog != null)
                return Ok(new { status = 200, blog });

            return NotFound();
        }

        // GET api/blog
        // GET api/blog?title=blogTitle
        // GET api/blog?method=time
        [HttpGet]
        public async Task<ActionResult> Get(string title, BlogOrderMethod? orderMethod)
        {
            List<Blog> blogs = null;

            if (title != null )
                blogs = await _blogHelper.GetByAsync(title);
            else if(orderMethod != null)
                blogs = await _blogHelper.GetByOrderedAsync((BlogOrderMethod)orderMethod);
            else
                blogs = await _context.Blogs.ToListAsync();

            if (blogs != null && blogs.Count() > 0)
            {
                var blogInfoShort = blogs.Select(b =>
                    new {
                        id = b.Id,
                        title = b.Title,
                        description = b.Description,
                        imgUrl = b.ImgUrl,
                        dateCreated = b.DateCreated
                    }
                );

                return Ok(new { status = 200, blogs = blogInfoShort });
            }

            return NotFound();
        }

        // POST api/blog
        [HttpPost]
        public async Task<IActionResult> Post(Blog blog)
        {
            blog.DateCreated = DateTime.Now;

            if (!ModelState.IsValid)
                return BadRequest();

            await _blogHelper.CreateAsync(blog);

            return CreatedAtAction("Post", new { status = 201, blog }); 
        }

        // PUT api/blog/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Blog newBlog)
        {
            var blog = await _context.Blogs.FindAsync(id);
            //update from newblog's information
            if (blog != null)
            {        
                var updatedBlog = await _blogHelper.UpdateAsync(blog, newBlog);

                return Ok(new { status = 200, blog=updatedBlog });
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = await _blogHelper.DeleteByAsync(id);
            int lineRemoved = 1;

            if (blog != null)
                return Ok(new { status = 200, lineRemoved, blog });

            return NotFound();
        }

        // DELETE api/blog?title=blogTitle
        [HttpDelete]
        public async Task<IActionResult> Delete(string title)
        {
            List<Blog> blogs = null;
            int lineRemoved = 0;

            if (title != null)
            {
                blogs = await _blogHelper.DeleteByAsync(title);
                lineRemoved = blogs.Count();
            } 

            if (blogs != null && blogs.Count() > 0)
            {
                return Ok(new { status = 200, lineRemoved, blogs });
            }

            return NotFound();
        }
    }
}
