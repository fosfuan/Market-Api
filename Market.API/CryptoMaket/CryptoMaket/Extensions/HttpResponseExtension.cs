using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMaket.Extensions
{
    public static class HttpResponseExtension
    {
        public static bool IsSuccessStatusCode(this HttpResponse request)
        {
            return ((int)request.StatusCode >= 200) && ((int)request.StatusCode <= 299);
        }
    }
}
