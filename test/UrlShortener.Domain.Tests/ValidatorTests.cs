using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void ValidUrlTest()
        {
            //var expression = @"/^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$/";
            var exp = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex rgx = new Regex(exp, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            var sampleUrls = new List<string>();
            sampleUrls.Add(@"https://regex101.com/");
            sampleUrls.Add(@"https://stackoverflow.com/questions/1288046/how-can-i-get-my-webapps-base-url-in-asp-net-mvc");
            foreach (var url in sampleUrls)
            {
                var result = rgx.Match(url);
                Assert.IsTrue(result.Success);
            }
        }
        [Test]
        public void InvalidUrlTest()
        {
            var exp = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex rgx = new Regex(exp, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            var sampleUrls = new List<string>();
            //sampleUrls.Add(@"https://regex101.coń/");
            sampleUrls.Add(@"httos://stackoverflow.com/questions/1288046/how-can-i-get-my-webapps-base-url-in-asp-net-mvc");
            sampleUrls.Add(@"https:/stackoverflow.com/questions/1288046/how-can-i-get-my-webapps-base-url-in-asp-net-mvc");
            foreach (var url in sampleUrls)
            {
                var result = rgx.Match(url);
                Assert.IsFalse(result.Success);
            }
        }
    }
}
