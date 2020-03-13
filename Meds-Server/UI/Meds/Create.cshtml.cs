using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Meds_Server.Model;

namespace Meds_Server
{
    public class CreateModel : PageModel
    {
        private readonly Meds_Server.Model.MedsServerContext _context;

        public CreateModel(Meds_Server.Model.MedsServerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Meds Meds { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Meds.Add(Meds);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
