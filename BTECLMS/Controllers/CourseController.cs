using Microsoft.AspNetCore.Mvc;

using Btec_Website.ViewModels;
using Btec_Website;
using Btec_Website.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
// You may need to adjust the namespace

namespace Btec_Website.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Course
        public IActionResult Courses()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currentUserRole = UserRole.Student; // Get the current user's role (manually or through some other means)
            var courses = _context.Courses.ToList();

            switch (currentUserRole)
            {
                case UserRole.Student:
                    courses = courses.Where(c => c.IsVisibleToStudents).ToList();
                    break;
                case UserRole.Teacher:
                    courses = courses.Where(c => c.IsVisibleToTeachers).ToList();
                    break;
                case UserRole.Admin:
                    // Admin has access to all courses
                    break;
                default:
                    // Handle unknown role
                    break;
            }

            return View(courses);
        }


        // GET: /Course/Add
        
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Course/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public IActionResult Add(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Courses));
            }
            return View(course);
        }

        // GET: /Course/Edit/5

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: /Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Teacher, Admin")]
        public IActionResult Edit(int id, Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Courses));
            }
            return View(course);
        }

        // POST: /Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Courses));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Enroll(int courseId)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            // Check if the user is a student
            if (currentUser != null && currentUser.Role == UserRole.Student)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course != null)
                {
                    // Check if the user is already enrolled in the course
                    var isEnrolled = await _context.UserCourse.AnyAsync(uc => uc.UserId == currentUser.Id && uc.CourseId == courseId);
                    if (!isEnrolled)
                    {
                        var userCourse = new UserCourse { UserId = currentUser.Id, CourseId = courseId };
                        _context.UserCourse.Add(userCourse);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}