using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeRacer.Data;
using CodeRacer.Models;

namespace CodeRacer.Pages.Submissions
{
    public class DeleteModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public DeleteModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Submission Submission { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Submission = await _context.Submissions
                .Include(s => s.Owner).FirstOrDefaultAsync(m => m.Id == id);

            if (Submission == null)
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

            Submission = await _context.Submissions.FindAsync(id);

            if (Submission != null)
            {
                _context.Submissions.Remove(Submission);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
