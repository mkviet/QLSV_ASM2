using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSVVV.Data;
using QLSVVV.Models;


namespace QLSVVV.Controllers
{
    /*  [Authorize(Roles = "Admin")]
      [Authorize(Roles = "Student")]
      [Authorize(Roles = "Teacher")]*/
  
    public class CoursesController : Controller
    {
        private readonly QLSVVVContext _context;

        public CoursesController(QLSVVVContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
              return _context.Course != null ? 
                          View(await _context.Course.ToListAsync()) :
                          Problem("Entity set 'QLSVVVContext.Course'  is null.");
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Class,DateStart,DateEnd,Major,Lecturer")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageCourse));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Class,DateStart,DateEnd,Major,Lecturer")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ManageCourse));
            }
            return View(course);
        }
        public IActionResult ViewStudentlnCourse(int id)
        {
            var course = _context.Course
                .Include(c => c.CourseStudent)
                .ThenInclude(cs => cs.Student)
                .FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            ViewBag.AllStudents = _context.Student.ToList();

            return View(course);
        }

        public IActionResult EditCourse(int id)
        {
            var course = _context.Course
                .Include(c => c.CourseStudent)
                .ThenInclude(cs => cs.Student)
                .FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            ViewBag.AllStudents = _context.Student.ToList();

            return View(course);
        }

        [HttpPost]
        public IActionResult AddStudentToCourse(int courseId, int studentId)
        {
            var course = _context.Course
                .Include(c => c.CourseStudent)
                .FirstOrDefault(c => c.Id == courseId);

            var student = _context.Student.FirstOrDefault(s => s.Id == studentId);

            if (course != null && student != null)
            {
                course.CourseStudent.Add(new CourseStudent { CourseId = courseId, StudentId = studentId });
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(EditCourse), new { id = courseId });
        }

        [HttpPost]
        public IActionResult RemoveStudentFromCourse(int courseId, int studentId)
        {
            var course = _context.Course
                .Include(c => c.CourseStudent)
                .FirstOrDefault(c => c.Id == courseId);

            var student = _context.Student.FirstOrDefault(s => s.Id == studentId);

            if (course != null && student != null)
            {
                var courseStudent = course.CourseStudent.FirstOrDefault(cs => cs.StudentId == studentId);
                if (courseStudent != null)
                {
                    course.CourseStudent.Remove(courseStudent);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction(nameof(EditCourse), new { id = courseId });
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'QLSVVVContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageCourse));
        }

        private bool CourseExists(int id)
        {
          return (_context.Course?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> ManageCourse()
        {
            return View(await _context.Course.ToListAsync());
        }
        
        public async Task<IActionResult> ViewCourse()
        {
            return View(await _context.Course.ToListAsync());
        }
        public async Task<IActionResult> ViewCourseStudent()
        {
            return View(await _context.Course.ToListAsync());
        }
        
        
    }
}
