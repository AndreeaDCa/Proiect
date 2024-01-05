using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.MakeupArtists
{
    public class DetailsModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public DetailsModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public MakeupArtist MakeupArtist { get; set; } = default!;
        public string ServiceName { get; set; } = string.Empty; // Inițializare cu șir gol
        public decimal ServiceCost { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MakeupArtist = await _context.MakeupArtist
                .Include(m => m.Service)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (MakeupArtist == null)
            {
                return NotFound();
            }

            // Verifică dacă există un serviciu asociat
            if (MakeupArtist.Service != null)
            {
                ServiceName = MakeupArtist.Service.ServiceName;
                ServiceCost = MakeupArtist.Service?.Cost ?? 0;

            }

            return Page();
        }
    }
}
