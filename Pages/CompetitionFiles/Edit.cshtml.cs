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

namespace CodeRacer.Pages.CompetitionFiles
{
    public class EditModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public EditModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", "Id");
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

            _context.Attach(CompetitionFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionFileExists(CompetitionFile.Id))
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

        private bool CompetitionFileExists(int id)
        {
            return _context.CompetitionFiles.Any(e => e.Id == id);
        }
    }
}
