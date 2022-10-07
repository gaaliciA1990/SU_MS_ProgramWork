using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Code version respenting the elements of the feedback JSON file
    /// </summary>
    public class FeedbackModel
    {
        // Store rating ID
        public string ID { get; set; }

        //Store the rating value
        [Required]
        public int Rating { get; set; }

        // Store the comments entered by users for the site feedback
        [StringLength (maximumLength:500, MinimumLength = 3, ErrorMessage = "Comments should have a length of more than {2} and less than {1}")]
        public string Comment { get; set; }

        /// <summary>
        /// The is a toString method that will convert this class (the model) to a string version. 
        /// This is using the JSON mechanism for serializing the model data to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString() => JsonSerializer.Serialize<FeedbackModel>(this);
    }
}