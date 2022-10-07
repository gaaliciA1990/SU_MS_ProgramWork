using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// Class containing unit tests for Privacy page model
    /// </summary>
    public class PrivacyTests
    {

        #region TestSetup
        //Privacy Model used to test our Privacy page model's behaviors
        public static PrivacyModel PageModel;

        /// <summary>
        /// test constructor
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();

            PageModel = new PrivacyModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// test OnGet function where the model should be in valid state 
        /// </summary>
        [Test]
        public void OnGet_Valid_State_Should_Return_Valid_Model()
        {
            // Arrange

            // Act
            PageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, PageModel.ModelState.IsValid);
        }

        #endregion OnGet
    }
}