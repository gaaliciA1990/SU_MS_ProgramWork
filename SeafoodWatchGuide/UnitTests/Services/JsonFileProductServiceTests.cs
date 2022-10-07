using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using System.Linq;
using Newtonsoft.Json;

namespace UnitTests.Services
{
    /// <summary>
    /// This class contains tests for the application's supported services
    /// listed in JsonFileProductService.cs
    /// </summary>
    class JsonFileProductServiceTests
    {
         #region TestSetup
        /// <summary>
        /// Place holder for setting up the test
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }
        #endregion TestSetup

        /// <summary>
        /// Function tests the CreateCard method using valid state and expecting
        /// a new card to be created
        /// </summary>
        [Test]
        public void CreateCard_Valid_Product_Should_Return_True()
        {

            // Arrange
            var origin_data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Region = "West Coast",
                Image = "testing image",
                Title = "testing title",
                Description = "Enter URL",
                Rating = ContosoCrafts.WebSite.RatingEnums.ProductRating.AVOID,
            };

            //Act
            TestHelper.ProductService.CreateCard(origin_data);
            var dataSet = TestHelper.ProductService.GetAllData();

            //Reset
            TestHelper.ProductService.DeleteCard(origin_data.Id);

            //Assert
            var newCreate = dataSet.FirstOrDefault(m => m.Id.Equals(origin_data.Id));
            var origin_data_json= JsonConvert.SerializeObject(origin_data);
            var newCreate_json = JsonConvert.SerializeObject(newCreate);

            Assert.AreEqual(origin_data_json, newCreate_json);
        }

        /// <summary>
        /// Test UpdateCard method using invalid input, the card
        /// should not be found and therefore not updated.
        /// </summary>
        [Test]
        public void UpdateCard_inValid_Product_Should_Return_Null()
        {

            // Arrange
            var origin_data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Region = "West Coast",
                Image = "testing image",
                Title = "testing title",
                Description = "Enter URL",
                Rating = ContosoCrafts.WebSite.RatingEnums.ProductRating.AVOID,
            };


            //Act
            var result = TestHelper.ProductService.UpdateCard(origin_data);

            //Reset


            //Assert
            Assert.AreEqual(result, null);
        }

        /// <summary>
        /// Test UpdateCard function using valid input, the card should be
        /// found and get updated accordingly.
        /// </summary>
        [Test]
        public void UpdateCard_Valid_Product_Should_Return_Updated_Product()
        {

            // Arrange
            var data = TestHelper.ProductService.GetAllData().First();
            string defaultDesription = data.Description;
            data.Description = "Test Test Test";

            //Act
            var result = TestHelper.ProductService.UpdateCard(data);
            var updated_data = TestHelper.ProductService.GetAllData().FirstOrDefault(x => x.Id.Equals(data.Id));

            //Reset
            data.Description = defaultDesription;
            TestHelper.ProductService.UpdateCard(data);

            //Assert
            Assert.AreEqual("Test Test Test", updated_data.Description);
        }

        /// <summary>
        /// Test GetRegionData method using invalid input, thus not returning
        /// anything.
        /// </summary>
        [Test]
        public void GetRegionData_inValid_Product_Should_Return_Empyt_IEnumerable()
        {

            // Arrange
            string testing_region = "no exist region";


            //Act
            var result = TestHelper.ProductService.GetRegionData(testing_region);

            //Reset


            //Assert
            Assert.IsEmpty(result);
        }

        /// <summary>
        /// Test GetRegionData using valid input, thus return the data for the
        /// specified region accordingly
        /// </summary>
        [Test]
        public void GetRegionData_Valid_Product_Should_Return_Not_Empty_IEnumerable()
        {

            // Arrange
            string testing_region = "West Coast";


            //Act
            var result = TestHelper.ProductService.GetRegionData(testing_region);

            //Reset


            //Assert
            Assert.IsNotEmpty(result);
        }

        /// <summary>
        /// Test GetAllRegions method, should return all supported regions
        /// </summary>
        [Test]
        public void GetAllRegions_Valid_Should_Return_All_Region()
        {

            // Arrange
            var checking = new string[7]{"West Coast", "Southwest", "Central", "Southeast",
                                "Northeast", "Hawaii", "National"};

            //Act
            var result = TestHelper.ProductService.GetAllRegions();


            //Assert
            Assert.AreEqual(checking, result);
        }
    }
}