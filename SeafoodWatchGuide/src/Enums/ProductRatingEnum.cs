using System;

/// <summary>
/// This is the ENUM class for seafood ratings, which are displayed on each card and dictate
/// which section of the guide the fish card is displayed on.
/// </summary>
namespace ContosoCrafts.WebSite.RatingEnums
{
    /// <summary>
    /// Method declares the ENUMs for our product ratings
    /// </summary>
    public enum ProductRating
    {
        // EnumMember value is intened to convert the numerical ratings
        // in the JSON file to their respective ENUM. Still not working, bug logged
        UNKNOWN = 0,
        BEST_CHOICE = 1,
        GOOD_ALTERNATIVE = 2,
        AVOID = 3
    }

    /// <summary>
    /// This is an extension class for ENUMs to implement additional functionality
    /// </summary>
   public static class EnumExtensions
    {
        /// <summary>
        /// This will convert the ENUM values to a String format so it's easier for 
        /// users to read and not in variable format
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public static string ConvertToString(ProductRating rating)
        {
            string str = Enum.GetName(rating);
            str = str.Replace("_", " ");
            str = char.ToUpper(str[0]) + str.Substring(1).ToLower();

            return str;
        }
    }
}