using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace app.Pages
{
    public class addDishModel : PageModel
    {
        private readonly ILogger _logger;
        public addDishModel(ILogger<addDishModel> logger)
        {
            _logger = logger;

        }
        [BindProperty]
        public string Name { get; set; }

        public void OnPost()
        {
            try
            {
                var recipe = new Recipe { Name = Name };
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
