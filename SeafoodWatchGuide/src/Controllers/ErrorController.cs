using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult CodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return RedirectToPage("/NotFound");
                default:
                    return RedirectToPage("/Error");
            }
        }
    }
}
