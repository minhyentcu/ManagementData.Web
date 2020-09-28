using ManagementData.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ManagementData.Web.FilterAttribute
{
    public class FilterAttributeData : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            var author = actionContext.Request.Headers.Authorization;
            if (author != null)
            {
                var scheme = author.Scheme;
                var token = author.Parameter;
            }


            actionContext.Response = new System.Net.Http.HttpResponseMessage( System.Net.HttpStatusCode.Unauthorized);
            //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
            //    new HttpResultModel
            //    {
            //        StatusCode = (int)HttpStatusCode.Forbidden,
            //        Message = "Unauthorized"
            //    });
        }
    }
}