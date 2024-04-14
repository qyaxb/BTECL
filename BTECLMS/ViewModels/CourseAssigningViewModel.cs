using Btec_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Btec_Website.ViewModels
{
    public class CourseAssigningViewModel : Controller
    {
        public Course Course { get; set; }
        public int Id { get; set; } // Assuming Id is the property representing the course ID
        public string Description { get; set; } // Add other course properties as needed
        public List<User> StudentsNotEnrolled { get; set; }
        public List<User> EnrolledStudents { get; set; }
        public IActionResult Index()
        {
            return View();
        }
    }
    
    
}
