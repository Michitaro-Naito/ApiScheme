using ApiScheme.Scheme;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ApiScheme.Client
{
    public static class Api
    {
        public static string apiServerUrl = null;

        /// <summary>
        /// Calls WebAPI. HTTP GET.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException">HTTP errors like 404, 500.</exception>
        /// <exception cref="ApiScheme.ApiException">Failed to call WebAPI. Returned from ApiServer. Can be derived Exceptions.</exception>
        /// <exception cref="System.Exception">Unknown errors.(Caused by bugs of this lib.)</exception>
        [DebuggerStepThrough]
        public static T Request<T>(string method, In request)
            where T : Out, new()
        {
            // Valid URL?
            if (apiServerUrl == null)
                apiServerUrl = ConfigurationManager.AppSettings["ApiServer"];
            if (apiServerUrl == null)
                throw new ArgumentNullException("Can't be null. Set Api.apiServerUrl or ConfigurationManager.AppSettings[\"ApiServer\"]");

            // Valid set of Request and Response?
            var t = new T();
            var inName = request.GetType().Name;
            var outName = t.GetType().Name;
            var name = Regex.Replace(inName, "In$", "");
            var outNameRequired = name + "Out";
            if (!Regex.Match(inName, "In$").Success)
                throw new ArgumentException(string.Format("InvalidClass. In:{0} must be named like ***In.", inName));
            if (outName != outNameRequired)
                throw new ArgumentException(string.Format("ClassName not match. In:{0} Out:{1} OutRequired:{2}", inName, outName, outNameRequired));

            // Calls ApiServer
            /*var path = "Api/Call";
            var req = WebRequest.Create(apiServerUrl + path + "?name=" + name + "&json=" + HttpUtility.UrlEncode(JsonConvert.SerializeObject(request)));
            string json = null;
            try
            {
                using (var res = req.GetResponse())
                {
                    using (var rs = res.GetResponseStream())
                    using (var reader = new StreamReader(rs, Encoding.UTF8))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var rs = e.Response.GetResponseStream())
                    using (var reader = new StreamReader(rs, Encoding.UTF8))
                    {
                        var body = reader.ReadToEnd();
                        throw new WebException(body);
                    }
                }
                throw;
            }
            catch (System.Exception e)
            {
                throw e;
            }*/

            // New impl
            var url = apiServerUrl + "Api/Call";
            string json = null;
            try
            {
                using (var wc = new WebClient())
                {
                    if (method == "GET")
                    {
                        var res = wc.DownloadData(url + "?name=" + name + "&json=" + HttpUtility.UrlEncode(JsonConvert.SerializeObject(request)));
                        json = Encoding.UTF8.GetString(res);
                    }
                    else if (method == "POST")
                    {
                        var res = wc.UploadValues(url, "POST", new System.Collections.Specialized.NameValueCollection()
                        {
                            {"name", name},
                            {"json", JsonConvert.SerializeObject(request)}
                        });
                        json = Encoding.UTF8.GetString(res);
                    }
                    else
                        throw new NotImplementedException("Method not implemented");
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var rs = e.Response.GetResponseStream())
                    using (var reader = new StreamReader(rs, Encoding.UTF8))
                    {
                        var body = reader.ReadToEnd();
                        throw new WebException(body);
                    }
                }
                throw;
            }
            catch (System.Exception)
            {
                throw;
            }

            // Valid json?
            var response = JsonConvert.DeserializeObject<T>(json);
            if (response == null)
                throw new System.Exception("Unknown error. Deserialized JSON is null. JSON string: " + json);

            // Server Exception?
            var apiException = ApiException.Create(response);
            if (apiException != null)
                throw apiException;

            return response;
        }

        [DebuggerStepThrough]
        public static T Get<T>(In request)
            where T : Out, new()
        {
            return Request<T>("GET", request);
        }

        [DebuggerStepThrough]
        public static T Post<T>(In request)
            where T : Out, new()
        {
            return Request<T>("POST", request);
        }
    }
}
