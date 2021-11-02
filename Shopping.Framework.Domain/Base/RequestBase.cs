using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Domain.Base
{
    public class RequestBase
    {
        public string Code { get; set; } = RequestBaseCode.Success;
        public string? Message { get; set; }

    }
    public class RequestBase<T> : RequestBase
    {
        public T Data { get; set; }
    }
    public class RequestBaseCode
    {
        public static string Success = "Success";
        public static string Fail = "Fail";
        public static string Existed = "Existed";
    }
}
