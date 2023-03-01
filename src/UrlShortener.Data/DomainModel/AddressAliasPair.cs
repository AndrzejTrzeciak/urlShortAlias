using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Data.DomainModel
{
    [Serializable]
    public class AddressAliasPair
    {
        public string? OriginalURL { get; set; }
        public string? ShortURL { get; set; }
        //public int? Id { get; set; }
    }
}
