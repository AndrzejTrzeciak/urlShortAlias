using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.DomainModel;

namespace UrlShortener.Data.Persistence.Interface
{
    public interface IPersistedDataRepository
    {
        bool ContatinsAlias(string alias);
        void SaveAlias(AddressAliasPair alias);
        string GetLongUrl(string shortUrl);
        IEnumerable<AddressAliasPair> GetStoredUrls();
    }
}
