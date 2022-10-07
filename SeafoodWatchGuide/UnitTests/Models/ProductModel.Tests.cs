using System;
using System.Collections.Generic;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using ContosoCrafts.WebSite.RatingEnums;

namespace UnitTests.Model
{
    /// <summary>
    /// This class contains all the tests for the application's ProductModel
    /// </summary>
    public class ProductModelTests
    {
        #region TestSetup
        //ProductModel object used to test Product model's functionalities
        public ProductModel Model;

        /// <summary>
        /// Test constructor.
        /// Creating the mock environment for conducting our test against
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            Model = new ProductModel();
        }
        #endregion

        #region GetSources
        /// <summary>
        /// Test GetSources function. Should return all sources as a formatted string
        /// </summary>
        [Test]
        public void GetSources_Valid_Should_Return_Formatted_Source_As_String()
        {
            //Arrange
            List<string> sources = new List<string>();
            sources.Add("US Farm");
            sources.Add("Farmed");

            string original = "US Farm, Farmed";

            var obj = new ProductModel()
            {
                Id = "id",
                Region = "region",
                Image = "image",
                Title = "title",
                Description = "description",
                Rating = ProductRating.BEST_CHOICE,
                Sources = sources
            };


            //Act
            var result = obj.GetSources();

            //Assert
            Assert.AreEqual(sources.Count, obj.Sources.Count);
            Assert.AreEqual(sources[0], obj.Sources[0]);
            Assert.AreEqual("Farmed", obj.Sources[1]);
            Assert.AreEqual(original, result);
        }
        #endregion GetSources

        #region ConvertToString
        /// <summary>
        /// UT for testing the serializer is acting as expected. Author
        /// unknown?
        /// </summary>
        [Test]
        public void ToString_Valid_Check_Serialize_Is_Working()
        {
            // Arrange
            // declare a list of strings to implement the source field
            List<string> sources = new List<string>();
            sources.Add("source");

            String Json = "{\"Id\":\"id\",\"Title\":\"title\",\"Region\":\"region\",\"Rating\":\"" + ProductRating.BEST_CHOICE + "\",\"Description\":\"description\",\"img\":\"image\",\"Sources\":[\"source\"]}";
            var obj = new ProductModel()
            {
                Id = "id",
                Region = "region",
                Image = "image",
                Title = "title",
                Description = "description",
                Rating = ProductRating.BEST_CHOICE,
                Sources = sources
            };

            // Act
            var serialized = obj.ToString();

            System.Diagnostics.Debug.WriteLine(Json);
            System.Diagnostics.Debug.WriteLine(serialized);

            //Reset

            // Assert
            Assert.AreEqual(serialized, Json);
        }
        #endregion
    }
}