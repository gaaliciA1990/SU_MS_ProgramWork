using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Class for managing the Service for the feedback JSON file
    /// </summary>
    public class JsonFileFeedbackService
    {
        // Class member variable for setting our JSON writing options when reading data
        private JsonWriterOptions writerOptions = new JsonWriterOptions { SkipValidation = false, Indented = true };

        /// <summary>
        /// Constructor method for the web host environment
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileFeedbackService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// A getter method for the webhost
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// This simplified the file path for the JSON file so it can be inferred instead
        /// of being hardcoded. Makes tracking data easier
        /// </summary>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "SiteFeedback.json"); }
        }

        /// <summary>
        /// This is creating a list of Feedback Models.
        /// IEnumerable is a list type
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FeedbackModel> GetAllFeedback()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<FeedbackModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Create a new feedback rating using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns></returns>
        public FeedbackModel CreateFeedback(int rating, string comment)
        {
            var feedback = new FeedbackModel()
            {
                ID = System.Guid.NewGuid().ToString(),
                Rating = rating,
                Comment = comment
            };

            // Get the current set, and append the new record to it becuase IEnumerable does not have Add
            var feedbackSet = GetAllFeedback();
            feedbackSet = feedbackSet.Append(feedback);

            SaveFeedback(feedbackSet);

            return feedback;
        }

        /// <summary>
        /// Save All feedback data to storage
        /// </summary>
        private void SaveFeedback(IEnumerable<FeedbackModel> feedback)
        {

            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<FeedbackModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    feedback
                );
            }
        }
    }
}