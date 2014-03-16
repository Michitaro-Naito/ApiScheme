using ApiScheme.Scheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiScheme
{
    /// <summary>
    /// Represents errors which occured during WebAPI execution.
    /// </summary>
    public class ApiException : System.Exception
    {
        public ApiException()
        {

        }
        public ApiException(string message)
            : base(message)
        {
        }

        public static ApiException Create<T>(T response)
            where T : Out
        {
            if (response.exception == null)
                return null;
            var type = Type.GetType(response.exception);
            if (type == null || !typeof(ApiException).IsAssignableFrom(type))
                return new ApiException(string.Format("Unknown Exception:{0} Message:{1}", response.exception, response.message));
            return (ApiException)Activator.CreateInstance(type, new object[]{ response.message });
        }
    }

    /// <summary>
    /// Thrown only for debugging purposes.
    /// </summary>
    public class TestApiException : ApiException
    {
        public TestApiException(string message)
            : base(message)
        {

        }
    }
}
