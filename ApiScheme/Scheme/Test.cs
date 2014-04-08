using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiScheme.Scheme
{
    public class PlusIn : In
    {
        /// <summary>
        /// Calculates a + b = c.
        /// </summary>
        public int a;

        /// <summary>
        /// Calculates a + b = c.
        /// </summary>
        public int b;

        /// <summary>
        /// This string will be echoed.
        /// </summary>
        public string echo;
    }
    public class PlusOut : Out
    {
        /// <summary>
        /// A result of a + b = c.
        /// </summary>
        public int c;

        /// <summary>
        /// A result of echo.
        /// </summary>
        public string echo;
    }
    public class PlusInvalidInn : In
    {
        public int a, b;
        public string echo;
    }
    public class PlusInvalidOut : Out
    {
        public int c;
        public string echo;
    }

    public class GetExceptionIn : In
    {

    }
    public class GetExceptionOut : Out
    {

    }
}
