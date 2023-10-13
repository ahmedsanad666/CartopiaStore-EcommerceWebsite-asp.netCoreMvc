using CartopiaStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using RestSharp;
using System.Text;
using System.Text.Json.Nodes;

namespace CartopiaStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {

        public IActionResult index()
        {
            string token = Request.Cookies["AuthToken"];
            string userId = Request.Cookies["UserId"];

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
              
            return View();
            }
                return RedirectToAction("index","Admin");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(Auth obj)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    string LogInApi = "https://dummyjson.com/auth/login";
                    //convert to json
                    string payload = JsonConvert.SerializeObject(obj);
                    // content type
                    HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                    //send response 
                    HttpResponseMessage response = client.PostAsync(LogInApi, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = response.Content.ReadAsStringAsync().Result;
                         var data =  JsonConvert.DeserializeObject<LogedUser>(responseData);
                        string token = data.Token;
                        int userId = data.Id;

                        // Create and add cookies
                        Response.Cookies.Append("AuthToken", token, new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(60) // Set the expiration time
                        });

                        Response.Cookies.Append("UserId", userId.ToString(), new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(60) // Set the expiration time
                        });

                        return RedirectToAction("index", "Admin");
                    }
                }
            }
          

            return RedirectToAction("Index");
        }
    }
}
