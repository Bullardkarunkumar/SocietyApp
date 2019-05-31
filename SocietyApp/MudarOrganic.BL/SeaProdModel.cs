using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudarOrganic.BL
{
    public class SeaProdModel
    {
        public int SeasonId { get; set; }
        public int ProductId { get; set; }
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
