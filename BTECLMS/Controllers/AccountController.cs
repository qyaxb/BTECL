using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Btec_Website.ViewModels;
using Btec_Website.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
namespace Btec_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
    
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    // Add a model state error for the email field
                    ModelState.AddModelError(nameof(RegisterViewModel.Email), "Email is already in use.");
                    // Return the view with the model to display the error message
                    return View(model);
                }
                // Create a new User object with the provided properties
                var user = new User { UserName = model. UserName, Email = model.Email, Password = model.Password, Role = model.Role };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to login or home page after registration
                return RedirectToAction("Login", "Account");
            }

            // If registration fails or ModelState is not valid, redisplay the registration form with validation errors
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        private bool IsValidUser(string email, string password)
        {
            // Perform authentication logic here (e.g., check credentials against a database)
            // Return true if the user is valid, false otherwise
            // This is just a placeholder implementation
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                // Check if the provided password matches the user's password
                return user.Password == password;
            }
            return false; // User not found or password doesn't match
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
               
              
                    // Retrieve the user from the database
                    var user = _context.Users.FirstOrDefault(u => u.Email == email);
                    if (user != null)
        {
            // Check if the password is correct
            if (user.Password == password)
            {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    // Add more claims as needed, e.g., roles
                };

                        var claimsIdentity = new ClaimsIdentity(claims, "login");

                        // Create a principal with the claims
                        var principal = new ClaimsPrincipal(claimsIdentity);

                        // Sign in the user
                        await HttpContext.SignInAsync(principal);
                        // Set session variables with the username and role
                        HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
             
                // Redirect to the home page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Password is incorrect
                ViewBag.ErrorMessage = "Invalid password";
                return View();
            }
        }
        else
        {
            // User with the provided email does not exist
            ViewBag.ErrorMessage = "User with the provided email does not exist";
            return View();
        }
                
            }

            // If login fails, display an error message
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear the session
            return RedirectToAction("Index", "Home"); // Redirect to home page after logout
        }

    }
}