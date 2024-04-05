using System;
using System.ComponentModel.DataAnnotations;

namespace hostel_management.Models
{
    public class StudentModel
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enrollment number is required")]
        public string EnrollmentNumber { get; set; }

        public string MobileNumber { get; set; }

        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Hostel name is required")]
        public string HostelName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}
