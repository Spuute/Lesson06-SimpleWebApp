using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace app.Pages
{
    public class addDishModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        public void OnPost()
        {
            var recipe = new Recipe {Name = Name};
            var client = new RestClient("https://restapi-cosmosdb.azurewebsites.net/api");
            var request = new RestRequest("/recipe?");
            request.AddJsonBody(recipe);
            var response = client.Post<Recipe>(request);
        }
    }
}
