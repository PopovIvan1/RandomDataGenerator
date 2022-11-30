namespace RandomDataGenerator.Models
{
    public class User
    {
        private static UserParser _userParser;
        private static Random _rnd;

        public int Number { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public static User GenerateUser(int seed, int number, double countError, UserParser jsonParseUser)
        {
            _rnd = new Random(seed);
            _userParser = jsonParseUser;

            var user = new User()
            {
                Id = seed,
                Number = number,
                Name = GenerateName(),
                Address = GenerateAddress(),
                Phone = GeneratePhone()
            };

            return GenerateErrors(user, countError);
        }

        private static string GenerateName()
        {
            var gender = _rnd.Next(2) == 1 ? _userParser!.Male : _userParser!.Female;
            return string.Join(" ", gender.FirstName[_rnd.Next(gender.FirstName.Length)],
                                   gender.MiddleName?[_rnd.Next(gender.MiddleName.Length)],
                                   gender.LastName[_rnd.Next(gender.LastName.Length)]);
        }

        private static string GenerateAddress()
        {
            var city = string.Format("{0}, _, {1}, {2}", _userParser.City[_rnd.Next(_userParser.City.Count)], _rnd.Next(300), _rnd.Next(1000));
            city = city.Replace("_", _userParser.Street[_rnd.Next(_userParser.Street.Count)]);

            return string.Join(", ", _userParser.Region[_rnd.Next(_userParser.Region.Count)], city);
        }

        private static string GeneratePhone()
        {
            return string.Format("{0}{1}{2}", _userParser.Phone.CodeCountry, _userParser.Phone.RegionCode[_rnd.Next(_userParser.Phone.RegionCode.Count)], _rnd.Next(9999999));
        }


        private static User GenerateErrors(User user, double countError)
        {

            int lenghtPhone = user.Phone.Length;
            int lenghtName = user.Name.Length;
            int lenghtAddress = user.Address.Length;

            int fractionalPart = (int)((countError - Math.Truncate(countError)) * 100);
            if (_rnd.Next(fractionalPart) > fractionalPart / 2)
                countError++;

            for (int i = 0; i < countError; i++)
            {
                switch (_rnd.Next(0, 3))
                {
                    case 0:
                        user.Phone = ErrorHandler(user.Phone, lenghtPhone);
                        break;
                    case 1:
                        user.Name = ErrorHandler(user.Name, lenghtName);
                        break;
                    case 2:
                        user.Address = ErrorHandler(user.Address, lenghtAddress);
                        break;
                }
            }

            return user;
        }

        private static string ErrorHandler(string value, int defaultCount)
        {
            if ((value.Length * 100) / defaultCount > 103)
                return RemoveSymbolInElement(value);
            else if ((value.Length * 100) / defaultCount < 97)
                return InsertSymbolInElement(value);
            else
            {
                return _rnd.Next(0, 3) switch
                {
                    0 => RemoveSymbolInElement(value),
                    1 => InsertSymbolInElement(value),
                    2 => SwapSymbolsInElement(value),
                    _ => value
                };
            }
        }

        private static string RemoveSymbolInElement(string value) => value.Remove(_rnd.Next(value.Length), 1);
        private static string InsertSymbolInElement(string value) => value.Insert(_rnd.Next(value.Length), _userParser.Letter[_rnd.Next(_userParser.Letter.Length - 1)].ToString());
        private static string SwapSymbolsInElement(string value)
        {
            int pos = _rnd.Next(value.Length - 1) + 1;
            char[] strToCharArray = value.ToCharArray();
            char tmp = strToCharArray[pos];
            strToCharArray[pos] = strToCharArray[pos - 1];
            strToCharArray[pos - 1] = tmp;
            return string.Concat(strToCharArray);
        }
    }
}
