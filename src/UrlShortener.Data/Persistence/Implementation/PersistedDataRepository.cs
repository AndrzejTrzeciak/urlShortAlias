using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.DomainModel;
using UrlShortener.Data.Persistence.Interface;

namespace UrlShortener.Data.Persistence.Implementation
{
    /// <summary>
    /// this class shiould talk to SQL or Mongo,cloud storage or any data storage provider,
    /// for demonstration this is
    /// </summary>
    public class PersistedDataRepository : IPersistedDataRepository
    {
        private readonly IConfiguration configuration;
        private readonly string dbFileFullName;
        private readonly ILogger<PersistedDataRepository> logger;
        public PersistedDataRepository(IConfiguration configuration, ILogger<PersistedDataRepository> logger)
        {
            this.configuration = configuration;
            var dbName = this.configuration.GetSection("AppSettings:DbName").Value;
            dbFileFullName = Path.Combine(Directory.GetCurrentDirectory(), dbName);
            using (var db = new LiteDatabase(dbFileFullName))
            {
                var col = db.GetCollection<AddressAliasPair>("aliases");
                col.EnsureIndex(x => x.ShortURL);
            }

            this.logger = logger;
        }

        public bool ContatinsAlias(string shortAlias)
        {
            using (var db = new LiteDatabase(dbFileFullName))
            {
                var collection = db.GetCollection<AddressAliasPair>("aliases");
                var entry = collection.Find(x => x.ShortURL == shortAlias);
                if (entry.Any()) 
                { 
                    return true; 
                }
                return false;
            }
        }

        public string GetLongUrl(string shortUrl)
        {
            using (var db = new LiteDatabase(dbFileFullName))
            {
                var collection = db.GetCollection<AddressAliasPair>("aliases");
                var entry = collection.FindOne(x => x.ShortURL == shortUrl);
                if (entry != null)
                    return entry.OriginalURL;
                return null;
            }
        }

        public IEnumerable<AddressAliasPair> GetStoredUrls()
        {
            using (var db = new LiteDatabase(dbFileFullName))
            {
                var collection = db.GetCollection<AddressAliasPair>("aliases");
                var result = collection.FindAll().ToList().Select(x => x);                
                return result;
            }
        }

        public void SaveAlias(AddressAliasPair alias)
        {
            using (var db = new LiteDatabase(dbFileFullName))
            {
                var collection = db.GetCollection<AddressAliasPair>("aliases");
                try
                {
                    collection.Insert(alias);
                    logger.LogTrace("Alias inserted: "+ alias);
                }catch (Exception ex)
                {
                    logger.LogError("Error while saving new alias. Message:" + Environment.NewLine 
                        + ex.Message + Environment.NewLine + "Inner exception: " + Environment.NewLine  
                        + ex.InnerException + Environment.NewLine 
                        +"Stack trace: " + Environment.NewLine + ex.StackTrace);
                }
            }
        }
    }
}
