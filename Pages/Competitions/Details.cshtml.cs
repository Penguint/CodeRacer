using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeRacer.Data;
using CodeRacer.Models;

namespace CodeRacer.Pages.Competitions
{
    public class DetailsModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public DetailsModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Competition Competition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions.FirstOrDefaultAsync(m => m.Id == id);

            if (Competition == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
