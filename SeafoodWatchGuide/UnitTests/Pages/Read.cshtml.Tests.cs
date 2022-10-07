using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;
using Moq;

namespace UnitTests.Pages.Read
{
    /// <summary>
    /// Class containing all unit tests for Read page model
    /// </summary>
    public class ReadTests
    {
        #region TestSetup
        //Read model used to test Read page model's behaviors
        public static ReadModel PageModel;

        /// <summary>
        /// Test constructor
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<ReadModel>>();

            PageModel = new ReadModel(MockLoggerDirect, TestHelper.ProductService)
            {
            };
        }


        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test OnGetDelete function where it should fetch the correct item to
        /// be reviewed before deletion on the Delete page
        /// </summary>
        [Test]
        public void OnGetDelete_Valid_Should_Return_Delete_Page()
        {
            // Arrange
            // Act
            var sampleProduct = TestHelper.ProductService.GetAllData().FirstOrDefault();
            var page = PageModel.OnGetDelete(sampleProduct.Id) as RedirectToPageResult;

            //Reset

            // Assert
            Assert.AreEqual("Delete", page.PageName);
            
        }

        /// <summary>
        /// Test OnGet function where it should fetch the correct item to show
        /// details on the Read page
        /// </summary>
        [Test]
        public void OnGet_Valid_ID_Should_Return_Product()
        {
            // Arrange
            // Act
            var randomProduct = TestHelper.ProductService.GetAllData().FirstOrDefault();
            var sampleProduct = TestHelper.ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(randomProduct.Id));
            PageModel.OnGet(sampleProduct.Id);

            //Reset

            // Assert
            Assert.AreEqual(sampleProduct.Id, PageModel.Product.Id);

        }

        /// <summary>
        /// Test OnGetUpdate function where it should fetch the correct item to
        /// get updated on the Update page
        /// </summary>
        [Test]
        public void OnGetUpdate_Valid_State_Should_Return_Update_Page()
        {
            // Arrange
            // Act
            var sampleProduct = TestHelper.ProductService.GetAllData().FirstOrDefault();
            var page = PageModel.OnGetUpdate(sampleProduct.Id) as RedirectToPageResult;

            //Reset

            // Assert
            Assert.AreEqual("Update", page.PageName);

        }

        /// <summary>
        /// Tests invalid id, which the model will return a null product because
        /// it is not in our database
        /// </summary>
        [Test]
        public void OnGet_InValid_Id_Bougs_Should_Return_Null_Product()
        {
            // Arrange

            // Act
            var result = PageModel.OnGet("Bogus") as RedirectToPageResult;

            //Reset

            // Assert
            Assert.AreEqual(null, PageModel.Product);
            Assert.AreEqual(true, PageModel.errorOccurred);
        }

        #endregion OnGet
    }
}