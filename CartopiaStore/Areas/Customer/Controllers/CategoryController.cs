using CartopiaStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CartopiaStore.Areas.Customer.Controllers
{
    public class CategoryController : Controller
    {
        [Area("Customer")]
        public   IActionResult Index()
        {
           
            return View();

        }
    }
}
