using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            // Bu veritabanına bağlanmak için oradaki tablolara erişebilicez.
            _context = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
            int deneme = 5;
            //var result = _context.Blog.Include(s => s.Student);
            //return View(result.ToList());
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
