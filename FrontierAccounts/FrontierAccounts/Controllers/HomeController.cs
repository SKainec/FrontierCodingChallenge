using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontierAccounts.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace FrontierAccounts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Account> accounts = null;

            //TODO: move the code to get the accounts into its own service which I would 
            //inject into the construct of this controller. For now, since we only have one 
            //get request, we're doing this in the code.
            using (var client = new HttpClient()) 
            {
                //We need to get the api asynchronously since we aren't sure how log it'll take. So we wait.
                var task = client.GetAsync("https://frontiercodingtests.azurewebsites.net/api/accounts/getall");
                task.Wait();
                
                var response = task.Result;

                //If it comes back successfully, we parse the accounts. Otherwise we add a model error indicating an issue.
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var strResult = readTask.Result;
                    accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(strResult);
                }
                else 
                {
                    ModelState.AddModelError(string.Empty, "We had an issue getting the accounts from the server.");
                }

                

            }
            return View(accounts);
        }

        public IActionResult CustomerInfo(IEnumerable<Account> accounts) {
            return PartialView(accounts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
