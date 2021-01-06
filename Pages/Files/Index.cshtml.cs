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
    public class IndexModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public IndexModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MyFile> MyFile { get;set; }

        public async Task OnGetAsync()
        {
            MyFile = await _context.Files.ToListAsync();
        }
    }
}
