using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeRacer.Data;
using CodeRacer.Models;

namespace CodeRacer.Pages.Competitions
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
            return Page();
        }

        [BindProperty]
        public Competition Competition { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Competitions.Add(Competition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
