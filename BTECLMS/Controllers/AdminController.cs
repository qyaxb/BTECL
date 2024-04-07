using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Btec_Website.Models;
using Btec_Website.ViewModels;

namespace Btec_Website.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action for displaying list of users
        public async Task<IActionResult> Index()
{
    // Retrieve the list of users from the database
    var users = await _context.Users.ToListAsync();

    // Map the list of users to a list of UserViewModels
    var userViewModels = users.Select(u => new UserViewModel
    {
        Id = u.Id,
        Email = u.Email,
        UserName = u.UserName,
        Role = u.Role,
        // Map other properties as needed
    }).ToList();

    return View(userViewModels);
}


        // Action for adding a new user
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    // Set other properties as needed
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // If ModelState is invalid, return to the add view with validation errors
            return View(model);
        }

        // Action for editing an existing user
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user from the database
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userViewModel.Id);

                // Check if the user exists
                if (user == null)
                {
                    return NotFound();
                }

                // Update user properties with the values from the view model
                user.Email = userViewModel.Email;
                user.UserName = userViewModel.UserName;
                user.Role = userViewModel.Role;

                // Save changes to the database
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                // Redirect to a different action after successful edit
                return RedirectToAction("Index"); // Assuming you want to redirect to the user list page
            }

            // If model state is not valid, return the edit view with the view model to display validation errors
            return View(userViewModel);
        }






        // Action for deleting a user
        public async Task<IActionResult> Delete(UserViewModel userViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userViewModel.Id);
            if (user == null)
            {
                return NotFound();
            }
            return View(userViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(UserViewModel userViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userViewModel.Id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
