using Microsoft.EntityFrameworkCore;
using PortfolioDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Helpers
{
    public enum BlogOrderMethod
    {
        Time,
        View
    }
    public class BlogHelper
    {
        private readonly PortfolioDbContext _context;

        public BlogHelper(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetByAsync(string title)
        {
            List<Blog> blogsByTitle = await _context.Blogs
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();

            if (blogsByTitle.Count() > 0)
            {
                return blogsByTitle;
            }

            return null;
        }

        public async Task<List<Blog>> GetByOrderedAsync(BlogOrderMethod method)
        {
            List<Blog> blogs = null;

            switch (method)
            {
                case BlogOrderMethod.Time:
                    blogs = await blogsOrderByTimeAsync();
                    break;
                case BlogOrderMethod.View:
                    blogs = await blogsOrderByViewAsync();
                    break;
            }

            if (blogs == null || blogs.Count() == 0)
                return null;

            return blogs;
        }

        private async Task<List<Blog>> blogsOrderByTimeAsync()
        {
            return await _context.Blogs.OrderByDescending(b => b.DateCreated).ToListAsync();
        }

        private async Task<List<Blog>> blogsOrderByViewAsync()
        {
            return await _context.Blogs.OrderByDescending(b =>
                        _context.Views.FirstOrDefault(v => v.PageName == ("kongiblog/" + b.Id)) == null ? 0
                        : _context.Views.FirstOrDefault(v => v.PageName == ("kongiblog/" + b.Id)).NumOfViews
                    ).ToListAsync();
        }

        public async Task<Blog> CreateAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return blog;
        }

        public async Task<Blog> UpdateAsync(Blog oldBlog, Blog newBlog)
        {
            //update from newblog's information
            var newBlogProp = newBlog.GetType().GetProperties();

            foreach (var prop in newBlogProp)
            {
                if (prop.Name != "Id" && prop.GetValue(newBlog) != null)
                {
                    oldBlog.GetType().GetProperty(prop.Name).SetValue(oldBlog, prop.GetValue(newBlog));
                }
            }

            await _context.SaveChangesAsync();

            return oldBlog;
        }

        public async Task<List<Blog>> DeleteByAsync(string title)
        {
            List<Blog> blogs = await this.GetByAsync(title);

            if (blogs != null && blogs.Count() > 0)
            {
                _context.Blogs.RemoveRange(blogs);
                await _context.SaveChangesAsync();
            }

            return blogs;
        }
        public async Task<Blog> DeleteByAsync(int id)
        {
            Blog blog = await _context.Blogs.FindAsync(id);

            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
            }

            return blog;
        }
    }
}
