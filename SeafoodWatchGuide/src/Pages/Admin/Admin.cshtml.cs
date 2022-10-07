using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Template for index (ie.Admin) page creation
    /// </summary>
    public class AdminModel : PageModel
    {
        //Logger
        private readonly ILogger<AdminModel> _logger;

        //Filter model to hold all criteria
        [BindProperty]
        public FilterModel Filter { get; set;}

        /// <summary>
        /// Constructor method for Admin Page
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public AdminModel(ILogger<AdminModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
            Filter = new FilterModel();
        }

        // Declares a getter for ProductService
        public JsonFileProductService ProductService { get; }

        // Declares a public getter and a private setter for Products
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// Initializing method for Admin page. All products are shown.
        /// </summary>
        public void OnGet()
        {
            Products = ProductService.GetAllData();
        }

        /// <summary>
        /// Method to react to an action of clicking on the Create button
        /// on the Admin page. User gets redirected to Create page.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGetCreate()
        {
            return RedirectToPage("Create");
        }

        /// <summary>
        /// Method to react to action of submitting a filter via the Apply
        /// button. User gets the refreshed page with stated filters.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            Products = ProductService.GetAllData();

            //Filter by Title
            if (Filter.Name != null)
            {
                var filterName = Filter.Name;
                Products = Products.Where(m => m.Title.ToLower().Contains(filterName.ToLower()));
            }

            //Filter by Region first, if chosen
            if (Filter.Region != null)
            {
                string val = Filter.Region;
                
                //Start with region
                Products = Products.Where(m => m.Region.Equals(val));
            }
            //Then filter by rating
            if (Filter.Rating != RatingEnums.ProductRating.UNKNOWN)
            {
                var rating = Filter.Rating;
                //Narrow down to rating
                Products = Products.Where(m => m.Rating.Equals(rating));
            }

            return Page();
        }

        /// <summary>
        /// Method to react to clicking of Clear button. Calls a clear data
        /// function in FilterModel to clear all filter data and reset the
        /// current selection of Products to be showing all Products
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGetClear()
        {
            Filter.ClearData();
            Products = ProductService.GetAllData();

            return Page();
        }
    }
}