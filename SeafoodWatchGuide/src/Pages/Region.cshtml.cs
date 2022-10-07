using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// The region page will display only the data of the region that is selected from the homepage.
    /// This removes the need for more than one guide page as we can pass the region string
    /// to display the data on that page.
    /// </summary>
    public class RegionModel : PageModel
    {
        //Logger to help with debugging
        private readonly ILogger<RegionModel> _logger;

        /// <summary>
        /// Constructor for Region page
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public RegionModel(ILogger<RegionModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
        }

        //Variable to store the ID of the region being requested
        public string region;

        //Variable to store seafood search string
        public string search;

        /// <summary>
        /// OnGet will retrieve the data from the Seafoods.json file 
        /// and return only the seafood from the region passed
        /// use filter to search serach seafood data in the region
        /// </summary>
        /// <param name="region"></param>
        /// <param name="filter"></param>
        public void OnGet(string region, string search = "")
        {
            this.region = region;
            this.search = search;
        }
    }
}