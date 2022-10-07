using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Template for page creation
    /// </summary>
    public class UpdateModel : PageModel
    {
        //Variable to notify if an error has happened
        public bool errorOccurred = false;

        /// <summary>
        /// Constructor for Update page
        /// </summary>
        /// <param name="productService"></param>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        //Product service object
        public JsonFileProductService ProductService { get; }

        //Product model object
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// get the product details 
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));

            //Flag if an error happened
            if (Product == null)
            {
                errorOccurred = true;
                //return RedirectToPage("./Admin");
            }
        }

        /// <summary>
        /// Post the model back to the page
        /// The model is in the class variable Product
        /// Call the data layer to Update that data
        /// Then return to the index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            //Check for input validation, if not valid, return to page
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            //Check if input was just a bunch of numbers in Title
            if (hasLetters(Product.Title) == false)
            {
                ModelState.AddModelError("numbers", "Title must contain letters");
                return Page();
            }

            //Check if Description only has a bunch of numbers and symbols
            if (hasLetters(Product.Description) == false)
            {
                ModelState.AddModelError(string.Empty, "Description must contain text.");
                return Page();
            }

            ProductService.UpdateCard(Product);
            // Redirect back to the admin page to continue editing if needed
            return RedirectToPage("./Admin");
        }

        /// <summary>
        /// Function tests whether the string t
        /// </summary>
        /// <returns></returns>
        private bool hasLetters(string strToCheck)
        {
            for (int i = 0; i < strToCheck.Length; i++)
            {
                if (char.IsLetter(strToCheck[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}