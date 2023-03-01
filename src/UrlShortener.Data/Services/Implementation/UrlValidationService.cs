using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using UrlShortener.Data.Services.Interface;

namespace UrlShortener.Data.Services.Implementation
{
    public class UrlValidationService : IUrlValidationService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<UrlValidationService> logger;

        public UrlValidationService(IConfiguration configuration, ILogger<UrlValidationService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public bool ValidateUrl(string url)
        {
            //string? expression = @"/^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$/";

            var exp = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex rgx = new Regex(exp, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            //configuration.GetSection("AppSettings:ValidationRegex").Value;
            //if (expression == null)
            //{
            //    logger.LogWarning("Validation not configured, input url not validated");
            //    return true; //validation not configured    
            //}
            //var result = Regex.IsMatch(expression, url);
            var result = rgx.Match(url).Success;
            if(!result)
            {
                logger.LogInformation($"received invalid url for alias generation. Input url: {url}");
            }
            return result;
        }
    }
}
