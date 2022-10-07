using System.Collections.Generic;
using System.Linq;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;
using Moq;

namespace UnitTests.Pages.Admin
{
    /// <summary>
    /// Class containing all unit tests for Admin page
    /// </summary>
    public class AdminTests
    {
        #region TestSetup
        //AdminModel object used to test Admin page's model
        public static AdminModel PageModel;

        /// <summary>
        /// Test constructor
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<AdminModel>>();

            PageModel = new AdminModel(MockLoggerDirect, TestHelper.ProductService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// test OnGet function when it fetches all data to be shown
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
            var randomProduct = TestHelper.ProductService.GetAllData();
            // Act
            PageModel.OnGet();

            //Reset

            // Assert
            Assert.AreEqual(PageModel.Products.Count(), randomProduct.Count());
        }

        /// <summary>
        /// Test OnGetCreate when user hits the Create button, they should be
        /// redirected to the Create page
        /// </summary>
        [Test]
        public void OnGetCreate_Valid_Should_Return_Create_Page()
        {
            // Arrange
            // Act
            var page = PageModel.OnGetCreate() as RedirectToPageResult;

            //Reset

            // Assert
            Assert.AreEqual("Create", page.PageName);
        }

        /// <summary>
        /// Test OnGetClear method which clears all data from the filter
        /// </summary>
        [Test]
        public void OnGetClear_Valid_Should_Clear_All_Filters()
        {
            // Arrange
            PageModel.Filter.Region = "West Coast";
            PageModel.Filter.Rating = ContosoCrafts.WebSite.RatingEnums.ProductRating.BEST_CHOICE;

            // Act
            PageModel.OnGetClear();

            //Reset

            //Assert
            Assert.AreEqual(null, PageModel.Filter.Region);
            Assert.AreEqual(ContosoCrafts.WebSite.RatingEnums.ProductRating.UNKNOWN, PageModel.Filter.Rating);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Tests OnPost method where product list is filtered by a valid Region
        /// </summary>
        [Test]
        public void OnPost_Valid_Region_West_Coast_Should_Return_Filtered_List()
        {
            //Arrange
            PageModel.Filter.Region = "West Coast";

            IEnumerable<ProductModel> productList = PageModel.ProductService.GetAllData();
            productList = productList.Where(m => m.Region.Equals("West Coast"));

            //Act
            PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(productList.Count(), PageModel.Products.Count());
        }

        /// <summary>
        /// Tests OnPost method where product list is filtered by a invalid Title
        /// </summary>
        [Test]
        public void OnPost_InValid_Title_Search_Name_Should_Return_Empty_List()
        {
            //Arrange
            PageModel.Filter.Name = "Bogus";

            IEnumerable<ProductModel> productList = PageModel.ProductService.GetAllData();
            productList = productList.Where(m => m.Title.Contains("Bogus"));

            //Act
            PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(productList.Count(), PageModel.Products.Count());
        }

        /// <summary>
        /// Tests OnPost method where product list is filtered by a valid Title
        /// </summary>
        [Test]
        public void OnPost_Valid_Title_Search_Name_Should_Return_Filter_List()
        {
            //Arrange
            PageModel.Filter.Name = "Bass";

            IEnumerable<ProductModel> productList = PageModel.ProductService.GetAllData();
            productList = productList.Where(m => m.Title.Contains("Bass"));

            //Act
            PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(productList.Count(), PageModel.Products.Count());
        }

        /// <summary>
        /// The Region can be be a bogus value not found on any product
        /// When it is bogus, it returns an empty set, so Any will be false
        /// </summary>
        [Test]
        public void OnPost_InValid_Region_Bogus_Should_Return_Null()
        {
            //Arrange
            PageModel.Filter.Region = "Bogus";

            //Act
            PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(false, PageModel.Products.Any());
        }

        /// <summary>
        /// The Region can be null, so need to pass in a null region to test
        /// When it is null, it returns the full product list
        /// </summary>
        [Test]
        public void OnPost_InValid_Region_Null_Should_Return_FullList()
        {
            //Arrange
            PageModel.Filter.Region = null;

            //Act
            PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(true, PageModel.Products.Any());
        }

        /// <summary>
        /// The Region can be be a bogus value not found on any product
        /// When it is bogus, it returns an empty set, so Any will be false
        /// </summary>
        [Test]
        public void OnPost_InValid_Rating_Bogus_Should_Return_Null()
        {
            //Arrange
            PageModel.Filter.Rating = ContosoCrafts.WebSite.RatingEnums.ProductRating.UNKNOWN;

            //Act
            PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(true, PageModel.Products.Any());
        }

        /// <summary>
        /// The Region can be be a bogus value not found on any product
        /// When it is bogus, it returns an empty set, so Any will be false
        /// </summary>
        [Test]
        public void OnPost_Valid_Rating_Best_Choice_Should_Return_Filtered_List()
        {
            //Arrange
            PageModel.Filter.Rating = ContosoCrafts.WebSite.RatingEnums.ProductRating.BEST_CHOICE;

            //Act
            PageModel.OnPost();

            //Reset

            //Assert
            Assert.AreEqual(true, PageModel.Products.Any());
        }
        #endregion OnPost
    }
}