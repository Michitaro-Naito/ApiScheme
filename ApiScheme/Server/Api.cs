using ApiScheme.Scheme;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiScheme.Server
{
    public static class Api
    {
        /*public static T ToRequest<T>(string json)
            where T:IApiRequest
        {
            if (json == null)
                throw new ArgumentNullException("Request json must not be null.");
            var req = JsonConvert.DeserializeObject<T>(json);
            if (req == null)
                throw new ArgumentException("Failed to parse JSON.");
            return req;
        }*/
        public static T ToIn<T>(string json)
            where T : In
        {
            if (json == null)
                throw new ArgumentNullException("Request json must not be null.");
            var req = JsonConvert.DeserializeObject<T>(json);
            if (req == null)
                throw new ArgumentException("Failed to parse JSON.");
            return req;
        }
    }
}
