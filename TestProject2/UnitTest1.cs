using Btec_Website;
using Btec_Website.Controllers;
using Btec_Website.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class WebsiteUnitTest
{
    [Test]
    public async Task Test_User_Authentication()
    {
        var dbContext = new ApplicationDbContext();
        var controller = new AccountController(dbContext); // Assuming AccountController is your authentication controller
        var email = "admin@admin";
        var password = "123";

        // Act
        var result = await controller.Login(email, password) as RedirectToActionResult;

        // Assert
        Assert.AreEqual("Index", result.ActionName);
        // Add more assertions as needed
    }

    [Test]
    public async Task Test_Course_Enrollment()
    {
        // Arrange
        var dbContext = new ApplicationDbContext(/* pass any required options if needed */);
        var controller = new CourseController(dbContext);// Assuming CourseController is your course management controller
        var courseId = 1;

        // Act
        var result = await controller.Enroll(courseId) as RedirectToActionResult;

        // Assert
        Assert.AreEqual("Index", result.ActionName);
        // Add more assertions as needed
    }

    [Test]
    public async Task Test_User_Deletion()
    {
        var dbContext = new ApplicationDbContext(/* pass any required options if needed */);
        var controller = new AdminController(dbContext); // Assuming AdminController is your admin management controller
        var userId = 1;

        // Act: Retrieve the user from the database
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        // Check if the user exists
        if (user != null)
        {
            // Call the Delete method with the retrieved user object
            var userViewModel = new Btec_Website.ViewModels.UserViewModel
            {
                // Map properties from user to userViewModel
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Role = user.Role,
                // Map other properties as needed
            };
            var result = await controller.Delete(userViewModel) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", result.ActionName);
            // Add more assertions as needed
        }
        else
        {
            // Handle the case where the user with the given ID doesn't exist
            Assert.Fail($"User with ID {userId} does not exist.");
        }
    }

    [Test]
    public async Task Test_Course_Creation()
    {
        // Arrange
        var dbContext = new ApplicationDbContext(/* pass any required options if needed */);
        var controller = new CourseController(dbContext); // Assuming CourseController is your course management controller
        var course = new Course { Name = "Test Course", Description = "This is a test course" };

        // Act
        var result = controller.Add(course) as RedirectToActionResult;

        // Assert
        Assert.AreEqual("Index", result.ActionName);
        // Add more assertions as needed
    }

    // Add similar methods for testing other functionalities like admin access, course deletion, and user registration
}
