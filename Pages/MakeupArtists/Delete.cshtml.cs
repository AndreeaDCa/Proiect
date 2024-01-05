using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.MakeupArtists
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public DeleteModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MakeupArtist MakeupArtist { get; set; } = default!;
        public string ServiceName { get; set; } = string.Empty; // Inițializare cu șir gol
        public decimal ServiceCost { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MakeupArtist == null)
            {
                return NotFound();
            }

            var makeupartist = await _context.MakeupArtist
                .Include(m => m.Service)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (makeupartist == null)
            {
                return NotFound();
            }
            else 
            {
                MakeupArtist = makeupartist;
                ServiceName = makeupartist.Service?.ServiceName;
                ServiceCost = makeupartist.ServiceCost;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MakeupArtist == null)
            {
                return NotFound();
            }
            var makeupartist = await _context.MakeupArtist.FindAsync(id);

            if (makeupartist != null)
            {
                MakeupArtist = makeupartist;
                _context.MakeupArtist.Remove(MakeupArtist);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
