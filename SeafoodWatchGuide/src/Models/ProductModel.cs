using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using ContosoCrafts.WebSite.RatingEnums;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Code version respenting the elements of the JSON file.
    /// </summary>
    public class ProductModel
    {
        //Keep track of the unique ID of the product
        public string Id { get; set; }

        //Contains the name of the product
        [StringLength(15, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        //Keep track of which region the product belongs to
        [Required]
        public string Region { get; set; }

        //Contains the rating of the product, should be 1 of the specified enums
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public ProductRating Rating { get; set; }

        //Contains the description of the product
        [StringLength(300)]
        [Required]
        public string Description { get; set; }

        //Keep track of the image that represents the product
        [JsonPropertyName("img")]
        [Required]
        [DataType(DataType.Url)]
        public string Image { get; set; }

        //Keep track of the Sources
        [ListStringLength(MaxStringLength = 75, MinStringLength = 3)]
        public List<string> Sources { get; set; }

        /// <summary>
        /// Function to get sources in a display format
        /// </summary>
        /// <returns></returns>
        public string GetSources()
        {
            string toReturn = "";
            for (int i = 0; i < Sources.Count; i++)
            {
                toReturn += Sources[i];

                toReturn += i == Sources.Count - 1 ? "" : ", ";   
            }

            return toReturn;
        }

        /// <summary>
        /// The is a toString method that will convert this class (the model) to a string version. 
        /// This is using the JSON mechanism for serializing the model data to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}