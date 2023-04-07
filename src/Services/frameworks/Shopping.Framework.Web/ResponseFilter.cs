using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shopping.Framework.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Web
{
    public class ResponseFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult==null)
                {
                    return;
                }
                if (objectResult.Value is HttpResponseMessage)
                {
                }
                else if (objectResult.Value is ResponseBase)
                { 
                }
                else
                {
                    if (objectResult.StatusCode != null && objectResult.StatusCode >= 400)
                    {
                    }
                    else
                    {
                        context.Result = new OkObjectResult(new ResponseBase<object>()
                        {
                            Code= ResponseBaseCode.Success,
                            Message= ResponseBaseCode.Success,
                            Data = objectResult.Value
                        });
                    }
                }
            }
        }
    }
}
