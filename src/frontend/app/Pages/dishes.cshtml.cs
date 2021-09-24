using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace app.Pages
{
    public class dishesModel : PageModel
    {
        public List<Recipe> Test;

        public void OnGet()
        {
            var client = new RestClient("https://restapi-cosmosdb.azurewebsites.net/api");
            var request = new RestRequest($"/recipe?", DataFormat.Json);

            var response = client.Execute<List<Recipe>>(request);
            Test = response.Data;
        }
    }
}
