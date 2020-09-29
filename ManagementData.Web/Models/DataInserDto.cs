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
        public DateTime? TimeCreate { get; set; }
    }

    public class DataDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreate { get; set; }
        public string UserId { get; set; }
        public string TimeString { get; set; }
        public int Stt { get; set; }
        public override string ToString()
        {
            return $"{Id} {Text} {TimeCreate.ToString("yyyy-mm-dd hh:mm:ss.fff")}";
        }
    }

    public class DataResult
    {
        public string Text { get; set; }
        public DateTime? TimeCreate { get; set; }
    }
}