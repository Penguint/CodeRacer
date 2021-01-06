using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeRacer.Data;
using CodeRacer.Models;

namespace CodeRacer.Pages.SubmissionFiles
{
    public class EditModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public EditModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubmissionFile SubmissionFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubmissionFile = await _context.SubmissionFiles
                .Include(s => s.Owner).FirstOrDefaultAsync(m => m.Id == id);

            if (SubmissionFile == null)
            {
                return NotFound();
            }
           ViewData["SubmissionId"] = new SelectList(_context.Submissions, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SubmissionFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmissionFileExists(SubmissionFile.Id))
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

        private bool SubmissionFileExists(int id)
        {
            return _context.SubmissionFiles.Any(e => e.Id == id);
        }
    }
}
