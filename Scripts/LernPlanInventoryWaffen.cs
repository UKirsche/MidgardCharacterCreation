using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LernPlanInventoryWaffen : FillInventory {


	// Use this for initialization
	public override void Start () {
		panelName = "WaffenPanel";
		base.Start();
	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	public override void FillPanel()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listItems = lernHelper.GetWaffenfertigkeitItems();
		ConfigurePrefab (listItems);
	}
}
