using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI; // Import the Edge namespace

namespace YourProject.Tests
{
    [TestFixture]
    public class SeleniumTests
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        private IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

        [SetUp]
        public void SetUp()
        {
            string driverPath = @"D:\BTECL\SeleniumTest\";
            // Set up the WebDriver with Microsoft Edge
            driver = new EdgeDriver(driverPath);
        }

        [TearDown]
        public void TearDown()
        {
            // Close the WebDriver
            driver.Quit();
        }

        [Test]
        public void TestAddingCourse()
        {
            // Navigate to the page
            driver.Navigate().GoToUrl("https://localhost:7012/Course/Add");

            // Find and fill the form fields
            IWebElement nameInput = driver.FindElement(By.Id("name"));
            nameInput.SendKeys("Test Course");

            IWebElement descriptionInput = driver.FindElement(By.Id("description"));
            descriptionInput.SendKeys("This is a test course for Selenium");

            // Submit the form
            IWebElement submitButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            submitButton.Click();

            // Wait for the action to complete
            System.Threading.Thread.Sleep(2000);

            // Check if the action was successful
            IWebElement confirmationMessage = driver.FindElement(By.XPath("//div[@id='confirmation']"));
            Assert.That(confirmationMessage.Text, Does.Contain("Course added successfully"));
        }
    }
    [TestFixture]
    public class UserEditTests
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        private IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method


        [SetUp]
        public void SetUp()
        {
            string driverPath = @"D:\BTECL\SeleniumTest\";
            // Set up the WebDriver with Microsoft Edge
            driver = new EdgeDriver(driverPath);
        }

        [TearDown]
        public void TearDown()
        {
            // Close the WebDriver if it's not null
            if (driver != null)
            {
                driver.Quit();
            }
        }

        [Test]
        public void TestEditUser()
        {
            try
            {
                // Navigate to the edit user page
                driver.Navigate().GoToUrl("https://localhost:7012/Admin/Edit");

                // Find the email input field and enter a new email
                IWebElement emailInput = driver.FindElement(By.Id("email"));
                emailInput.Clear();
                emailInput.SendKeys("newemail@example.com");

                // Find the username input field and enter a new username
                IWebElement userNameInput = driver.FindElement(By.Id("userName"));
                userNameInput.Clear();
                userNameInput.SendKeys("newusername");

                // Find the role dropdown and select a new role
                IWebElement roleDropdown = driver.FindElement(By.Id("role"));
                SelectElement roleSelect = new SelectElement(roleDropdown);
                roleSelect.SelectByText("Teacher");

                // Submit the form
                IWebElement saveButton = driver.FindElement(By.XPath("//button[@type='submit']"));
                saveButton.Click();

                // Wait for the action to complete
                System.Threading.Thread.Sleep(2000);

                // Check if the action was successful
                IWebElement confirmationMessage = driver.FindElement(By.XPath("//div[@id='confirmation']"));
                Assert.That(confirmationMessage.Text, Does.Contain("User updated successfully"));
            }
            catch (Exception ex)
            {
                // Handle any exceptions and log or report them as needed
                Assert.Fail($"Test failed: {ex.Message}");
            }
        }
    }
    [TestFixture]
    public class UserRegistrationTests
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        private IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method


        [SetUp]
        public void SetUp()
        {
            string driverPath = @"D:\BTECL\SeleniumTest\";
            // Set up the WebDriver with Microsoft Edge
            driver = new EdgeDriver(driverPath);
        }

        [TearDown]
        public void TearDown()
        {
            // Close the WebDriver if it's not null
            if (driver != null)
            {
                driver.Quit();
            }
        }

        [Test]
        public void TestRegisterUser()
        {
            try
            {
                // Navigate to the registration page
                driver.Navigate().GoToUrl("https://localhost:7012/Account/Register");

                // Find the username input field and enter a username
                IWebElement userNameInput = driver.FindElement(By.Name("UserName"));
                userNameInput.SendKeys("newuser");

                // Find the email input field and enter an email
                IWebElement emailInput = driver.FindElement(By.Name("Email"));
                emailInput.SendKeys("newuser@example.com");

                // Find the password input field and enter a password
                IWebElement passwordInput = driver.FindElement(By.Name("Password"));
                passwordInput.SendKeys("password123");

                // Find the confirm password input field and enter the same password
                IWebElement confirmPasswordInput = driver.FindElement(By.Name("ConfirmPassword"));
                confirmPasswordInput.SendKeys("password123");

                // Find the role dropdown and select a role
                IWebElement roleDropdown = driver.FindElement(By.Name("Role"));
                SelectElement roleSelect = new SelectElement(roleDropdown);
                roleSelect.SelectByText("Student");

                // Submit the form
                IWebElement registerButton = driver.FindElement(By.CssSelector("button[type='submit']"));
                registerButton.Click();

                // Wait for the action to complete
                System.Threading.Thread.Sleep(2000);

                // Check if the action was successful
                IWebElement confirmationMessage = driver.FindElement(By.XPath("//div[@id='confirmation']"));
                Assert.That(confirmationMessage.Text, Does.Contain("User registered successfully"));
            }
            catch (Exception ex)
            {
                // Handle any exceptions and log or report them as needed
                Assert.Fail($"Test failed: {ex.Message}");
            }
        }
    }
}
