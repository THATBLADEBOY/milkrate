using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using milkrate.Data;
using milkrate.Models;

namespace milkrate.Controllers
{
    public class PiecesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PiecesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pieces
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var pieces = from p in _context.Piece
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                pieces = pieces.Where(p => p.Title.Contains(searchString)
                                       || p.PieceType.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    pieces = pieces.OrderByDescending(p => p.Title);
                    break;
                case "Date":
                    pieces = pieces.OrderBy(p => p.ReleaseDate);
                    break;
                case "date_desc":
                    pieces = pieces.OrderByDescending(p => p.ReleaseDate);
                    break;
                default:
                    pieces = pieces.OrderBy(p => p.ReleaseDate);
                    break;
            }

            return View(await pieces.AsNoTracking().ToListAsync());
        }

        // GET: Pieces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Piece
                .FirstOrDefaultAsync(m => m.ID == id);
            if (piece == null)
            {
                return NotFound();
            }

            return View(piece);
        }

        // GET: Pieces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pieces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,PieceType,RetailPrice,AveragePrice,Premium,LastSale,PageLink,ImageLink,ReleaseDate")] Piece piece)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piece);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piece);
        }

        // GET: Pieces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Piece.FindAsync(id);
            if (piece == null)
            {
                return NotFound();
            }
            return View(piece);
        }

        // POST: Pieces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,PieceType,RetailPrice,AveragePrice,Premium,LastSale,PageLink,ImageLink,ReleaseDate")] Piece piece)
        {
            if (id != piece.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PieceExists(piece.ID))
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
            return View(piece);
        }

        // GET: Pieces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Piece
                .FirstOrDefaultAsync(m => m.ID == id);
            if (piece == null)
            {
                return NotFound();
            }

            return View(piece);
        }

        // POST: Pieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var piece = await _context.Piece.FindAsync(id);
            _context.Piece.Remove(piece);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PieceExists(int id)
        {
            return _context.Piece.Any(e => e.ID == id);
        }
    }
}
