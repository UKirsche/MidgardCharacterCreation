using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllgemeinWissenInventory : FillInventory<LernPlanInventoryDisplay> {


	// Use this for initialization
	public override void Start () {
		panelName = "AllgemeinWissen";
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
		List<InventoryItem> listItems = lernHelper.GetAllgemeinWissen();
		ConfigurePrefab (listItems);
	}
}
