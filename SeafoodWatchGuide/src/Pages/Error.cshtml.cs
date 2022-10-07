using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Model for error page
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        //Variable to hold the ID of the error request
        public string RequestId { get; set; }

        //Variable to specify whether to show the request ID or not
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        //Logger to help with debugging when there is an error
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Constructor for Error page
        /// </summary>
        /// <param name="logger"></param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method to fetch error stack trace
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}