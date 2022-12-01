using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        public IActionResult ErrorChanged(string amountInput, string amountRange)
        {
            if (amountInput == null)
            {
                DataGenerator.countErrorToString = "0";
                DataGenerator.CountError = 0;
            }
            else
            {
                DataGenerator.countErrorToString = amountRange;
                DataGenerator.CountError = Convert.ToDouble(amountInput.Replace('.', ','));
            }
            return setDataGeneratorValues();
        }

        public IActionResult SeedOrRegionChanged(string seed, string region)
        {;
            int number = 0;
            bool isNumber = int.TryParse(seed, out number);
            if (isNumber) DataGenerator.Seed = number;
            else DataGenerator.Seed = 0;
            DataGenerator.Region = region;
            return setDataGeneratorValues();
        }

        private IActionResult setDataGeneratorValues()
        {
            DataGenerator.Page = 0;
            if (DataGenerator.Region == null) DataGenerator.Region = "ru";
            DataGenerator.Users = new List<User>();
            DataGenerator.Users = DataGenerator.GenerateUsers();
            DataGenerator.Users.AddRange(DataGenerator.GenerateUsers());
            return RedirectToAction("Index", "Home");
        }
    }
}