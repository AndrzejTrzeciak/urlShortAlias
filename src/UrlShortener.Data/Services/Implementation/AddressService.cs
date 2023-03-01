using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.DomainModel;
using UrlShortener.Data.Repositories;
using UrlShortener.Data.Repositories.Interface;
using UrlShortener.Data.Services.Interface;

namespace UrlShortener.Data.Services.Implementation
{
    public class AddressService : IAddressService
    {

        private readonly IConfiguration configuration;
        private readonly IAddressRepository addressRepository;

        public AddressService(IConfiguration configuration, 
            IAddressRepository addressRepository)
        {
            this.configuration = configuration;
            this.addressRepository = addressRepository;
        }

        public IEnumerable<AddressAliasPair> GetAddressAliases()
        {
            return this.addressRepository.GetAllAliases();
        }

        public string GetLongUrl(string shortAlias)
        {
            //return "www.wp.pl";
            string longUrl = addressRepository.GetLongAddressVersion(shortAlias);
            return longUrl;
        }

        public string GetShortAlias(string longURL)
        {
            string basePath = configuration.GetSection("AppSettings:BasePath").Value;            
            var shortAlias = addressRepository.SaveNewAlias(longURL);
            return $@"{basePath}/{shortAlias}";
        }

        //public string SaveNewAlias(string longURL)
        //{
        //    return "hhhhh";
        //}
    }
}
