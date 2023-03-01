using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Services.Interface;

namespace UrlShortener.Data.Services.Implementation
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        private const string allowedCharacters = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqRrsStTuUvVwWxXyYzZ123456789";
        public string GenerateRandomString(int length)
        {
            Random random = new Random();
           
            var chars = Enumerable.Range(0, length)
                .Select(x => allowedCharacters[random.Next(0, allowedCharacters.Length -1)]);
            return new string(chars.ToArray());

        }
    }
}
