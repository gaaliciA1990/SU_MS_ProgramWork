using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using System.Linq;
using Newtonsoft.Json;

namespace UnitTests.Services
{
    /// <summary>
    /// This class contains tests for the application's supported services
    /// listed in JsonFileFeedbackService.cs
    /// </summary>
    class JsonFileFeedbackServiceTests
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
        /// Function tests the CreateFeedback method using valid state and expecting
        /// a new feedback to be created
        /// </summary>
        [Test]
        public void CreateFeedback_Valid_Product_Should_Return_True()
        {

            // Arrange
            var origin_data = new FeedbackModel()
            {
                ID = "",
                Rating = 4,
                Comment = "testing comment",
            };

            //Act TODO: FIX THIS WHEN IMPLEMENTATION IS COMPLETED!
            var updatedData = TestHelper.FeedbackService.CreateFeedback(origin_data.Rating, origin_data.Comment);
            var dataSet = TestHelper.FeedbackService.GetAllFeedback();

            //Reset


            //Assert
            var newCreate = dataSet.FirstOrDefault(m => m.ID.Equals(updatedData.ID));
            var origin_data_json= JsonConvert.SerializeObject(updatedData);
            var newCreate_json = JsonConvert.SerializeObject(newCreate);

            Assert.AreEqual(updatedData.ID, newCreate.ID);
            Assert.AreEqual(origin_data_json, newCreate_json);
        }
    }
}