using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeRacer.Data;
using CodeRacer.Models;

namespace CodeRacer.Pages.CompetitionFiles
{
    public class DetailsModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public DetailsModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CompetitionFile CompetitionFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CompetitionFile = await _context.CompetitionFiles
                .Include(c => c.Owner).FirstOrDefaultAsync(m => m.Id == id);

            if (CompetitionFile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
