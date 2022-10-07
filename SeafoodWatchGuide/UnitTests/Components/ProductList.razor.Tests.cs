using Bunit;
using NUnit.Framework;
using ContosoCrafts.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace UnitTests.Components
{
    /// <summary>
    /// This class contains all tests for ProductList.razor
    /// </summary>
    public class ProductListTests : BunitTestContext
    {
        #region TestSetup

        /// <summary>
        /// Set up for testing
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        /// <summary>
        /// Test if ProductList rendered correctly, which should have a container
        /// called Best Choice.
        /// </summary>
        [Test]
        public void ProductList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("BEST_CHOICE"));
        }

        #region SelectProduct
        /// <summary>
        /// Pass as part of the parameters to the ProductList Component, the Region of Central
        /// Then find the Div that holds the Best Choice items
        /// Look inside that inner HTML for that div, looking for the items
        /// Tuna should be there
        /// Octopus should not
        /// </summary>
        [Test]
        public void SelectRegion_Valid_Region_Central_Best_Choice_Should_Contain_Tuna()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "div_BEST_CHOICE";

            var page = RenderComponent<ProductList>(parameters => parameters.Add(p => p.region, "Central"));

            var resultTest = page.Markup;

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Div");

            // Act

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Assert
            Assert.AreEqual(true, button.InnerHtml.Contains("Tuna-Albacore"));

            Assert.AreEqual(false, button.InnerHtml.Contains("Octopus"));
        }

        /// <summary>
        /// Pass as part of the parameters to the ProductList Component, the Region of Hawaii, search key as Oysters
        /// Then find the Div that holds the Best Choice items
        /// Look inside that inner HTML for that div, looking for the items
        /// Oysters should be there
        /// Octopus should not 
        /// </summary>
        [Test]
        public void SearchTitle_Valid_Title_Oysters_Result_Should_Contain_Oysters()
        {
            //Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var divID = "div_BEST_CHOICE";

            var page = RenderComponent<ProductList>(parameters => parameters.Add(p => p.search, "Oysters").Add(p => p.region, "Hawaii"));
            var resultTest = page.Markup;

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Div");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(divID));

            // Assert
            Assert.AreEqual(true, button.InnerHtml.Contains("Oysters"));
            Assert.AreEqual(false, button.InnerHtml.Contains("Octopus"));
        }

        /// <summary>
        /// Clicking on the more info button for a best choice fish
        /// checks if the modal shows up 
        /// makes sure the title and rating is correct for that fish
        /// </summary>
        [Test]
        public void SelectFish_Valid_Best_Choice_Fish_Should_have_Modal_With_Correct_Title_And_Best_Choice_Rating()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var region = "Northeast";
            var id = "NE16";
            var title = "Swordfish";
            var page = RenderComponent<ProductList>(parameters => parameters.Add(p => p.region, region));

            //Act
            page.Find("#" + id + "_more_info").Click();
            var mark = page.Markup;

            // Assert
            Assert.AreEqual(true, mark.Contains("#productModal"));
            Assert.AreEqual(title, page.Find("#productModal #productTitle").InnerHtml);
            Assert.AreEqual("Best choice", page.Find("#productModal .card-footer").InnerHtml);
        }

        /// <summary>
        /// Clicking on the more info button for good alternative a fish
        /// checks if the modal shows up 
        /// makes sure the title and rating is correct for that fish
        /// </summary>
        [Test]
        public void SelectFish_Valid_Good_Alternative_Fish_Should_have_Modal_With_Correct_Title_And_Good_Alternative_Rating()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var region = "Northeast";
            var id = "NE21";
            var title = "Bluefish";
            var page = RenderComponent<ProductList>(parameters => parameters.Add(p => p.region, region));

            //Act
            page.Find("#" + id + "_more_info").Click();
            var mark = page.Markup;

            // Assert
            Assert.AreEqual(true, mark.Contains("#productModal"));
            Assert.AreEqual(title, page.Find("#productModal #productTitle").InnerHtml);
            Assert.AreEqual("Good alternative", page.Find("#productModal .card-footer").InnerHtml);
        }

        /// <summary>
        /// Clicking on the more info button for an avoid fish
        /// checks if the modal shows up 
        /// makes sure the title and rating is correct for that fish
        /// </summary>
        [Test]
        public void SelectFish_Valid_Avoid_Fish_Should_have_Modal_With_Correct_Title_And_Avoid_Rating()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var region = "Northeast";
            var id = "NE49";
            var title = "Atlantic Halbut";
            var page = RenderComponent<ProductList>(parameters => parameters.Add(p => p.region, region));

            //Act
            page.Find("#" + id + "_more_info").Click();
            var mark = page.Markup;

            // Assert
            Assert.AreEqual(true, mark.Contains("#productModal"));
            Assert.AreEqual(title, page.Find("#productModal #productTitle").InnerHtml);
            Assert.AreEqual("Avoid", page.Find("#productModal .card-footer").InnerHtml);
        }
        #endregion SelectProduct
    }
}