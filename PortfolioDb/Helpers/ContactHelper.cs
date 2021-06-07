using Microsoft.EntityFrameworkCore;
using PortfolioDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Helpers
{
    public class ContactHelper : IAsyncHelper<Contact>
    {
        private readonly PortfolioDbContext _context;
        public ContactHelper(PortfolioDbContext context)
        {
            _context = context;
        }
        public async Task<Contact> CreateAsync(Contact contact)
        {
            contact.DateSent = DateTime.Now;
            _context.Contacts.Add(contact);

            await _context.SaveChangesAsync();

            return contact;
        }
        public async Task<List<Contact>> DeleteByAsync(string fullName)
        {
            var contacts = await _context.Contacts
                .Where(c => c.FullName.ToLower() == fullName.ToLower())
                .ToListAsync();

            _context.Contacts.RemoveRange(contacts);
            await _context.SaveChangesAsync();

            return contacts;
        }
        public async Task<Contact> DeleteByAsync(int id)
        {
            Contact contact = await _context.Contacts.FindAsync(id);

            if(contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }

            return contact;
        }
        public async Task<List<Contact>> DeleteAllAsync()
        {
            var contacts = await _context.Contacts.ToListAsync();

            _context.Contacts.RemoveRange(contacts);
            await _context.SaveChangesAsync();

            return contacts;
        }
    }
}
