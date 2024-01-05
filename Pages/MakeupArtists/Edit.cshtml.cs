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

namespace Proiect.Pages.MakeupArtists
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
        public MakeupArtist MakeupArtist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MakeupArtist == null)
            {
                return NotFound();
            }

            var makeupartist =  await _context.MakeupArtist
                .Include(m => m.Products)
                .Include(m => m.Appointments)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (makeupartist == null)
            {
                return NotFound();
            }
            MakeupArtist = makeupartist;
            ViewData["ServiceID"] = new SelectList(_context.Set<Service>(), "ID", "ServiceName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Preia Cost din Service asociat
            MakeupArtist.ServiceCost = (decimal)(_context.Service.FirstOrDefault(s => s.ID == MakeupArtist.ServiceID)?.Cost);

            // Verificam dacă ServiceCost are o valoare
            if (MakeupArtist.ServiceCost == null)
            {
                ModelState.AddModelError("MakeupArtist.ServiceID", "Invalid Service ID");
                return Page();
            }
            _context.Attach(MakeupArtist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakeupArtistExists(MakeupArtist.ID))
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

        private bool MakeupArtistExists(int id)
        {
          return (_context.MakeupArtist?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
