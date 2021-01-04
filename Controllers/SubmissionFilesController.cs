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
    public class SubmissionFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmissionFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubmissionFiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubmissionFiles.Include(s => s.Owner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubmissionFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submissionFile = await _context.SubmissionFiles
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submissionFile == null)
            {
                return NotFound();
            }

            return View(submissionFile);
        }

        // GET: SubmissionFiles/Create
        public IActionResult Create()
        {
            ViewData["SubmissionId"] = new SelectList(_context.Submissions, "Id", "Id");
            return View();
        }

        // POST: SubmissionFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubmissionId,Id,Path")] SubmissionFile submissionFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(submissionFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubmissionId"] = new SelectList(_context.Submissions, "Id", "Id", submissionFile.SubmissionId);
            return View(submissionFile);
        }

        // GET: SubmissionFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submissionFile = await _context.SubmissionFiles.FindAsync(id);
            if (submissionFile == null)
            {
                return NotFound();
            }
            ViewData["SubmissionId"] = new SelectList(_context.Submissions, "Id", "Id", submissionFile.SubmissionId);
            return View(submissionFile);
        }

        // POST: SubmissionFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubmissionId,Id,Path")] SubmissionFile submissionFile)
        {
            if (id != submissionFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submissionFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionFileExists(submissionFile.Id))
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
            ViewData["SubmissionId"] = new SelectList(_context.Submissions, "Id", "Id", submissionFile.SubmissionId);
            return View(submissionFile);
        }

        // GET: SubmissionFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submissionFile = await _context.SubmissionFiles
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submissionFile == null)
            {
                return NotFound();
            }

            return View(submissionFile);
        }

        // POST: SubmissionFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submissionFile = await _context.SubmissionFiles.FindAsync(id);
            _context.SubmissionFiles.Remove(submissionFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionFileExists(int id)
        {
            return _context.SubmissionFiles.Any(e => e.Id == id);
        }
    }
}
