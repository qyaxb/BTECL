using Btec_Website.Models;
using Btec_Website.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Btec_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.Session.GetString("UserName");
            
           
            var isLoggedIn = User.Identity.IsAuthenticated;
            
            if (isLoggedIn)
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                // Check if the currentUser object is not null
                if (currentUser != null)
                {
                    var courses = await _context.Courses.ToListAsync();

                    // Populate a list of CourseViewModel objects
                    var courseViewModels = new List<CourseViewModel>();
                    foreach (var course in courses)
                    {
                        var isEnrolled = IsUserEnrolled(currentUser.Id, course.Id);
                        courseViewModels.Add(new CourseViewModel { Course = course, IsEnrolled = isEnrolled });
                    }

                    // Pass the courseViewModels to the view
                    return View(courseViewModels);
                }
                else
                {
                    // Handle the case where user information couldn't be retrieved
                    // Redirect the user to an error page or take appropriate action
                    return RedirectToAction("Error");
                }
            }
            else
            {
                // Redirect the user to the login page if they are not authenticated
                return RedirectToAction("Login", "Account");
            }

        }
        private bool IsUserEnrolled(int userId, int courseId)
        {
            // Check if the user is enrolled in the specified course
            return _context.UserCourse.Any(uc => uc.UserId == userId && uc.CourseId == courseId);
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
