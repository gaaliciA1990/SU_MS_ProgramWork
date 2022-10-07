using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Take Action page for Seafood Watch Guide to provide site users with
    /// more information on how they can help. Shouldn't need a model, but 
    /// keeping just in case
    /// </summary>
    public class TakeActionModel : PageModel
    {
        //Logger to help with debugging
        private readonly ILogger<TakeActionModel> _logger;

        /// <summary>
        /// Constructor for Privacy page
        /// </summary>
        /// <param name="logger"></param>
        public TakeActionModel(ILogger<TakeActionModel> logger)
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