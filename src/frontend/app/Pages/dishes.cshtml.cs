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
    public class dishesModel : PageModel
    {
        private readonly ILogger<dishesModel> _logger;
        public dishesModel(ILogger<dishesModel> logger)
        {
            _logger = logger;
        }

       
        public List<Recipe> Recipes;

        public void OnGet()
        {
            try
            {
                var client = new RestClient("https://restapi-cosmosdb.azurewebsites.net/api");
                var request = new RestRequest($"/recipes", DataFormat.Json);

                var response = client.Execute<List<Recipe>>(request);
                Recipes = response.Data;
                _logger.LogInformation($"Successfull read from db");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
