using System.Linq;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Template for page creation
    /// </summary>
    public class ReadModel : PageModel
    {
        //Logger to help with debugging
        private readonly ILogger<ReadModel> _logger;

        //Product Service object used to access application's services
        public JsonFileProductService ProductService { get; }

        //Product object contains information about the product being shown
        public ProductModel Product { get; set; }

        //Variable to notify if an error has happened
        public bool errorOccurred = false;

        /// <summary>
        /// Constructor for Read Page
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>

        public ReadModel(ILogger<ReadModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        /// <summary>
        /// Method to load the selected item to be shown
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
            if (Product == null)
            {
                errorOccurred = true;
                //return RedirectToPage("./Admin");
            }

            return Page();
        }

        /// <summary>
        /// Method for redirecting users to Delete page when they choose to Delete
        /// the item.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult OnGetDelete(string Id)
        {
            return RedirectToPage("Delete", new { id = Id });
        }

        /// <summary>
        /// Method for redirecting users to Update page when they choose to
        /// Update values in the item
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult OnGetUpdate(string Id)
        {
            return RedirectToPage("Update", new { id = Id });
        }
    }
}