using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Btec_Website.Models
{
    public class UserCourse
    {
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CourseId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Course Course { get; set; }
    }
}