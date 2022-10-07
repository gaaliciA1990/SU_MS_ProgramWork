using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using ContosoCrafts.WebSite.RatingEnums;

namespace UnitTests.Model
{
    /// <summary>
    /// Class contains unit tests for FilterModel.cs
    /// </summary>
    public class FilterModelTests
    {
        #region TestSetup
        //FilterModel object used to test FilterModel's functionalities
        public FilterModel Model;

        /// <summary>
        /// Test constructor.
        /// Creating the mock environment for conducting our test against
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            Model = new FilterModel();
        }
        #endregion


        #region ClearData
        /// <summary>
        /// This tests the ClearData method in FilterModel, which should
        /// reset all filter criteria to default values
        /// </summary>
        [Test]
        public void ClearData_Valid_Should_Set_All_Data_To_Default()
        {
            // Arrange
            Model.Rating = ProductRating.BEST_CHOICE;
            Model.Region = "West Coast";

            // Act
            Model.ClearData();

            //Reset

            // Assert
            Assert.AreEqual(null, Model.Region);
            Assert.AreEqual(ProductRating.UNKNOWN, Model.Rating);
        }
        #endregion ClearData
    }
}