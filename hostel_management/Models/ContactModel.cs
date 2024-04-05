using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hostel_management.Models
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }  // Auto-incremented by the database

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Inquiry type is required")]
        public string InquiryType { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }

}
