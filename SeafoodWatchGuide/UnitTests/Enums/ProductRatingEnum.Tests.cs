using System;
using ContosoCrafts.WebSite.RatingEnums;
using NUnit.Framework;

namespace UnitTests.ProductRatingEnum
{
    /// <summary>
    /// This class contains all the tests for the application's ProductRatingEnum.cs
    /// </summary>
    public class ProductRatingEnumTests
    {
        #region TestSetup
        //ProductRating array variable used to test Product rating Enums
        public Array Ratings;

        /// <summary>
        /// Test constructor.
        /// Creating the mock environment for conducting our test against.
        /// Pulling the list of rating into an array
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            Ratings = Enum.GetValues(typeof(ProductRating));
        }
        #endregion

        #region EnumTests
        /// <summary>
        /// Test the value 0 return Unknown
        /// </summary>
        [Test]
        public void ProductRating_Valid_Unknown_Enum_Returned()
        {
            // Arrange

            // Act
            ProductRating unknown = (ProductRating)(Ratings.GetValue(0));

            //Reset

            // Assert
            Assert.AreEqual(ProductRating.UNKNOWN, unknown);
        }

        /// <summary>
        /// Test the value 1 return best choice
        /// </summary>
        [Test]
        public void ProductRating_Valid_BestChoice_Enum_Returned()
        {
            // Arrange

            // Act
            ProductRating unknown = (ProductRating)(Ratings.GetValue(1));

            //Reset

            // Assert
            Assert.AreEqual(ProductRating.BEST_CHOICE, unknown);
        }

        /// <summary>
        /// Test the value 2 return good alternative
        /// </summary>
        [Test]
        public void ProductRating_Valid_GoodAlternative_Enum_Returned()
        {
            // Arrange

            // Act
            ProductRating unknown = (ProductRating)(Ratings.GetValue(2));

            //Reset

            // Assert
            Assert.AreEqual(ProductRating.GOOD_ALTERNATIVE, unknown);
        }

        /// <summary>
        /// Test the value 0 return Unknown
        /// </summary>
        [Test]
        public void ProductRating_Valid_Avoid_Enum_Returned()
        {
            // Arrange

            // Act
            ProductRating unknown = (ProductRating)(Ratings.GetValue(3));

            //Reset

            // Assert
            Assert.AreEqual(ProductRating.AVOID, unknown);
        }
        #endregion

        #region EnumExtensionTests
        /// <summary>
        /// This tests the convertToSTring in ProductModel.cs to confirm that the correct string is displayed 
        /// when Best Choice is converted from the productrating ENUM.
        /// </summary>
        [Test]
        public void ConvertToString_Valid_String_For_BestChoice_ENUM()
        {
            // Arrange - not needed

            // Act
            var Result = EnumExtensions.ConvertToString(ContosoCrafts.WebSite.RatingEnums.ProductRating.BEST_CHOICE);

            //Reset

            // Assert
            Assert.AreEqual("Best choice", Result);
        }

        /// <summary>
        /// This tests the convertToSTring in ProductModel.cs to confirm that the correct string is displayed 
        /// when Good Alternative is converted from the productrating ENUM.
        /// </summary>
        [Test]
        public void ConvertToString_Valid_String_For_GoodAlt_ENUM()
        {
            // Arrange - not needed

            // Act
            var Result = EnumExtensions.ConvertToString(ContosoCrafts.WebSite.RatingEnums.ProductRating.GOOD_ALTERNATIVE);

            //Reset

            // Assert
            Assert.AreEqual("Good alternative", Result);
        }

        /// <summary>
        /// This tests the convertToSTring in ProductModel.cs to confirm that the correct string is displayed 
        /// when Avoid is converted from the productrating ENUM.
        /// </summary>
        [Test]
        public void ConvertToString_Valid_String_For_Avoid_ENUM()
        {
            // Arrange - not needed

            // Act
            var Result = EnumExtensions.ConvertToString(ContosoCrafts.WebSite.RatingEnums.ProductRating.AVOID);

            //Reset

            // Assert
            Assert.AreEqual("Avoid", Result);
        }
    }
    #endregion
}