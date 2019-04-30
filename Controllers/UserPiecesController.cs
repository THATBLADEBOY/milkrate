using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using milkrate.Data;
using milkrate.Models;

namespace milkrate.Controllers
{
    public class UserPiecesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserPiecesController(ApplicationDbContext ctx,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: UserPieces
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.UserPiece.Where(p => p.UserId == user.Id);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserPieces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPiece = await _context.UserPiece
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPiece == null)
            {
                return NotFound();
            }

            return View(userPiece);
        }

        // GET: UserPieces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserPieces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConditionId,UserId,PieceId,Value")] UserPiece userPiece)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userPiece);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userPiece);
        }

        // GET: UserPieces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPiece = await _context.UserPiece.FindAsync(id);
            if (userPiece == null)
            {
                return NotFound();
            }
            return View(userPiece);
        }

        // POST: UserPieces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConditionId,UserId,PieceId,Value")] UserPiece userPiece)
        {
            if (id != userPiece.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPiece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPieceExists(userPiece.Id))
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
            return View(userPiece);
        }

        // GET: UserPieces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPiece = await _context.UserPiece
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPiece == null)
            {
                return NotFound();
            }

            return View(userPiece);
        }

        // POST: UserPieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userPiece = await _context.UserPiece.FindAsync(id);
            _context.UserPiece.Remove(userPiece);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPieceExists(int id)
        {
            return _context.UserPiece.Any(e => e.Id == id);
        }
    }
}
