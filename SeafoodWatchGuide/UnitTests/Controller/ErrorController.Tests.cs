using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Controllers;
using System.Linq;

namespace UnitTests.Controller
{
    /// <summary>
    /// This class contains all the tests for the application's Error Controller
    /// </summary>
    class ErrorControllerTests
    {
        #region TestSetup
        //ErrorController object used to test Error controller's functionalities
        public ErrorController Controller;

        /// <summary>
        /// Test constructor.
        /// Creating the mock environment for conducting our test against
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            Controller = new ErrorController();
        }
        #endregion

        #region CodeHandler
        /// <summary>
        /// This tests the 404 status code is routed to the correct page
        /// </summary>
        [Test]
        public void CodeHandler_404_Valid_Redirect_Should_Return_True()
        {
            // Arrange
            int statusCode = 404;

            // Act
            RedirectToPageResult page = (RedirectToPageResult)Controller.CodeHandler(statusCode);

            // Reset

            // Assert
            Assert.AreEqual("/NotFound", page.PageName);
        }

        /// <summary>
        /// This tests other status code is routed to the correct page, like 500
        /// </summary>
        [Test]
        public void CodeHandler_Default_Valid_Redirect_Should_Return_True()
        {
            // Arrange
            int statusCode = 500;

            // Act
            RedirectToPageResult page = (RedirectToPageResult)Controller.CodeHandler(statusCode);

            // Reset

            // Assert
            Assert.AreEqual("/Error", page.PageName);
        }
        #endregion
    }
}
