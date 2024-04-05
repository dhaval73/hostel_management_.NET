using hostel_management.Models;
using Microsoft.AspNetCore.Mvc;
using hostel_management.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;



namespace hostel_management.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Sign_in()
        {
            var model = new SignInModel(); // Create an instance of SignInModel
            return View(model); // Pass the model to the view
        }


        [HttpPost]
        public IActionResult Sign_in(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                // Find user by email
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    // Verify password
                    if (VerifyPassword(model.Password, existingUser.Password))
                    {
                        // Password is correct, set session variables and redirect
                        HttpContext.Session.SetString("UserId", existingUser.Id.ToString());
                        HttpContext.Session.SetString("Role", existingUser.Role.ToString());
                        HttpContext.Session.SetString("Username", "Dhaval");

                        // Redirect to user dashboard or any desired page
                        return RedirectToAction("Index", "User");
                    }
                }

                // User not found or incorrect password
                ModelState.AddModelError("Password", "Invalid email or password");
                return View(model);
            }

            // Model state is not valid
            return View(model);
        }

        public IActionResult Sign_up()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sign_up(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(model);
                }

                string hashedPassword = HashPassword(model.Password);

                var newUser = new SignUpModel
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = hashedPassword
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Sign_in");
            }

            return View(model);
        }

        public IActionResult Sign_out()
        {
            // Clear session variables
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Username");

            // Redirect to a desired page (e.g., sign-in page)
            return RedirectToAction("Sign_in");
        }


        private string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password using PBKDF2
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Combine the salt and hashed password
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            // Split the stored password into salt and hashed password
            string[] parts = hashedPassword.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedHash = parts[1];

            // Compute the hash of the entered password using the same salt
            string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Compare the computed hash with the stored hash
            return storedHash == computedHash;
        }

    }
}
