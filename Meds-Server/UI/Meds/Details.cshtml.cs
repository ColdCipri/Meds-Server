using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Meds_Server.Model;

namespace Meds_Server
{
    public class DetailsModel : PageModel
    {
        private readonly Meds_Server.Model.MedsServerContext _context;

        public DetailsModel(Meds_Server.Model.MedsServerContext context)
        {
            _context = context;
        }

        public Meds Meds { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meds = await _context.Meds.FirstOrDefaultAsync(m => m.Id == id);

            if (Meds == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
