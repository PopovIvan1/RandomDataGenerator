using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using RandomDataGenerator.Models;
using System.Diagnostics;

namespace RandomDataGenerator.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string GenerateNewUsers()
        {
            var users = new List<User>();
            users = DataGenerator.GenerateUsers();
            DataGenerator.Users.AddRange(users);
            string result = "";
            foreach (var user in users) 
            {
                result = result + "<tr> <td>" + user.Number.ToString() + "</td> <td>"
                    + user.Id.ToString() + "</td> <td>"
                    + user.Name + "</td> <td>" 
                    + user.Address + "</td> <td>"
                    + user.Phone + "</td> </tr>";
            }
            return result;
        }

        public IActionResult RegenerateUsers(string amountInput, string seed, string region, string amountRange)
        {
            setDataGeneratorValues(amountInput, seed, region, amountRange);
            DataGenerator.Users = DataGenerator.GenerateUsers();
            DataGenerator.Users.AddRange(DataGenerator.GenerateUsers());
            return RedirectToAction("Index", "Home");
        }

        private void setDataGeneratorValues(string amountInput, string seed, string region, string amountRange)
        {
            DataGenerator.Page = 0;
            DataGenerator.countErrorToString = amountRange;
            DataGenerator.CountError = Convert.ToDouble(amountInput.Replace('.',','));
            DataGenerator.Seed = int.Parse(seed);
            DataGenerator.Region = region;
            DataGenerator.Users = new List<User>();
        }
    }
}