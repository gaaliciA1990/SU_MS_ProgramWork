using System;
using System.Collections.Generic;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using ContosoCrafts.WebSite.RatingEnums;

namespace UnitTests.Model
{
    /// <summary>
    /// This class contains all the tests for the ProductModel's Sources list
    /// </summary>
    public class SourceValidateTests
    {
        #region TestSetup
        //ValidationAtribute object to test string lengths in list of strings
        public ListStringLength Attribute;

        /// <summary>
        /// Test constructor.
        /// Creating the mock environment for conducting our test against
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            Attribute = new ListStringLength();
        }
        #endregion


        #region DataAnnotationCheck
        /// <summary>
        /// checking if all strings in the Sources list are all between min and max character length UT for testing the serializer is acting as expected.
        /// All list items are in valid length. Test should return true.
        /// </summary>
        [Test]
        public void ListStringLength_Valid_List_Items_Between_Boundaries_Should_Return_True()
        {
            // Arrange
            // declare a list of strings to implement the source field
            List<string> sources = new List<string>();
            int minLenth = 5;
            int maxLenth = 20;
            sources.Add("abcde");
            sources.Add("abcdef");
            sources.Add("abcdedg");
            sources.Add("abcdefgh");
            sources.Add("abcdefghi");

            // Act
            Attribute.MaxStringLength = maxLenth;
            Attribute.MinStringLength = minLenth;
            var attributeVal = Attribute.IsValid(sources);

            // Assert
            Assert.AreEqual(attributeVal, true);
        }

        /// <summary>
        /// checking if all strings in the Sources list are all outside min and max character length
        /// UT for testing the serializer is acting as expected. Item has invalid
        /// length and so the test should return false.
        /// </summary>
        [Test]
        public void ListStringLength_Invalid_List_Items_Not_Valid_Length_Should_Return_False()
        {
            // Arrange
            // declare a list of strings to implement the source field
            List<string> sources = new List<string>();
            int minLenth = 5;
            int maxLenth = 8;
            sources.Add("a");
            sources.Add("ab");
            sources.Add("abc");
            sources.Add("abcd");
            sources.Add("abcdefghij");

            // Act
            Attribute.MaxStringLength = maxLenth;
            Attribute.MinStringLength = minLenth;
            var attributeVal = Attribute.IsValid(sources);

            // Assert
            Assert.AreEqual(attributeVal, false);
        }

        /// <summary>
        /// checking if list items in sources list are empty
        /// </summary>
        [Test]
        public void ListStringLength_InValid_List_Empty_Should_Return_False()
        {
            // Arrange
            // declare a list of strings to implement the source field
            List<string> sources = new List<string>();
            sources.Add("");


            // Act
            var attributeVal = Attribute.IsValid(sources);

            // Assert
            Assert.AreEqual(attributeVal, false);
        }

        /// <summary>
        /// checking if items in list are null
        /// </summary>
        [Test]
        public void ListStringLength_Invalid_List_Items_Null_Should_Return_False()
        {
            // Arrange
            // declare a list of strings to implement the source field
            List<string> sources = new List<string>();
            string s = null;
            sources.Add(s);


            // Act
            var attributeVal = Attribute.IsValid(sources);

            // Assert
            Assert.AreEqual(attributeVal, false);
        }

        /// <summary>
        /// checking if some strings in the Source list are outside min and max character length
        /// </summary>
        [Test]
        public void ListStringLength_Invalid_Some_Items_Have_Invalid_Length_Should_Return_False()
        {
            // Arrange
            // declare a list of strings to implement the source field
            List<string> sources = new List<string>();
            int minLenth = 4;
            int maxLenth = 8;
            sources.Add("abcde");
            sources.Add("abcdef");
            sources.Add("abcdedg");
            sources.Add("abcdefgh");
            sources.Add("abcdefghi");

            // Act
            Attribute.MaxStringLength = maxLenth;
            Attribute.MinStringLength = minLenth;
            var attributeVal = Attribute.IsValid(sources);

            // Assert
            Assert.AreEqual(attributeVal, false);
        }


        /// <summary>
        /// checking if Sources list is null
        /// </summary>
        [Test]
        public void ListStringLength_Invalid_List_Null_Should_Return_False()
        {
            // Arrange
            int minLenth = 4;
            int maxLenth = 8;

            // Act
            Attribute.MaxStringLength = maxLenth;
            Attribute.MinStringLength = minLenth;
            var attributeVal = Attribute.IsValid(null);

            // Assert
            Assert.AreEqual(attributeVal, false);
        }

        /// <summary>
        /// checking if Sources list has strings with no alphabetical characters
        /// </summary>
        [Test]
        public void ListStringLength_Invalid_Item_Has_No_Alphabetical_Characters_Should_Return_False()
        {
            // Arrange
            // declare a list of strings to implement the source field
            List<string> sources = new List<string>();
            int minLenth = 3;
            int maxLenth = 20;
            sources.Add("12345");
            sources.Add("123456");
            sources.Add("1234567");
            sources.Add("12345678");
            sources.Add("123456789");

            // Act
            Attribute.MaxStringLength = maxLenth;
            Attribute.MinStringLength = minLenth;
            var attributeVal = Attribute.IsValid(sources);

            // Assert
            Assert.AreEqual(attributeVal, false);
        }


        #endregion
    }
}