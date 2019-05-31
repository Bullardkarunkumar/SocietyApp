using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudarOrganic.BL
{
    public class SeasonProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SeasonName { get; set; }
        public int SeasonId { get; set; }
        public bool Selected { get; set; }
    }
}
