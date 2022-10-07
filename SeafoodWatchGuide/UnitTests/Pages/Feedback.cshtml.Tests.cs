using System.Linq;
using ContosoCrafts.WebSite.Models;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;
using Moq;

namespace UnitTests.Pages.Feedback
{
    /// <summary>
    /// Class containing all unit tests for Feedback page
    /// </summary>
    public class FeedbackTests
    {

        #region TestSetup
        //pageModel represents the Feedback model used to get tested
        public static FeedbackPageModel PageModel;

        /// <summary>
        /// Test constructor
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<FeedbackPageModel>>();

            PageModel = new FeedbackPageModel(MockLoggerDirect, TestHelper.FeedbackService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test OnGet function on Privacy model
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_Valid_Model_State()
        {
            // Arrange
            var data = PageModel.previewFeedBacks;
            // Act
            PageModel.OnGet();

            // Assert
            Assert.AreEqual(true, PageModel.ModelState.IsValid);
            Assert.AreEqual(false, PageModel.displayThankYouMessage);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Test OnPost function with valid states, should return valid model
        /// state and display flag should be turned to true
        /// </summary>
        [Test]
        public void OnPost_Valid_Input_Should_Return_Valid_Model_State()
        {
            //Arrange
            PageModel.Feedback = new FeedbackModel
            {
                ID = "123",
                Rating = 5,
                Comment = "test comment"
            };

            bool beforeOnPost = PageModel.displayThankYouMessage;


            //Act
            var result = PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(true, PageModel.ModelState.IsValid);
            Assert.AreEqual(true, PageModel.displayThankYouMessage);
            Assert.AreEqual(false, beforeOnPost);
        }

        /// <summary>
        /// Test OnPost function with invalid states, should return invalid model
        /// state and display flag should be turned to false
        /// </summary>
        [Test]
        public void OnPost_Invalid_Rating_Null_Should_Return_Invalid_Model_State()
        {
            //Arrange
            PageModel.Feedback = new FeedbackModel
            {
                ID = "123",
                Comment = "test comment"
            };

            bool beforeOnPost = PageModel.displayThankYouMessage;
            PageModel.ModelState.AddModelError("bogus", "bogus error");

            //Act
            var result = PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(false, PageModel.ModelState.IsValid);
            Assert.AreEqual(false, beforeOnPost);
            Assert.AreEqual(false, PageModel.displayThankYouMessage);
            
        }
        #endregion OnPost
    }
}