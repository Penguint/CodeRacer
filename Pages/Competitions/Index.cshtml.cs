﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly CodeRacer.Data.ApplicationDbContext _context;

        public IndexModel(CodeRacer.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Competition> Competition { get;set; }

        public async Task OnGetAsync()
        {
            Competition = await _context.Competitions.ToListAsync();
        }
    }
}
