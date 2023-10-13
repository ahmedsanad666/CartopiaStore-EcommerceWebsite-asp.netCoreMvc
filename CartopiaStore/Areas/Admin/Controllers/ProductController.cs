using CartopiaStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CartopiaStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
           List<Product> allProducts = new List<Product>();
            
            using (HttpClient client = new HttpClient())
            {
                string productsLink = "https://dummyjson.com/products";
               

                HttpResponseMessage productsResponse = await client.GetAsync(productsLink);
              

                if (productsResponse.IsSuccessStatusCode)
                {
                    string productsData = await productsResponse.Content.ReadAsStringAsync();
                    var p = JsonConvert.DeserializeObject<ProductClass>(productsData);
                    foreach (var product in p.Products.Take(6))
                    {
                        product.priceAfterDiscount = product.price - (product.price * (product.discountPercentage / 100));
                    }

                    allProducts = p.Products.ToList();

                }

              
              
            }

            return View(allProducts);
        }

      
        
        // POST  add new product 

        public IActionResult Create()
        {
            Product product =new Product();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            Product addedProduct = null;

            using (HttpClient client = new HttpClient())
            {
                string addProductLink = "https://dummyjson.com/products/add";

               
                string productJson = JsonConvert.SerializeObject(product);
                HttpContent content = new StringContent(productJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(addProductLink, content);

                if (response.IsSuccessStatusCode)
                {
                    string addedProductData = await response.Content.ReadAsStringAsync();
                    addedProduct = JsonConvert.DeserializeObject<Product>(addedProductData);

                    
                    addedProduct.priceAfterDiscount = addedProduct.price - (addedProduct.price * (addedProduct.discountPercentage / 100));
                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }
        // GET product by id 
        // PUT REQUESAT  UPDATE PRODUCT     
        public async Task<IActionResult> Edit(int id)
        {
            Product product = null;
            if (id == null)
            {
                return NotFound();
            }

            using (HttpClient client = new HttpClient())
            {
                string productLink = $"https://dummyjson.com/products/{id}";

                HttpResponseMessage productResponse = await client.GetAsync(productLink);

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(productData);


                    product.priceAfterDiscount = product.price - (product.price * (product.discountPercentage / 100));

                    return View(product);
                }
            }

            return NotFound(); 
        }

      

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product updatedProduct)
        {
            Product updatedProductData = null;

            using (HttpClient client = new HttpClient())
            {
                string updateProductLink = $"https://dummyjson.com/products/{id}";

                
                string updatedProductJson = JsonConvert.SerializeObject(updatedProduct);
                HttpContent content = new StringContent(updatedProductJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(updateProductLink, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    updatedProductData = JsonConvert.DeserializeObject<Product>(data);

                   
                    updatedProductData.priceAfterDiscount = updatedProductData.price - (updatedProductData.price * (updatedProductData.discountPercentage / 100));
               return RedirectToAction("Index");
                }
            }

            return View(updatedProduct);
        }

        // delete product by id 


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string deleteProductLink = $"https://dummyjson.com/products/{id}";

                HttpResponseMessage response = await client.DeleteAsync(deleteProductLink);

                if (response.IsSuccessStatusCode)
                {
                  
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    return View("Error");
                }
            }
        }


    }
}
