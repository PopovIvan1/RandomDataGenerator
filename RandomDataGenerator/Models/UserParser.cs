using System.Numerics;

namespace RandomDataGenerator.Models
{
    public class UserParser
    {
        public UserFIO Male { get; set; }
        public UserFIO Female { get; set; }
        public List<string> City { get; set; }
        public List<string> Street { get; set; }
        public List<string> Region { get; set; }
        public Phone Phone { get; set; }
        public string Letter { get; set; }
    }

    public class UserFIO
    {
        public string[] FirstName { get; set; } = default!;
        public string[]? MiddleName { get; set; }
        public string[] LastName { get; set; } = default!;
    }

    public class Phone
    {
        public string CodeCountry { get; set; } = default!;
        public List<string> RegionCode { get; set; } = default!;
    }
}
