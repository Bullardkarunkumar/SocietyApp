using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Society.Models
{
  public  class TenureDetailsViewModel
    {
        public int Id { get; set; }
        public string TenureName { get; set; }
        public int StartDuration { get; set; }
        public int EndDuration { get; set; }
        public decimal RateOfIntrest { get; set; }
    }
}
