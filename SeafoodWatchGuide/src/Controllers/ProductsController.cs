using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.RatingEnums;


namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// This class is the controller for handling interactions with our products
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// This is a constructor for the controller 
        /// </summary>
        /// <param name="productService"></param>
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // This is a getter method for ProductService
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// This is retrieving the product information from the JSON file
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetAllData();
        }

        [HttpPatch]
        /// <summary>
        /// This code gets the rating from the user, sets the rating in the variable,
        /// then appends the rating to the JSON file
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            //ProductService.AddRating(request.ProductId, request.Rating);
            return Ok();
        }

        /// <summary>
        /// This is the model for the ENUM ratings. Represents the data to be updated
        /// in the JSON file. Will associate a rating with a seafood product
        /// </summary>
        public class RatingRequest
        {
            public string ProductId { get; set; }
            public ProductRating Rating { get; set; }
        }
    }
}