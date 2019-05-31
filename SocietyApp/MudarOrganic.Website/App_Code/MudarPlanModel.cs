using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MudarPlanModel
/// </summary>
public class MudarPlanModel
{
    public string FarmerID { get; set; }
    public string FarmerName { get; set; }
    public string FarmerCode { get; set; }
    public string VillageName { get; set; }
    public Guid InspectorCode { get; set; }
    public string InspectorName { get; set; }
    public DateTime PlanDate { get; set; }
    public DateTime VisitedDate { get; set; }

}