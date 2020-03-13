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
    public class IndexModel : PageModel
    {
        private readonly Meds_Server.Model.MedsServerContext _context;

        public IndexModel(Meds_Server.Model.MedsServerContext context)
        {
            _context = context;
        }

        public IList<Meds> Meds { get;set; }

        public async Task OnGetAsync()
        {
            Meds = await _context.Meds.ToListAsync();
        }
    }
}
