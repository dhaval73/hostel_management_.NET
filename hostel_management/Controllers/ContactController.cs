using Microsoft.AspNetCore.Mvc;
using hostel_management.Data;
using hostel_management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace hostel_management.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contact/Index
      
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return View(contacts);
        }


        // GET: Contact/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, Email, MobileNo, InquiryType, Message")] ContactModel contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Save data
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create));
                }
                catch (Exception ex)
                {
                    // Log exception
                    // You should log exceptions to a logging framework like Serilog or NLog
                    Console.WriteLine("Error occurred while saving data:");
                    Console.WriteLine(ex.Message);
                    ModelState.AddModelError("", "An error occurred while saving the contact.");
                    TempData["SuccessMessage"] = "Contact created successfully.";
                    return View(contact); // Return to the create view with the entered data
                }
            }

            // If ModelState is not valid, return to the create view with validation errors
            return View(contact);
        }

        // Other CRUD actions (Edit, Delete, Details) can be implemented similarly
    }
}
