using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiScheme.Scheme
{
    public class PlusIn : In
    {
        public int a, b;
        public string echo;
    }
    public class PlusOut : Out
    {
        public int c;
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
