using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Society.Models
{
    public class MembershipViewModel
    {
        public Int64 AdmissionNumber { get; set; } =0;
        public string MemberName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string SpouseName { get; set; } = string.Empty;
        public Int64 AadhaarNumber { get; set; } = 0;
        public Int64 PanNumber { get; set; } = 0;
        public DateTime? DoB { get; set; } = null;
        public int Age { get; set; } = 0;
        public string MobileNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string NomineeName { get; set; } = string.Empty;
        public string NomineeRelation { get; set; } = string.Empty;
        public int NoofShares { get; set; } = 0;
        public int ShareValue { get; set; } = 0;
        public int TotalShareAmt { get; set; } = 0;
        public string EntranceFee { get; set; } = string.Empty;

    }
}
