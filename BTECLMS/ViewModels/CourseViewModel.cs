using Btec_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Btec_Website.ViewModels
{
    public class CourseViewModel : Controller
    {
        public Course Course { get; set; }
        public bool IsEnrolled { get; set; }
        public IActionResult Index()
        {
            return View();
        }
    }
}
