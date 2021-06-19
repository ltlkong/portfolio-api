using Microsoft.EntityFrameworkCore;
using PortfolioDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Helpers
{
    public class ViewHelper
    {
        private readonly PortfolioDbContext _context;
        public ViewHelper(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<View> GetByAsync(string name)
        {
            View view = await _context.Views
                .FirstOrDefaultAsync(v => v.PageName.ToLower() == name.ToLower());

            return view;
        }
        public async Task<View> CreateAsync(View view)
        {
            _context.Views.Add(view);

            await _context.SaveChangesAsync();

            return view;
        }
    }
}
