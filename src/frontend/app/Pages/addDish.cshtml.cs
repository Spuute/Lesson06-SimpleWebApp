using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace app.Pages
{
    [BindProperties(SupportsGet = true)]
    public class addDishModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment environment;
        public addDishModel(ILogger<addDishModel> logger, IHostingEnvironment environment)
        {
            this.environment = environment;
            _logger = logger;
        }

        public string Name { get; set; }
        public string Description1 { get; set; }
        public IFormFile Photo1 { get; set; }
        public void OnPost()
        {
            var hej = new Blobservice(environment);
            var uri = hej.UploadToAzureAsync(Photo1);
            try
            {
                var recipe = new Recipe
                {
                    Name = Name,
                    Description = Description1,
                    PhotoUrl = uri
                };

                var client = new RestClient("https://restapi-cosmosdb.azurewebsites.net/api");
                var request = new RestRequest("/recipe?");
                request.AddJsonBody(recipe);
                var response = client.Post<Recipe>(request);

                _logger.LogInformation($"Successfully saved to db");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

        }
    }
}
