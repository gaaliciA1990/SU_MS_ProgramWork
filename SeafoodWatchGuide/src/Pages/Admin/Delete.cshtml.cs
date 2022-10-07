using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Class for Delete page in CRUDi implementation
    /// </summary>
    public class DeleteModel : PageModel
    {
        //Variable to notify if an error has happened
        public bool errorOccurred = false;

        //Logger to help with debugging
        private readonly ILogger<DeleteModel> _logger;

        /// <summary>
        /// Constructor for Delete Page
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public DeleteModel(ILogger<DeleteModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        //Product service object
        public JsonFileProductService ProductService { get; }

        //Product model object
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Method for loading the requested item to get deleted
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));

            //Flag if an error happened
            if (Product == null)
            {
                errorOccurred = true;
            }
        }

        /// <summary>
        /// Method for perform deletion and redirect the user back to Admin page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            //delete card
            ProductService.DeleteCard(Product.Id);
            // Redirect back to the admin page to continue editing if needed
            return RedirectToPage("Admin");
        }
    }
}