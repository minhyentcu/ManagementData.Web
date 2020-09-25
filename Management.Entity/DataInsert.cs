using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Entity
{
  public class DataInsert
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreate { get; set; }
        public string UserId { get; set; }
    }
}
