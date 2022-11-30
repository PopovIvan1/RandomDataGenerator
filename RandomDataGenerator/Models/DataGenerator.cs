using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace RandomDataGenerator.Models
{
    public static class DataGenerator
    {
        public static string? Region { get; set; }
        public static double CountError { get; set; }
        public static string countErrorToString { get; set; } = "0";
        public static int Seed { get; set; }
        public static int Page { get; set; }
        public static List<User> Users { get; set; } = new List<User>();
        public static List<User> GenerateUsers()
        {
            List<User> users = new List<User>();
            var userParser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserParser>(File.ReadAllText("wwwroot/" + Region + ".json"))!;

            for (int i = 0; i < 10; i++)
            {
                users.Add(User.GenerateUser(((Seed + Page) * 10) + i + 1, (Page * 10) + i + 1, CountError, userParser));
            }

            Page++;
            return users;
        }
    }
}
