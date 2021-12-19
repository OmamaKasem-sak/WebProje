//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using WebProje.Data;
//using WebProje.Models;

//namespace WebProje.Controllers
//{
//    public class StudentController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public StudentController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Student
//        public async Task<IActionResult> Index()
//        {
//            var applicationDbContext = _context.Student.Include(s => s.Department).Include(s => s.Homeland);
//            return View(await applicationDbContext.ToListAsync());
//        }

//        // GET: Student/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.Student
//                .Include(s => s.Department)
//                .Include(s => s.Homeland)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (student == null)
//            {
//                return NotFound();
//            }

//            return View(student);
//        }

//        // GET: Student/Create
//        public IActionResult Create()
//        {
//            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id");
//            ViewData["HomelandId"] = new SelectList(_context.Homeland, "Id", "Id");
//            return View();
//        }

//        // POST: Student/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthday,PlaceOfBirth,University,DepartmentId,HomelandId")] Student student)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(student);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", student.DepartmentId);
//            ViewData["HomelandId"] = new SelectList(_context.Homeland, "Id", "Id", student.HomelandId);
//            return View(student);
//        }

//        // GET: Student/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.Student.FindAsync(id);
//            if (student == null)
//            {
//                return NotFound();
//            }
//            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", student.DepartmentId);
//            ViewData["HomelandId"] = new SelectList(_context.Homeland, "Id", "Id", student.HomelandId);
//            return View(student);
//        }

//        // POST: Student/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Birthday,PlaceOfBirth,University,DepartmentId,HomelandId")] Student student)
//        {
//            //if (id != student.Id)
//            //{
//            //    return NotFound();
//            //}

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(student);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    //if (!StudentExists(student.Id))
//                    //{
//                    //    return NotFound();
//                    //}
//                    //else
//                    //{
//                    //    throw;
//                    //}
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", student.DepartmentId);
//            ViewData["HomelandId"] = new SelectList(_context.Homeland, "Id", "Id", student.HomelandId);
//            return View(student);
//        }

//        // GET: Student/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.Student
//                .Include(s => s.Department)
//                .Include(s => s.Homeland)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (student == null)
//            {
//                return NotFound();
//            }

//            return View(student);
//        }

//        // POST: Student/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var student = await _context.Student.FindAsync(id);
//            _context.Student.Remove(student);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool StudentExists(int id)
//        {
//            return _context.Student.Any(e => e.Id == id);
//        }
//    }
//}
