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
    public class DeleteModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public DeleteModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MyFile = await _context.Files.FindAsync(id);

            if (MyFile != null)
            {
                _context.Files.Remove(MyFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
