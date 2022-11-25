using Shopping.UI.MemberApp.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services
{
    public class ResponseBase
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
    public class ResponseBase<T> : ResponseBase
    {
        public T Data { get; set; }
    }
}
