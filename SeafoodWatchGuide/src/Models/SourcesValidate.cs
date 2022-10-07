using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Custom data annotation, check string length in List
    /// </summary>
    public class ListStringLength : ValidationAttribute
    {
        //max character allowed in Kist item
        public int MinStringLength { get; set; }

        //min character allowed in Kist item
        public int MaxStringLength { get; set; }

        /// <summary>
        /// validate list items in value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null) {
                return new ValidationResult("The sources field is required.");
            }

            foreach (var str in value as List<string>)
            {
                if (str == null || str == "")
                {
                    return new ValidationResult("sources field is required");
                }

                if (Regex.Matches(str, @"[a-zA-Z]").Count <= 0)
                {
                    return new ValidationResult("sources require aphabetical characters.");
                }

                if (str.Length > MaxStringLength || str.Length < MinStringLength) 
                {
                    return new ValidationResult("sources length between " + MinStringLength + "-"+ MaxStringLength + ".");
                }
            }
            return ValidationResult.Success;
        }
    }
}