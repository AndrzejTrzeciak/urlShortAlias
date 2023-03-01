using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Data.Services.Interface
{
    public interface IRandomStringGenerator
    {
        string GenerateRandomString(int length);
    }
}
