using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Reflection;
using WebProje.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace WebProje.Controllers
{
    public class BlogController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public BlogController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Blog.Include(b => b.ApplicationUser).Where(u => u.ApplicationUser.Id == _userManager.GetUserId(User));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            ViewData["ApplicationUserId"] = _userManager.GetUserId(User);

            return View(blog);
        }

        // GET: Blog/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = _userManager.GetUserId(User);
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogViewModel blogViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(blogViewModel);

                Blog blog = new Blog
                {
                    Title = blogViewModel.Title,
                    Image = uniqueFileName,
                    Content = blogViewModel.Content,
                    CreateDate = DateTime.Now,
                    ApplicationUserId = blogViewModel.ApplicationUserId
                };

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ApplicationUserId"] = _userManager.GetUserId(User);

            return View(blogViewModel);
        }

        private string UploadedFile(BlogViewModel blog)
        {
            string uniqueFileName = null;

            if (blog.Image != null)
            {
                string uploadsFolder = Environment.CurrentDirectory + @"\wwwroot\images\blog";
                uniqueFileName = Guid.NewGuid().ToString() + "_" + blog.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    blog.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.Where(u => u.ApplicationUserId == _userManager.GetUserId(User))
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            ViewData["ApplicationUserId"] = _userManager.GetUserId(User);
            ViewData["ImageName"] = blog.Image;


            BlogViewModel blogViewModel = new BlogViewModel
            {
                ApplicationUserId = blog.ApplicationUserId,
                Content = blog.Content,
                Id = blog.Id,
                CreateDate = blog.CreateDate,
                Title = blog.Title,
                ImageName = blog.Image
            };

            return View(blogViewModel);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string imageName, BlogViewModel blogViewModel)
        {
            if (id != blogViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (blogViewModel.Image != null)
                    {
                        string uniqueFileName = UploadedFile(blogViewModel);

                        Blog blog = new Blog
                        {
                            Id = blogViewModel.Id,
                            Title = blogViewModel.Title,
                            Image = uniqueFileName,
                            Content = blogViewModel.Content,
                            CreateDate = blogViewModel.CreateDate,
                            ApplicationUserId = blogViewModel.ApplicationUserId
                        };

                        _context.Update(blog);
                    }

                //    else
                //    {
                //        var blog = await _context.Blog.Where(u => u.ApplicationUserId == _userManager.GetUserId(User))
                //.FirstOrDefaultAsync(m => m.Id == id);

                //        if (blog == null)
                //        {
                //            return NotFound();
                //        }

                //        Blog blogModel = new Blog
                //        {
                //            Title = blogViewModel.Title,
                //            Image = blogViewModel.ImageName,
                //            Content = blogViewModel.Content,
                //            CreateDate = blogViewModel.CreateDate,
                //            ApplicationUserId = blogViewModel.ApplicationUserId
                //        };
                //        _context.Update(blogModel);
                //    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blogViewModel.Id))
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

            ViewData["ApplicationUserId"] = _userManager.GetUserId(User);

            return View(blogViewModel);
        }

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .Include(b => b.ApplicationUser).Where(u => u.ApplicationUserId == _userManager.GetUserId(User))
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blog.Where(u => u.ApplicationUserId == _userManager.GetUserId(User))
                .FirstOrDefaultAsync(m => m.Id == id);
            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.Id == id);
        }
    }
}
