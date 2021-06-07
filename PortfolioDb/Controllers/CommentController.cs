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
    public class CommentController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        private readonly CommentHelper _commentHelper;
        public CommentController(PortfolioDbContext context, CommentHelper commentHelper)
        {
            _context = context;
            _commentHelper = commentHelper;
        }

        // GET api/comment
        // GET api/comment?blogId=1
        [HttpGet]
        public async Task<IActionResult> Get(int? blogId)
        {
            List<Comment> comments = null;

            if (blogId != null)
            {
                comments = await _commentHelper.GetByAsync((int)blogId);
            }
            else
            {
                comments = await _context.Comments.ToListAsync();
            }

            if(comments != null && comments.Count() > 0)
                return Ok(new { status = 200, comments });
            
            return NotFound();
        }

        // POST api/comment
        [HttpPost]
        public async Task<IActionResult> Post(Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _commentHelper.CreateAsync(comment);

            return CreatedAtAction("Post", new { status = 201, comment });
        }

        // DELETE api/comment/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Comment comment = await _commentHelper.DeleteByAsync(id);
            int lineRemoved = 1;

            if (comment != null)
                return Ok(new { status = 200, comment, lineRemoved });
            return NotFound();
        }

        // DELETE api/comment?blogTitle=blogTitle
        [HttpDelete]
        public async Task<IActionResult> Delete(string blogTitle)
        {
            List<Comment> comments = null;
            int lineRemoved = 0;

            if(blogTitle != null)
            {
                comments = await _commentHelper.DeleteByAsync(blogTitle);
                lineRemoved = comments.Count();
            }

            if(comments != null)
                return Ok(new { status = 200, comments, lineRemoved });

            return NotFound();
        }
    }
}
