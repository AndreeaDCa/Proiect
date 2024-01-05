using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
          
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IList<Appointment> Appointments { get; set; }

        public async Task OnGetAsync()
        {
            var appointmentsQuery = _context.Appointments
                .Include(a => a.MakeupArtist)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                appointmentsQuery = appointmentsQuery.Where(a => EF.Functions.Like((a.FirstName + " " + a.LastName), $"%{SearchString}%"));

            }

            Appointments = await appointmentsQuery.ToListAsync();
        
    }
    }
}
