using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["MakeupArtistID"] = new SelectList(_context.MakeupArtist, "ID", "FullName"); // ca sa nu afiseze ID-ul am schimbat "ID" cu "FullName"
            return Page();
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Appointments == null || Appointment == null)
            {
                return Page();
            }

            // Verificam dacă există deja programări pentru același artist, în aceeași dată și oră
            var existingAppointments = await _context.Appointments
                .Where(a => a.MakeupArtistID == Appointment.MakeupArtistID &&
                    a.AppointmentDate > Appointment.AppointmentDate.AddMinutes(-30) &&
                    a.AppointmentDate < Appointment.AppointmentDate.AddMinutes(30))
                .ToListAsync();
            if (existingAppointments.Any())
            {
                // Există deja programări pentru același artist, la aceeași dată și oră
                ModelState.AddModelError(string.Empty, "Nu se poate realiza această programare, alegeți altă dată sau oră.");
                ViewData["MakeupArtistID"] = new SelectList(_context.MakeupArtist, "ID", "FullName");
                return Page();
            }

            Appointment.IsOccupied = true;

            _context.Appointments.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
