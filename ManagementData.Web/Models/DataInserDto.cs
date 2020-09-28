using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementData.Web.Models
{
    public class DataInserDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreate { get; set; }
    }
}