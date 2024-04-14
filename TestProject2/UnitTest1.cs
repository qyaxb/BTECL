using Btec_Website;
using Btec_Website.Controllers;
using Btec_Website.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using Btec_Website.ViewModels;

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
        var controller = new CourseController(dbContext);
        var courseId = 1;

        // Act
        var result = await controller.Enroll(courseId) as RedirectToActionResult;

        // Assert
        Assert.AreEqual("Index", result.ActionName);
        // Add more assertions as needed
    }

    
    // Modify the Delete action to display a confirmation view
    [HttpGet]
    [Test]
    public async Task Delete()
    {
        var dbContext = new ApplicationDbContext();
        var controller = new AdminController(dbContext);

        // Create a user view model for testing
        var userViewModel = new UserViewModel { Id = 12 }; // Set the Id to a valid user Id for testing

        // Act: Invoke the Delete action with the user view model
        var result = await controller.Delete(userViewModel);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }

    // Modify the DeleteConfirmed action to perform the deletion
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
 
   
    // Update the test case to invoke the DeleteConfirmed action
    public async Task Test_User_Deletion()
    {
        var dbContext = new ApplicationDbContext(/* pass any required options if needed */);
        var controller = new AdminController(dbContext); // Assuming AdminController is your admin management controller
        var userId = 11;

        // Act: Invoke the DeleteConfirmed action directly
        var result = await controller.Delete(new UserViewModel { Id = userId }) as RedirectToActionResult;

        // Assert
        Assert.AreEqual("Delete", result.ActionName);
        // Add more assertions as needed
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
        Assert.AreEqual("Courses", result.ActionName);
        // Add more assertions as needed
    }

    // Add similar methods for testing other functionalities like admin access, course deletion, and user registration
}
