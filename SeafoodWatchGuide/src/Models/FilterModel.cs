using ContosoCrafts.WebSite.RatingEnums;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Code version respenting the elements of the filter
    /// </summary>
    public class FilterModel
    {
        //Store which Region was chosen for filtering
        public string Region { get; set; }

        // Store filter name for search function
        public string Name { get; set; }

        //Store which Rating was chosen for filtering
        public ProductRating Rating { get; set; }

        /// <summary>
        /// Function to clear all current filter data
        /// </summary>
        public void ClearData()
        {
            Region = null;
            Rating = ProductRating.UNKNOWN;
        }
    }
}