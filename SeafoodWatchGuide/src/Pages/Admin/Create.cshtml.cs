using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;


namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// The Create Model class provides the structure for how the Create
    /// page should behave.
    /// </summary>
    public class CreateModel : PageModel
    {
        // ILogger variable
        private readonly ILogger<CreateModel> _logger;

        /// <summary>
        /// Constructor method for the Create Page
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public CreateModel(ILogger<CreateModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        //Product service object to provide all available services
        public JsonFileProductService ProductService { get; }

        // Getter and Setter method for the Creat page
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Since we are creating a new record in the dataset, we don't need to pull anything for validation
        /// </summary>
        public void OnGet()
        {

        }

        /// <summary>
        /// Upon clicking Submit, the update is saved and the user is redirected back to the Admin page to 
        /// so they can update more information
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
            if(hasLetters(Product.Title) == false)
            {
                ModelState.AddModelError(string.Empty, "Title must contain text.");
                return Page();
            }

            //Check if Description only has a bunch of numbers and symbols
            if(hasLetters(Product.Description) == false)
            {
                ModelState.AddModelError(string.Empty, "Description must contain text.");
                return Page();
            }

            //Check on url, whether the link leads to a valid image file
            if(validURL() == false)
            {
                ModelState.AddModelError(string.Empty, "Please enter an image url with the following extensions: .jpg, .jpeg, .png, .gif");
                return Page();
            }


            //If valid, create the card
            ProductService.CreateCard(Product);
            return RedirectToPage("Admin");
        }

        /// <summary>
        /// Function tests whether the input url directs to an actual image.
        /// Accepted image extensions are: jpg, jpeg, png and gif
        /// </summary>
        /// <returns></returns>
        private bool validURL()
        {
            string[] acceptedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            foreach (string ext in acceptedExtensions) {
                if (Product.Image.Contains(ext))
                {
                    return true;
                }
            }
            
            return false;
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