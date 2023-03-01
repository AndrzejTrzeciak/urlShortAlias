using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.DomainModel;

namespace UrlShortener.Data.Repositories.Interface
{
    public interface IAddressRepository
    {
        string? GetLongAddressVersion(string shortAddress);
        string SaveNewAlias(string longAddress);

        IEnumerable<AddressAliasPair> GetAllAliases();
    }
}
