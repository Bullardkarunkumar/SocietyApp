using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MudarItemModel
/// </summary>
public class MudarItemModel
{
	public MudarItemModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Guid FarmerId { get; set; }
    public int FarmId { get; set; }
    public int PlantationId { get; set; }
    public decimal Quantity { get; set; }
}