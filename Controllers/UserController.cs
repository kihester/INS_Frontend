using INS_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace INS_Frontend.controllers
{
    
    public class UserController : Controller
    {

        //Hosted web API REST Service base url
        String Baseurl = " http://localhost:5172";

        public async Task<ActionResult> All()
        {
            List<User> users = new List<User>();
            var accessToken = HttpContext.Session.GetString("JWToken");
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage response = await client.GetAsync("User");
                //Checking the response is successful or not which is sent using HttpClient
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var userResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the User list
                    users = JsonConvert.DeserializeObject<List<User>>(userResponse);
                }
                return View(users);
            }
        }
    }
}
