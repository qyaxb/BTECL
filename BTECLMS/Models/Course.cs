using System.ComponentModel.DataAnnotations;

namespace Btec_Website.Models
{
    public class Course
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsVisibleToStudents { get; set; }

        public bool IsVisibleToTeachers { get; set; }
    }
}
