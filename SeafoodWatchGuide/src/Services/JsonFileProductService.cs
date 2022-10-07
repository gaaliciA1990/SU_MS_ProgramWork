using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Class for managing the Service for seafood JSON file
    /// </summary>
    public class JsonFileProductService
    {
        // Class member variable for setting our JSON writing options when reading data
        private JsonWriterOptions writerOptions = new JsonWriterOptions { SkipValidation = false, Indented = true };

        /// <summary>
        /// Constructor method for the web host environment
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// A getter method for the webhost
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment
        {
            get;
        }

        /// <summary>
        /// This simplified the file path for the JSON file so it can be inferred instead
        /// of being hardcoded. Makes tracking data easier
        /// </summary>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "Seafoods.json"); }
        }

        /// <summary>
        /// This is creating a list of product Models.
        /// IEnumerable is a list type
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductModel> GetAllData()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// This returns a list all cards belonging to a region.
        /// </summary>
        public IEnumerable<ProductModel> GetRegionData(string region)
        {
            // Get the all current data set
            var data = GetAllData();
            // get all region data from dataset
            var regionData = from d in data
                             where d.Region == region
                             orderby d.Title
                             select d;
            return regionData;
        }

        /// <summary>
        /// This method will pull all of the Seafood products and appeand the data entered in the form
        /// to the JSON file and serialize the data
        /// </summary>
        /// <param name="model"></param>
        public void CreateCard(ProductModel model)
        {
            //Get the current set, and append the new record to it
            var products = GetAllData();

            products = products.Append(model);

            SaveData(products);
        }

        /// <summary>
        /// Update the item from the System
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProductModel UpdateCard(ProductModel data)
        {
            //Get the current set, and append the new record to it
            var products = GetAllData();
            // Get the single data record from dataset that has same id
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (productData == null)
            {
                return null;
            }

            productData.Title = data.Title;
            productData.Description = data.Description;
            productData.Image = data.Image;
            productData.Rating = data.Rating;
            productData.Region = data.Region;
            productData.Sources = data.Sources;

            SaveData(products);

            return productData;
        }

        /// <summary>
        /// private funtion used to save data
        /// </summary>
        /// <param name="products"></param>
        private void SaveData(IEnumerable<ProductModel> products)
        {
            // this checks out the JSON file for us to edit and ensures
            // nothing else can edit it while we are using it
            using (var outputStream = File.Create(JsonFileName))
            {
                // This is writing the entered data from our form and saving it to the JSON file with our 
                // preset writer options
                JsonSerializer.Serialize(new Utf8JsonWriter(outputStream, writerOptions), products);
            }
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns></returns>
        public ProductModel DeleteCard(string id)
        {

            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            // Get the single data record from dataset
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));
            // get all data except the delete items from Jsonfile
            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);

            SaveData(newDataSet);

            return data;
        }

        /// <summary>
        /// Place holder for all Regions currently supported (will change once we
        /// get our json files for each region up and running
        /// </summary>
        public string[] GetAllRegions()
        {
            return new string[7]{"West Coast", "Southwest", "Central", "Southeast",
                                "Northeast", "Hawaii", "National"};
        }
    }
}