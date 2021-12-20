using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
       
        private readonly ILogger<HomeController> _logger;
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController (ILogger<HomeController> logger,IHtmlLocalizer<HomeController> localizer, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _localizer = localizer;
            _context = applicationDbContext;
        }
        //public HomeController()
        //{
        //    // Bu veritabanına bağlanmak için oradaki tablolara erişebilicez.
            
        //}

        public IActionResult Index()
        {
            //return View();
            var test = _localizer["HelloWorld"];
            ViewData["HelloWorld"] = test;
            var result = _context.Blog.Include(s => s.ApplicationUser);
            return View(result.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CultureMangement(string culture,string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return LocalRedirect(returnUrl);


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
