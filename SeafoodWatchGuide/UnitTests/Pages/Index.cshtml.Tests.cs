using System.Linq;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;
using Moq;

namespace UnitTests.Pages.Index
{
    /// <summary>
    /// Class containing all unit tests for Index page
    /// </summary>
    public class IndexTests
    {

        #region TestSetup
        //pageModel represents the Index model used to get tested
        public static IndexModel PageModel;

        /// <summary>
        /// Test constructor
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();

            PageModel = new IndexModel(MockLoggerDirect, TestHelper.ProductService)
            {
            };
        }
        #endregion TestSetup


        #region OnGet
        /// <summary>
        /// Test OnGet function where all data should be retrieved successfully
        /// to be shown.
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
            //Dummy variable to store original counts of all products
            var randomProduct = TestHelper.ProductService.GetAllData();

            // Act
            //Set the page model's product variable
            PageModel.OnGet();

            //Reset

            // Assert
            Assert.AreEqual(PageModel.Products.Count(), randomProduct.Count());
        }
        #endregion OnGet
    }
}