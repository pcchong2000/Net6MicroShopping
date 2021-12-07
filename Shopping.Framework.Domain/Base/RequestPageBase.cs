using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Domain.Base
{
    public interface IRequestPageBase
    {
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
    public class RequestPageBase : IRequestPageBase
    {
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
    public class ResponsePageBase<T> : RequestBase, IRequestPageBase
    {
        public ResponsePageBase()
        {

        }
        public ResponsePageBase(RequestPageBase request)
        { 
            this.PageIndex = request.PageIndex;
            this.PageSize = request.PageSize;
        }
        public IEnumerable<T> List { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageTotal { get; set; }
    }
}
