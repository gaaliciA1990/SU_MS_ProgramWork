using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using ContosoCrafts.WebSite.RatingEnums;
using System;

namespace UnitTests.Model
{
    /// <summary>
    /// Class contains unit tests for FilterModel.cs
    /// </summary>
    public class FeedbackModelTests
    {
        #region TestSetup
        //FilterModel object used to test FilterModel's functionalities
        public FeedbackModel Model;

        /// <summary>
        /// Test constructor.
        /// Creating the mock environment for conducting our test against
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            Model = new FeedbackModel();
        }
        #endregion


        #region ToString
        /// <summary>
        /// UT for testing the serializer is acting as expected. Author
        /// unknown?
        /// TODO: Fix UT before pushing
        /// </summary>
        [Test]
        public void ToString_Valid_Check_Serialize_Is_Working()
        {
            // Arrange
            // declare a list of strings to implement the source field
            
            String Json = "{\"ID\":\"id\",\"Rating\":3,\"Comment\":\"comment\"}";

            var obj = new FeedbackModel()
            {
                ID = "id",
                Rating = 3,
                Comment = "comment"
            };

            // Act
            var serialized = obj.ToString();

            System.Diagnostics.Debug.WriteLine(Json);
            System.Diagnostics.Debug.WriteLine(serialized);

            //Reset

            // Assert
            Assert.AreEqual(serialized, Json);
        }

        #endregion ClearData
    }
}