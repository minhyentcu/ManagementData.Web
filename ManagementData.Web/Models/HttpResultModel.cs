using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementData.Web.Models
{
    public class HttpResultModel
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
    }
}