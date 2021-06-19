using Microsoft.EntityFrameworkCore;
using PortfolioDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Helpers
{
    public class CommentHelper
    {
        private readonly PortfolioDbContext _context;

        public CommentHelper(PortfolioDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetByAsync(int blogId)
        {
            var comments = await _context.Comments
                .Where(c => c.BlogId == blogId).ToListAsync();          

            return comments;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {

            comment.DateSent = DateTime.Now;
            _context.Comments.Add(comment);

            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> DeleteByAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }

            return comment;
        }

        public async Task<List<Comment>> DeleteByAsync(string blogTitle)
        {
            var blog = await _context.Blogs
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Title.ToLower() == blogTitle.ToLower());

            List<Comment> comments = null;

            if (blog != null)
            {
                comments = blog.Comments.ToList();

                _context.Comments.RemoveRange(comments);

                await _context.SaveChangesAsync();
            }

            return comments;
        }
    }
}
