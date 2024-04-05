using hostel_management.Data;
using Microsoft.AspNetCore.Mvc;
using hostel_management.Models;
using Microsoft.AspNetCore.Http;

namespace hostel_management.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        private bool IsUserAuthenticatedAndAdmin()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            return !string.IsNullOrEmpty(userId) && role != null && role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        public IActionResult Index()
        {
            if (!IsUserAuthenticatedAndAdmin())
            {
                return RedirectToAction("Sign_in", "Auth");
            }
            // Fetch student data from the database
            var students = _context.Students.ToList();
            return View(students);
        }
        public IActionResult Add_student()
        {
            if (!IsUserAuthenticatedAndAdmin())
            {
                return RedirectToAction("Sign_in", "Auth");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Add_student(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the student already exists
                if (_context.Students.Any(s => s.EnrollmentNumber == model.EnrollmentNumber))
                {
                    ModelState.AddModelError("EnrollmentNumber", "Student already exists.");
                    return View("Add_student", model);
                }

                // Add the new student to the database
                _context.Students.Add(model);
                _context.SaveChanges();

                // Redirect to the Add_student page or any other appropriate page
                return RedirectToAction("Add_student");
            }

            // If the model state is not valid, return to the Add_student page with validation errors
            return View("Add_student", model);
        }


        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound(); // Return 404 Not Found if the student is not found
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            // Redirect to the Index page or any other appropriate page
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                var existingStudent = _context.Students.Find(model.StudentId);
                if (existingStudent == null)
                {
                    return NotFound();
                }

                // Update the existing student details
                existingStudent.FirstName = model.FirstName;
                existingStudent.LastName = model.LastName;
                existingStudent.Gender = model.Gender;
                existingStudent.EnrollmentNumber = model.EnrollmentNumber;
                existingStudent.MobileNumber = model.MobileNumber;
                existingStudent.RoomNumber = model.RoomNumber;
                existingStudent.HostelName = model.HostelName;
                existingStudent.DateOfBirth = model.DateOfBirth;
                existingStudent.Address = model.Address;

                _context.SaveChanges();

                // Redirect to the Index page or any other appropriate page
                return RedirectToAction("Index");
            }

            // If the model state is not valid, return to the EditStudent page with validation errors
            return View(model);
        }




        public IActionResult Rooms()
        {
            if (!IsUserAuthenticatedAndAdmin())
            {
                return RedirectToAction("Sign_in", "Auth");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Room(RoomModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the room already exists
                if (_context.Rooms.Any(r => r.HostelName == model.HostelName && r.RoomNumber == model.RoomNumber))
                {
                    ModelState.AddModelError("RoomNumber", "Room already exists.");
                    return View("Rooms", model);
                }

                // Add the new room to the database
                _context.Rooms.Add(model);
                _context.SaveChanges();

                // Redirect to the Rooms page or any other appropriate page
                return RedirectToAction("Rooms");
            }

            // If the model state is not valid, return to the Rooms page with validation errors
            return View("Rooms", model);
        }

    }
}
