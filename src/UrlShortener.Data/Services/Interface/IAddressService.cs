using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.DomainModel;

namespace UrlShortener.Data.Services.Interface
{
    public interface IAddressService
    {
        string GetShortAlias(string longURL);
        
        string GetLongUrl(string shortAlias);

        IEnumerable<AddressAliasPair> GetAddressAliases();
    }
}
