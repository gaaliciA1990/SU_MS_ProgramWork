using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;
using Moq;

namespace UnitTests.Pages.Region
{
    /// <summary>
    /// Unit testing for the Region page to ensure errors we can predict
    /// are covered.
    /// </summary>
    public class RegionTests
    {
        /// <summary>
        /// Setting up the testing environment for the Region page
        /// </summary>
        #region TestSetup

        //pageModel represents the Model object being used to test
        public static RegionModel PageModel;

        /// <summary>
        /// Creating the mock environment for conducting our test against
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<RegionModel>>();

            PageModel = new RegionModel(MockLoggerDirect, TestHelper.ProductService)
            {
            };
        }
        #endregion

        /// <summary>
        /// This tests the OnGet for region.cshtml.cs to confirm that the correct region is pulled 
        /// when called
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Region_For_Data_Pulled_Should_Return_Requested_Region()
        {
            // Arrange
            string region = "Central";

            // Act
            PageModel.OnGet(region);

            //Reset

            // Assert
            Assert.AreEqual("Central", PageModel.region);
        }
        #endregion
    }
}