using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Class contains the structure for Privacy page and how it should behave
    /// </summary>
    public class PrivacyModel : PageModel
    {
        //Logger to help with debugging
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// Constructor for Privacy page
        /// </summary>
        /// <param name="logger"></param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// OnGet method for Privacy page. Essentially doing nothing for now
        /// </summary>
        public void OnGet()
        {
        }
    }
}