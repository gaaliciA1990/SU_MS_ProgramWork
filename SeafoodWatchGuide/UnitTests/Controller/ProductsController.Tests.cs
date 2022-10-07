using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.RatingEnums;
using ContosoCrafts.WebSite.Controllers;
using System.Linq;

namespace UnitTests.Controller
{
    /// <summary>
    /// This class contains all the tests for the application's ProductController
    /// </summary>
    public class ProductControllerTests
    {
        #region TestSetup
        //ProductsController object used to test Product controller's functionalities
        public ProductsController Controller;

        /// <summary>
        /// Test constructor.
        /// Creating the mock environment for conducting our test against
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            Controller = new ProductsController(TestHelper.ProductService);
        }
        #endregion

        #region ConvertToString
        /// <summary>
        /// This tests the convertToSTring in ProductModel.cs to confirm that the correct string is displayed 
        /// when Best Choice is converted from the productrating ENUM.
        /// </summary>
        [Test]
        public void GetAllData_Valid_Should_Return_True()
        {
            // Arrange
            //Dummy variable to store original counts of all products
            var randomProduct = TestHelper.ProductService.GetAllData();

            // Act
            //Set the page model's product variable
            var Data = Controller.Get();

            //Reset

            // Assert
            Assert.AreEqual(Data.Count(), randomProduct.Count());
        }

        /// <summary>
        /// This tests construction of a RatingRequest object 
        /// </summary>
        [Test]
        public void RatingRequest_Valid_Should_Return_True()
        {
            // Arrange
            var RatingReq = new ProductsController.RatingRequest();

            // Act
            RatingReq.ProductId= "testId";
            RatingReq.Rating = ProductRating.AVOID;

            //Reset

            // Assert
            Assert.AreEqual(RatingReq.ProductId, "testId");
            Assert.AreEqual(RatingReq.Rating, ProductRating.AVOID);
        }

        /// <summary>
        /// This tests the return of Ok ActionResult
        /// </summary>
        [Test]
        public void RatingRequest_Invalid_Should_Return_Null()
        {
            // Arrange
            var RatingReq = new ProductsController.RatingRequest();
            RatingReq.ProductId = "testId";
            RatingReq.Rating = ProductRating.AVOID;

            // Act
            var OkResult = Controller.Patch(RatingReq) as OkObjectResult;

            //Reset


            // Assert
            Assert.AreEqual(OkResult, null);
        }
        #endregion
    }
}