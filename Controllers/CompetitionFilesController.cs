using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeRacer.Data;
using CodeRacer.Models;

namespace CodeRacer.Controllers
{
    public class CompetitionFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetitionFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CompetitionFiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CompetitionFiles.Include(c => c.Owner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CompetitionFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionFile = await _context.CompetitionFiles
                .Include(c => c.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competitionFile == null)
            {
                return NotFound();
            }

            return View(competitionFile);
        }

        // GET: CompetitionFiles/Create
        public IActionResult Create()
        {
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", nameof(Competition.Title));
            return View();
        }

        // POST: CompetitionFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetitionId,Id,Path")] CompetitionFile competitionFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competitionFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", "Id", competitionFile.CompetitionId);
            return View(competitionFile);
        }

        // GET: CompetitionFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionFile = await _context.CompetitionFiles.FindAsync(id);
            if (competitionFile == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", nameof(Competition.Title), competitionFile.CompetitionId);
            return View(competitionFile);
        }

        // POST: CompetitionFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetitionId,Id,Path")] CompetitionFile competitionFile)
        {
            if (id != competitionFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competitionFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionFileExists(competitionFile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", "Id", competitionFile.CompetitionId);
            return View(competitionFile);
        }

        // GET: CompetitionFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionFile = await _context.CompetitionFiles
                .Include(c => c.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competitionFile == null)
            {
                return NotFound();
            }

            return View(competitionFile);
        }

        // POST: CompetitionFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competitionFile = await _context.CompetitionFiles.FindAsync(id);
            _context.CompetitionFiles.Remove(competitionFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionFileExists(int id)
        {
            return _context.CompetitionFiles.Any(e => e.Id == id);
        }
    }
}
