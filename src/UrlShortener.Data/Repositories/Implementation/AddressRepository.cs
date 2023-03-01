using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.DomainModel;
using UrlShortener.Data.Persistence.Interface;
using UrlShortener.Data.Repositories.Interface;
using UrlShortener.Data.Services.Interface;

namespace UrlShortener.Data.Repositories.Implementation
{
    public class AddressRepository : IAddressRepository
    {
        private IMemoryCache cache;
        private IPersistedDataRepository persistedData;
        private IRandomStringGenerator randomStringGenerator;
        private readonly IConfiguration configuration;
        private readonly ILogger<AddressRepository> logger;

        public AddressRepository(IMemoryCache cache,
            IPersistedDataRepository persistedData,
            IRandomStringGenerator randomStringGenerator,
            IConfiguration configuration)
        {
            this.cache = cache;
            this.persistedData = persistedData;
            this.randomStringGenerator = randomStringGenerator;
            this.configuration = configuration;
        }

        public IEnumerable<AddressAliasPair> GetAllAliases()
        {
            return persistedData.GetStoredUrls();
        }

        public string? GetLongAddressVersion(string shortAddress)
        {
            if(cache.TryGetValue(shortAddress, out string longAddress))
            {
                return longAddress;
            }
            else
            {
                var persistedLongUrl = persistedData.GetLongUrl(shortAddress);
                if(persistedLongUrl != null)
                {
                    var entry = cache.CreateEntry(shortAddress);
                    entry.Value = persistedLongUrl;
                    return persistedLongUrl;
                }
                logger.LogInformation($"real URL for requested alias not found. requested alias: {shortAddress}");
                return null;
            }
        }

        public string SaveNewAlias(string longAddress)
        {
            int aliassLength = int.Parse(configuration.GetSection("AppSettings:AliasLength").Value);
            var newShortAddress = randomStringGenerator.GenerateRandomString(aliassLength);  //TODO: introduce IConfiguration and store it as parameter
            while(persistedData.ContatinsAlias(newShortAddress)) 
            {
                logger.LogInformation("Random alias duplicate found, consider increasing config parameter AppSettings:AliasLength");
                newShortAddress = randomStringGenerator.GenerateRandomString(5);
            }
            AddressAliasPair newAliasPair = new AddressAliasPair
            {
                OriginalURL = longAddress,
                ShortURL = newShortAddress
            };
            persistedData.SaveAlias(newAliasPair);
            var entry = cache.CreateEntry(newShortAddress);
            entry.Value = longAddress;
            return newShortAddress;
        }
    }
}
