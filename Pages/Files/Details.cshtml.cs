using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeRacer.Data;
using CodeRacer.Models;

namespace CodeRacer.Pages.Files
{
    public class DetailsModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public DetailsModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public MyFile MyFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MyFile = await _context.Files.FirstOrDefaultAsync(m => m.Id == id);

            if (MyFile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
