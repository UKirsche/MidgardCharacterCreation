using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LernPlanInventoryZauber : FillInventory<LernPlanInventoryDisplay> {

	// Use this for initialization
	public override void Start () {
		panelName = "ZauberPanel";
		base.Start();
	}

	/// <summary>
	/// Fills the panel with Zauber. 
	/// </summary>
	public override void FillPanel()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listZauberformeln = lernHelper.GetZauberFormeln();
		List<InventoryItem> listZaubersalze = lernHelper.GetZauberSalze();
		List<InventoryItem> listZauberlieder = lernHelper.GetZauberLieder();

		//Concat lists:
		listZauberformeln.AddRange(listZaubersalze);
		listZauberformeln.AddRange (listZauberlieder);

		ConfigurePrefab (listZauberformeln);

	}
		
}

