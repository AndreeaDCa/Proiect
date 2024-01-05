using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.MakeupArtists
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
       
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ServiceID"] = new SelectList(_context.Set<Service>(), "ID", "ServiceName");
            return Page();
        }

        [BindProperty]
        public MakeupArtist MakeupArtist { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.MakeupArtist == null || MakeupArtist == null)
            {
                return Page();
            }

            // Preiați Cost din Service asociat
            MakeupArtist.ServiceCost = (decimal)_context.Service.FirstOrDefault(s => s.ID == MakeupArtist.ServiceID)?.Cost;

            // Verificați dacă ServiceCost are o valoare
            if (MakeupArtist.ServiceCost == null)
            {
                ModelState.AddModelError("MakeupArtist.ServiceID", "Invalid Service ID");
                return Page();
            }
            // Adăugare la colecția Appointments (presupunând o relație One-to-Many)
            if (MakeupArtist.Appointments != null)
            {
                foreach (var appointment in MakeupArtist.Appointments)
                {
                    appointment.MakeupArtist = MakeupArtist;
                    _context.Appointments.Add(appointment);
                }
            }

            // Adăugare la colecția Reviews (presupunând o relație One-to-Many)
            if (MakeupArtist.Reviews != null)
            {
                foreach (var review in MakeupArtist.Reviews)
                {
                    review.MakeupArtist = MakeupArtist;
                    _context.Review.Add(review);
                }
            }

            // Adăugare la colecția Products (presupunând o relație One-to-Many)
            if (MakeupArtist.Products != null)
            {
                foreach (var product in MakeupArtist.Products)
                {
                    product.MakeupArtist = MakeupArtist;
                    _context.Product.Add(product);
                }
            }
            _context.MakeupArtist.Add(MakeupArtist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
