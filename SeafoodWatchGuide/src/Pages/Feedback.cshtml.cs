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
    /// Home page for Seafood Watch Guide from GladiatorMonkys 
    /// group in Fundamentals of Software Engineering, CPSC 5110
    /// </summary>
    public class FeedbackPageModel : PageModel
    {
        //Logger to help with debugging
        private readonly ILogger<FeedbackPageModel> _logger;

        //To get connected to all provided services
        public JsonFileFeedbackService FeedbackService { get; }

        //To store all products currently in database
        public IEnumerable<FeedbackModel> AllFeedbacks { get; private set; }

        //To store highest feedbacks currently in database
        public IEnumerable<FeedbackModel> previewFeedBacks { get; set; }

        //To store submitted feedback
        [BindProperty]
        public FeedbackModel Feedback { get; set; }

        //Display thank you message after submitting feedback
        public bool displayThankYouMessage;

        /// <summary>
        /// Constructor for Index page
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public FeedbackPageModel(ILogger<FeedbackPageModel> logger, JsonFileFeedbackService feedbackService)
        {
            _logger = logger;
            FeedbackService = feedbackService;
            AllFeedbacks = FeedbackService.GetAllFeedback();
        }

        /// <summary>
        /// Method to fetch all items to be shown on the Index page
        /// </summary>
        public void OnGet()
        {
            previewFeedBacks = AllFeedbacks.Reverse();

            displayThankYouMessage = false;
        }

        /// <summary>
        /// Method to react to action of submitting a feedback via Submit button.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            //Return to page with error messages
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            //Save the feedback if everything checks off
            FeedbackService.CreateFeedback(Feedback.Rating, Feedback.Comment);
            //return RedirectToPage("./Feedback");
            displayThankYouMessage = true;
            return Page();
        }

    }
}