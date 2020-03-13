using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meds_Server.Model;

namespace Meds_Server
{
    public class EditModel : PageModel
    {
        private readonly Meds_Server.Model.MedsServerContext _context;

        public EditModel(Meds_Server.Model.MedsServerContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Meds).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedsExists(Meds.Id))
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

        private bool MedsExists(int id)
        {
            return _context.Meds.Any(e => e.Id == id);
        }
    }
}
