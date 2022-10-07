using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;
using Moq;

namespace UnitTests.Pages.Delete
{
    /// <summary>
    /// Class containing all unit tests for Delete page
    /// </summary>
    public class DeleteTests
    {
        #region TestSetup
        //DeleteModel object used to test Delete page's model
        public static DeleteModel PageName;


        /// <summary>
        /// Test constructor
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<DeleteModel>>();

            PageName = new DeleteModel(MockLoggerDirect, TestHelper.ProductService)
            {
            };
        }


        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test OnGet function, which should fetch the correct item requested
        /// </summary>

        [Test]
        public void OnGet_Valid_ID_Should_Return_Product()
        {
            // Arrange
            var randomProduct = TestHelper.ProductService.GetAllData().FirstOrDefault();
            var sampleProduct = TestHelper.ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(randomProduct.Id));

            //Act
            PageName.OnGet(sampleProduct.Id);

            //Reset

            // Assert
            Assert.AreEqual(sampleProduct.Id, PageName.Product.Id);

        }

        /// <summary>
        /// Test OnGet function with invalid id, which should let the product
        /// variable be null and return error message
        /// </summary>
        [Test]
        public void OnGet_Invalid_ID_Bogus_Should_Return_Null()
        {
            // Arrange
            PageName.ModelState.AddModelError("bogus", "bogus error");

            // Act
            PageName.OnGet("bogus");

            //Reset

            // Assert
            Assert.AreEqual(false, PageName.ModelState.IsValid);
            Assert.AreEqual(null, PageName.Product);
            Assert.AreEqual(true, PageName.errorOccurred);
        }

        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Test OnPost function where deletion should happen successfully, and
        /// user gets directed back to Admin page.
        /// </summary>
        [Test]
        public void OnPost_Valid_ID_Should_Delete_Product_And_Return_To_Admin_Page()
        {
            // Arrange
            var randomProduct = TestHelper.ProductService.GetAllData().FirstOrDefault();
            PageName.Product = randomProduct;
            RedirectToPageResult page;

            //Act
            page = PageName.OnPost() as RedirectToPageResult;
            var sampleProduct = TestHelper.ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(randomProduct.Id));

            //Reset

            // Assert
            Assert.AreEqual(null, sampleProduct);
            Assert.AreEqual("Admin", page.PageName);


        }

        #endregion OnPost

    }
}