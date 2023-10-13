using CartopiaStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CartopiaStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : AdminControllerBase
    {
        public async  Task<IActionResult> Index()
        {
            AdminModel adminModel = new AdminModel();
            using (HttpClient client = new HttpClient())
            {
                string productsLink = "https://dummyjson.com/products";
                string categoriesLink = "https://dummyjson.com/products/categories";
                string usersApi = "https://dummyjson.com/users";

                HttpResponseMessage productsResponse = await client.GetAsync(productsLink);
                HttpResponseMessage categoriesResponse = await client.GetAsync(categoriesLink);
                HttpResponseMessage usersResponse = await client.GetAsync(usersApi);

                if (productsResponse.IsSuccessStatusCode)
                {
                    string productsData = await productsResponse.Content.ReadAsStringAsync();
                    var p = JsonConvert.DeserializeObject<ProductClass>(productsData);
                    foreach (var product in p.Products.Take(6))
                    {
                        product.priceAfterDiscount = product.price - (product.price * (product.discountPercentage / 100));
                    }
                    ViewBag.ProductCount = p.Products.Count;
                    adminModel.Products = p.Products.Take(6).ToList();

                }

                if (categoriesResponse.IsSuccessStatusCode)
                {
                    string categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<string>>(categoriesData);
                    var category = new Category { Categories = data.Take(6).ToList() };
                    ViewBag.CatCount = data.Count;
                    adminModel.Categories = category;
                }
                if (usersResponse.IsSuccessStatusCode)
                {
                    string userData =  usersResponse.Content.ReadAsStringAsync().Result;
                    var userResponse = JsonConvert.DeserializeObject<AdminModel>(userData);

                    if (userResponse != null && userResponse.Users != null)
                    {
                        List<User> users = userResponse.Users.Take(6).ToList();
                        ViewBag.userCount = userResponse.Users.Count;
                        adminModel.Users = users;
                    }
              
                    
                   
                }
            }

            return View(adminModel);
        }
    }
}
