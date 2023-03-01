using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data.DomainModel;
using UrlShortener.Data.Services.Interface;

namespace UrlShortener.WebApplication.Controllers
{
    public class UrlTranslatorController : Controller
    {
        private readonly IAddressService addressService;

        public UrlTranslatorController(IAddressService addressService)
        {
            this.addressService = addressService;
        }
        
        [Route("/{shortUrl}")]
        public IActionResult GetLongUrl(string shortUrl)
        {
            var realAddress = addressService.GetLongUrl(shortUrl);
            return new RedirectResult(realAddress);
            //return Redirect(realAddress);            
        }
    }
}
