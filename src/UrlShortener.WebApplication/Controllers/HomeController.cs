using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlShortener.Data.DomainModel;
using UrlShortener.Data.Services.Interface;
using UrlShortener.WebApplication.Models;

namespace UrlShortener.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IAddressService addressService;
        private readonly IUrlValidationService urlValidationService;

        public HomeController(ILogger<HomeController> logger, IAddressService addressService, IUrlValidationService urlValidationService)
        {
            this.logger = logger;
            this.addressService = addressService;
            this.urlValidationService = urlValidationService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult TechTaskDetails()
        {
            return View("TechTaskDetails");
        }

        public IActionResult AddAlias(string longUrl) 
        {
            if(!urlValidationService.ValidateUrl(longUrl))
            {
                logger.LogInformation("Invalid URL received - will not be added");
                return View("Error", new ErrorViewModel ());
            }
            var alias = addressService.GetShortAlias(longUrl);
            var viewModel = new AddressAliasPair
            {
                OriginalURL = longUrl,
                ShortURL = alias
            };
            return View("ShowAddedAddress", viewModel);
        }

        public IActionResult ShowAddAlias() 
        { 
            return View("AddAlias"); 
        }

        public IActionResult ShowAliasList()
        {
            var list = addressService.GetAddressAliases();
            return View("AliasList",list);
        }
    }
}