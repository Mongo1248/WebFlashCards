using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebFlashCards.Data;
using WebFlashCards.Models;




namespace WebFlashCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly IConfiguration _configuration; //kkkkkkkkkkkk added for test
        private readonly WebFlashCardsContext _context;

        public CardsController(WebFlashCardsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            //return _context.Cards != null ?
            //          View(await _context.Cards.ToListAsync()) :
            //          Problem("Entity set 'WebFlashCardsContext.Cards'  is null.");

            if (_context.Cards != null)
            {
                var cards = await _context.Cards.ToListAsync();
                return View(cards);
            }
            else
            {
                return Problem("Entity set 'WebFlashCardsContext.Cards' is null.");
            }

        }



        // GET: Cards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.CardID == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            return View();
        }


        //[HttpGet("TestConnectionString")]
        //public IActionResult TestConnectionString()
        //{
        //    var connectionString = _configuration.GetConnectionString("WebFlashCardsContext");

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            return Ok("Connection successful");
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest($"Error: {ex.Message}");
        //        }
        //    }
        //}


        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardID,QuestionText,AnswerText,QuestionMedia,QuestionMediaType,AnswerMedia,AnswerMediaType,OwnerUserID")] Card card)
        {
            if (ModelState.IsValid)
            {
                card.CardID = Guid.NewGuid();
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CardID,QuestionText,AnswerText,QuestionMedia,QuestionMediaType,AnswerMedia,AnswerMediaType,OwnerUserID")] Card card)
        {
            if (id != card.CardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.CardID))
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
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.CardID == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Cards == null)
            {
                return Problem("Entity set 'WebFlashCardsContext.Cards'  is null.");
            }
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(Guid id)
        {
          return (_context.Cards?.Any(e => e.CardID == id)).GetValueOrDefault();
        }
    }
}
