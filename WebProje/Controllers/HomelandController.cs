using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class HomelandController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomelandController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Homeland
        public async Task<IActionResult> Index()
        {
            return View(await _context.Homeland.ToListAsync());
        }

        // GET: Homeland/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeland = await _context.Homeland
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeland == null)
            {
                return NotFound();
            }

            return View(homeland);
        }

        // GET: Homeland/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homeland/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Space,Population,Climate")] Homeland homeland)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeland);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeland);
        }

        // GET: Homeland/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeland = await _context.Homeland.FindAsync(id);
            if (homeland == null)
            {
                return NotFound();
            }
            return View(homeland);
        }

        // POST: Homeland/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Space,Population,Climate")] Homeland homeland)
        {
            if (id != homeland.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeland);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomelandExists(homeland.Id))
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
            return View(homeland);
        }

        // GET: Homeland/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeland = await _context.Homeland
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeland == null)
            {
                return NotFound();
            }

            return View(homeland);
        }

        // POST: Homeland/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeland = await _context.Homeland.FindAsync(id);
            _context.Homeland.Remove(homeland);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomelandExists(int id)
        {
            return _context.Homeland.Any(e => e.Id == id);
        }
    }
}
