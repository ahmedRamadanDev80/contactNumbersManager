using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using contactNumbersManager.Data;
using contactNumbersManager.Models;
using System.Drawing.Printing;

namespace contactNumbersManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(string nameFilter = "", string phoneFilter = "", string addressFilter = "", int? editingId = null,int page = 1, int pageSize = 5)
        {
            // Filters
            ViewBag.NameFilter = nameFilter;
            ViewBag.PhoneFilter = phoneFilter;
            ViewBag.AddressFilter = addressFilter;
            // ID of the contact being edited
            ViewBag.EditingId = editingId;  
            // Pagination
            ViewBag.CurrentPage = page;     
            ViewBag.PageSize = pageSize;    

            int totalContacts = await _context.Contacts.CountAsync();
            ViewBag.TotalContacts = totalContacts;

            var query = _context.Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
                query = query.Where(c => c.Name.Contains(nameFilter));

            if (!string.IsNullOrEmpty(phoneFilter))
                query = query.Where(c => c.Phone.Contains(phoneFilter));

            if (!string.IsNullOrEmpty(addressFilter))
                query = query.Where(c => c.Address.Contains(addressFilter));

            var contacts = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(contacts);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Address,Notes")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        [HttpPost]
        public IActionResult SaveEdit(Contact editedContact, string nameFilter ="", string phoneFilter = "", string addressFilter = "", int page = 1)
        {
            if (ModelState.IsValid)
            {
                var contact = _context.Contacts.Find(editedContact.Id);
                if (contact != null)
                {
                    contact.Name = editedContact.Name;
                    contact.Phone = editedContact.Phone;
                    contact.Address = editedContact.Address;
                    contact.Notes = editedContact.Notes;

                    _context.SaveChanges();
                }
            }

            // Redirect to the same page after saving changeswith the same filters
            return RedirectToAction("Index", new { page, nameFilter, phoneFilter, addressFilter });
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
