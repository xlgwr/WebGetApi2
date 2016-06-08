using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Net.Http;
using Common.Logging;
using System.Web.Mvc;
using System.Web.Http.Filters;

namespace GetPanamaDBWebApi.Filters
{
    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            ILog log = LogManager.GetLogger(HttpContext.Current.Request.Url.LocalPath);
            log.Error(context.Exception);

            var message = context.Exception.Message;
            if (context.Exception.InnerException != null)
                message = context.Exception.InnerException.Message;

            context.Response = new HttpResponseMessage() { Content = new StringContent(message) };

            base.OnException(context);
        }
    }
}