﻿using CartopiaStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CartopiaStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewModel viewModel = new ViewModel();
            using(HttpClient client = new HttpClient())
            {

                string ProductsApi = "https://dummyjson.com/products";
                string CatApi = "https://dummyjson.com/products/categories";

                HttpResponseMessage ProductResponse = await client.GetAsync(ProductsApi);
                HttpResponseMessage CatResponse = await client.GetAsync(CatApi);

                if (ProductResponse.IsSuccessStatusCode)
                {
                    string productsData = await ProductResponse.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ProductClass>(productsData);
                    foreach (var product in data.Products)
                    {
                        product.priceAfterDiscount = product.price - (product.price * (product.discountPercentage / 100));
                    }

                    viewModel.Products = data.Products;
                }
                if (CatResponse.IsSuccessStatusCode)
                {
                    string categoriesData = await CatResponse.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<string>>(categoriesData);
                    var category = new Category { Categories = data };
                    viewModel.Categories = category;
                }

            }

            return View(viewModel);
        }
    }
}