using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Common.Logging;
using System.Web.Mvc;

namespace GetPanamaDBWebApi.Filters
{
    public class MvcHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ILog log = LogManager.GetLogger(filterContext.RequestContext.HttpContext.Request.Url.LocalPath);
            log.Error(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}