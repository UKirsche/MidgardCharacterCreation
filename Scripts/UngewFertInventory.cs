using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UngewFertInventory : FillInventory {

	// Use this for initialization
	public override void Start () {
		panelName = "UngewFertigkeiten";
		base.Start();
	}


	public override void FillPanel()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listItems = lernHelper.GetUngewFertigkeiten();
		ConfigurePrefab (listItems);

	}
}
