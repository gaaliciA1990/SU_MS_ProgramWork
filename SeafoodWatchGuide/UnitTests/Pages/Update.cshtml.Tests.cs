using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;
using ContosoCrafts.WebSite.Models;
using System;

namespace UnitTests.Pages.Update
{
    /// <summary>
    /// Unit Tests for the Update.cshtml.cs page. This class will have all 
    /// test for each method in the Update file
    /// </summary>
    public class UpdateTests
    {
        #region TestSetup
        //Update model object used to test Update page model's behaviors
        public UpdateModel PageModel;

        /// <summary>
        /// test constructor
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            PageModel = new UpdateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test OnGet function with valid state, which it should then fetch the
        /// correct item to get updated
        /// </summary>

        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            PageModel.OnGet("H1");

            //Reset

            // Assert
            Assert.AreEqual(true, PageModel.ModelState.IsValid);
            Assert.AreEqual("Bigeye Scad", PageModel.Product.Title);
        }

        /// <summary>
        /// Test OnGet function with invalid id, which should let the product
        /// variable be null and return error message
        /// </summary>
        [Test]
        public void OnGet_Invalid_ID_Bogus_Should_Return_Null()
        {
            // Arrange
            PageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            PageModel.OnGet("bogus");

            //Reset

            // Assert
            Assert.AreEqual(false, PageModel.ModelState.IsValid);
            Assert.AreEqual(null, PageModel.Product);
            Assert.AreEqual(true, PageModel.errorOccurred);
        }
        #endregion OnGet


        #region OnPost
        /// <summary>
        /// test OnPost function with invalid state, which it should not allow
        /// the update to happen. Done by checking if the model state is invalid
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange

            // Force an invalid error state
            PageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = PageModel.OnPost() as ActionResult;

            //Reset

            // Assert
            Assert.AreEqual(false, PageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Tests Onpost with invalid title input, model state should be invalid
        /// and page gets returned without submission.
        /// </summary>
        [Test]
        public void OnPost_InValid_Title_Input_NotValid_Return_Page()
        {
            // Arrange
            PageModel.Product = new ProductModel
            {
                Id = "Crab",
                Image = "data/Fish_img/Crab.bmp",
                Rating = ContosoCrafts.WebSite.RatingEnums.ProductRating.AVOID,
                Description = "Blue crab caught in Maryland's Chesapeake Bay waters with trotlines is a Best Choice. The stock is healthy, and overfishing is a low concern. The trotline fishery doesn't catch any other species, and management is rated highly effective. There are no major concerns about seafloor impacts, but ecosystem-based management measures haven't been implemented.",
                Region = "West Coast",
                Title = "392847#($*#$"
            };
            

            // Act
            var result = PageModel.OnPost() as ActionResult;

            //Reset

            // Assert
            Assert.AreEqual(false, PageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Tests Onpost with invalid title input, model state should be invalid
        /// and page gets returned without submission.
        /// </summary>
        [Test]
        public void OnPost_InValid_Description_Input_NotValid_Return_Page()
        {
            // Arrange
            PageModel.Product = new ProductModel
            {
                Id = "Crab",
                Image = "data/Fish_img/Crab.bmp",
                Rating = ContosoCrafts.WebSite.RatingEnums.ProductRating.AVOID,
                Description = "3947508437",
                Region = "West Coast",
                Title = "Test"
            };

            // Act
            var result = PageModel.OnPost() as ActionResult;

            //Reset

            // Assert
            Assert.AreEqual(false, PageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Test OnPost function with valid state, which should allow the update
        /// to happen. Done by checking if the model is in valid state, if update
        /// happened, the function should also redirect the user back to
        /// Admin page.
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            // Arrange

            PageModel.Product = new ProductModel
            {
                Id = "Crab",
                Image = "data/Fish_img/Crab.bmp",
                Rating = ContosoCrafts.WebSite.RatingEnums.ProductRating.AVOID,
                Description = "Blue crab caught in Maryland's Chesapeake Bay waters with trotlines is a Best Choice. The stock is healthy, and overfishing is a low concern. The trotline fishery doesn't catch any other species, and management is rated highly effective. There are no major concerns about seafloor impacts, but ecosystem-based management measures haven't been implemented.",
                Region = "West Coast",
                Title = "Crab: Southern King(Argentina)"
            };
            

            // Act
            var result = PageModel.OnPost() as RedirectToPageResult;

            //Reset

            // Assert
            Assert.AreEqual(true, PageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Admin"));
        }
        #endregion OnPost

    }
}