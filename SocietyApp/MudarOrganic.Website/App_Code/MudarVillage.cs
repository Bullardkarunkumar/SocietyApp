using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MudarVillage
/// </summary>
public class MudarVillage
{
    public MudarVillage()
    {
        Farmers = new List<MudarFarmer>();
    }
    public string VillageName { get; set; }
    public int loopCount { get; set; }
    public List<MudarFarmer> Farmers { get; set; }
}