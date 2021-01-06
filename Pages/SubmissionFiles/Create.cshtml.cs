using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeRacer.Data;
using CodeRacer.Models;

namespace CodeRacer.Pages.SubmissionFiles
{
    public class CreateModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public CreateModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SubmissionId"] = new SelectList(_context.Submissions, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public SubmissionFile SubmissionFile { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SubmissionFiles.Add(SubmissionFile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
