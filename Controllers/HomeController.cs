using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using milkrate.Data;
using milkrate.Models;

namespace milkrate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext ctx,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User); 

        public async Task<IActionResult> Index()
        {

            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.UserPiece.Include(p => p.Piece).Where(p => p.UserId == user.Id);

            List<int> Values = new List<int> { };
            List<string> Titles = new List<string> { };

            foreach (var value in applicationDbContext)
            {
                if (value.Piece.AveragePrice != 0)
                {
                    Values.Add(value.Piece.AveragePrice);
                } else
                {
                    Values.Add(value.Piece.RetailPrice);
                };
            }
            foreach (var value in applicationDbContext)
            {
                Titles.Add(value.Piece.Title);
            }
            ViewData["FieldsList"] = Values;
            ViewData["FieldsList2"] = Titles;

            ViewData["FieldsList3"] = _context.Piece.OrderByDescending(p => p.ReleaseDate).Take(4).ToList();

            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
