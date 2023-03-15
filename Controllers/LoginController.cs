using INS_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace INS_Frontend.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["status"] != null)
            {
                ViewBag.message = "Incorrect userId or password";
                TempData.Remove("status");
            }
            return View();
        }

        public async Task<IActionResult> LoginUser(UserLogin userLogin)
        {

            String url = "http://localhost:5172/Login";

            using (var httpClient=new HttpClient())
            {
                StringContent stringContent = new StringContent(
                    JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(url, stringContent))
                {
                    string tokenResponse = await response.Content.ReadAsStringAsync();  
                   // might need to change 
                    if(tokenResponse == "User not found")
                    {
                        TempData["status"] = "Failed";
                        return Redirect("~/login/index");
                    }
                    HttpContext.Session.SetString("JWToken", tokenResponse);
                }
                return Redirect("~/user/all");
            }
        }

        public async Task<IActionResult> Logoff()
        {
            // remove token
            HttpContext.Session.Clear();
            return Redirect("~/login/index");
        }
    }
}
