using PortfolioDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Helpers
{
    public class DrawingHelper
    {
        private readonly PortfolioDbContext _context;

        public DrawingHelper(PortfolioDbContext context)
        {
            _context = context;
        }
        public async Task<Drawing> CreateAsync(Drawing drawing)
        {
            _context.Drawings.Add(drawing);

            await _context.SaveChangesAsync();

            return drawing;
        }

        public async Task<Drawing> DeleteByAsync(int id)
        {
            var drawing = await _context.Drawings.FindAsync(id);

            if (drawing != null)
            {
                _context.Drawings.Remove(drawing);
                await _context.SaveChangesAsync();
            }

            return drawing;
        }
    }
}
