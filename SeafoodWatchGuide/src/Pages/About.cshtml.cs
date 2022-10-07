using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Page place holder for base code from index page
    /// </summary>
    public class AboutModel : PageModel
    {
        //Logger to help with debugging
        private readonly ILogger<AboutModel> _logger;

        /// <summary>
        /// Constructor for About page
        /// </summary>
        /// <param name="logger"></param>
        public AboutModel(ILogger<AboutModel> logger)
        {
            _logger = logger;
            /// ProductService = productService;
        }

        /// <summary>
        /// Placeholder method to fetch info needed
        /// </summary>
        public void OnGet()
        {
        }
    }
}