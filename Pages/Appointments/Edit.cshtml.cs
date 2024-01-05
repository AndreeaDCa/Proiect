using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Appointments
{
    [Authorize(Roles = "Admin")]

    public class EditModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public EditModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;
        public SelectList UsersSelectList { get; set; }
        public SelectList MakeupArtistsSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.MakeupArtist)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (appointment == null)
            {
                return NotFound();
            }

            Appointment = appointment;

            ViewData["MakeupArtistsSelectList"] = new SelectList(_context.MakeupArtist, "ID", "FullName", Appointment.MakeupArtistID);

            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["MakeupArtistsSelectList"] = new SelectList(_context.MakeupArtist, "ID", "FullName", Appointment.MakeupArtistID);

                return Page();
            }

            var existingAppointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.MakeupArtistID == Appointment.MakeupArtistID && a.AppointmentDate == Appointment.AppointmentDate);

            if (existingAppointment != null)
            {
                ModelState.AddModelError(string.Empty, "Există deja o programare pentru același artist și aceeași dată/oră.");
                return Page();
            }

            _context.Attach(Appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(Appointment.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
