using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFlashCards.Data;
using WebFlashCards.Models;

namespace WebFlashCards.Controllers
{
    public class AnswerScoresController : Controller
    {
        private readonly WebFlashCardsContext _context;

        public AnswerScoresController(WebFlashCardsContext context)
        {
            _context = context;
        }

        // GET: AnswerScores
        public async Task<IActionResult> Index()
        {
              return _context.AnswerScores != null ? 
                          View(await _context.AnswerScores.ToListAsync()) :
                          Problem("Entity set 'WebFlashCardsContext.AnswerScores'  is null.");
        }

        // GET: AnswerScores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AnswerScores == null)
            {
                return NotFound();
            }

            var answerScore = await _context.AnswerScores
                .FirstOrDefaultAsync(m => m.AnswerScoreId == id);
            if (answerScore == null)
            {
                return NotFound();
            }

            return View(answerScore);
        }

        // GET: AnswerScores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnswerScores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerScoreId,CardID,ScoreCount")] AnswerScore answerScore)
        {
            if (ModelState.IsValid)
            {
                answerScore.AnswerScoreId = Guid.NewGuid();
                _context.Add(answerScore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(answerScore);
        }

        // GET: AnswerScores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AnswerScores == null)
            {
                return NotFound();
            }

            var answerScore = await _context.AnswerScores.FindAsync(id);
            if (answerScore == null)
            {
                return NotFound();
            }
            return View(answerScore);
        }

        // POST: AnswerScores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AnswerScoreId,CardID,ScoreCount")] AnswerScore answerScore)
        {
            if (id != answerScore.AnswerScoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answerScore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerScoreExists(answerScore.AnswerScoreId))
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
            return View(answerScore);
        }

        // GET: AnswerScores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AnswerScores == null)
            {
                return NotFound();
            }

            var answerScore = await _context.AnswerScores
                .FirstOrDefaultAsync(m => m.AnswerScoreId == id);
            if (answerScore == null)
            {
                return NotFound();
            }

            return View(answerScore);
        }

        // POST: AnswerScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AnswerScores == null)
            {
                return Problem("Entity set 'WebFlashCardsContext.AnswerScores'  is null.");
            }
            var answerScore = await _context.AnswerScores.FindAsync(id);
            if (answerScore != null)
            {
                _context.AnswerScores.Remove(answerScore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerScoreExists(Guid id)
        {
          return (_context.AnswerScores?.Any(e => e.AnswerScoreId == id)).GetValueOrDefault();
        }
    }
}
