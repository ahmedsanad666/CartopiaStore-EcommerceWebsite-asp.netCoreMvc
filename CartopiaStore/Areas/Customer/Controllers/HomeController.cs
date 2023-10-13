using CartopiaStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CartopiaStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewModel viewModel = new ViewModel();

            using (HttpClient client = new HttpClient())
            {
                string productsLink = "https://dummyjson.com/products";
                string categoriesLink = "https://dummyjson.com/products/categories";

                HttpResponseMessage productsResponse = await client.GetAsync(productsLink);
                HttpResponseMessage categoriesResponse = await client.GetAsync(categoriesLink);

                if (productsResponse.IsSuccessStatusCode)
                {
                    string productsData = await productsResponse.Content.ReadAsStringAsync();
                    var p = JsonConvert.DeserializeObject<ProductClass>(productsData);
                    foreach (var product in p.Products.Take(9))
                    {
                        product.priceAfterDiscount = product.price - (product.price * (product.discountPercentage / 100));
                    }

                    viewModel.Products = p.Products.Take(9).ToList();
                }

                if (categoriesResponse.IsSuccessStatusCode)
                {
                    string categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<string>>(categoriesData);
                    var category = new Category { Categories = data.Take(6).ToList() };
                    viewModel.Categories = category;
                }
            }

            return View(viewModel);

        }





        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            using (HttpClient client = new HttpClient())
            {
                ViewModel viewModel = new ViewModel();
                string apiUrl = "https://dummyjson.com/products"; // Replace with your actual API endpoint.
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string productsData = await response.Content.ReadAsStringAsync();
                    var p = JsonConvert.DeserializeObject<ProductClass>(productsData);

                    viewModel.Products = p.Products;
                    return PartialView("_ProductListPartial", viewModel.Products.Where(p => p.category == category).ToList());
                }
            }

            // Handle error or return an empty list of products.
            return PartialView("_ProductListPartial", new List<Product>());
        }
    }
}