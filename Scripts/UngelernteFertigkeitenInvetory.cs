using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UngelernteFertigkeitenInvetory : FillInventory<LernPlanInventoryDisplay> {


	// Use this for initialization
	public override void Start () {
		panelName = "UngelFertigkeiten";
		base.Start ();
	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	public override void FillPanel()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listItems = lernHelper.GetUngelernteFertigkeiten();
		ConfigurePrefab (listItems);
	}
}
