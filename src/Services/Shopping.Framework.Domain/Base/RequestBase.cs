using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Domain.Base
{
    public class ResponseBase
    {
        public string Code { get; set; } = ResponseBaseCode.Success;
        public string? Message { get; set; }

    }
    public class ResponseBase<T> : ResponseBase
    {
        public T? Data { get; set; }
    }
    public class ResponseBaseCode
    {
        public static string Success = "success";
        public static string Fail = "fail";
        public static string Existed = "existed";
    }
}
