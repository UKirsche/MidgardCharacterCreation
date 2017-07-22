using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LernPlanInventoryWaffen : MonoBehaviour {

	//Verweis auf zu füllendes Panel
	public LernPlanInventoryDisplay inventoryDisplayPrefab;
	public Transform displayParent;

	private const string panelName = "WaffenPanel";


	// Use this for initialization
	void Start () {

		//Fülle den Mist auf
		FillPanelWaffen();

	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	private void FillPanelWaffen()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listItems = lernHelper.GetWaffenfertigkeitItems();

		//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
		inventoryDisplayPrefab = (LernPlanInventoryDisplay)Instantiate (inventoryDisplayPrefab);
		inventoryDisplayPrefab.name = panelName;
		inventoryDisplayPrefab.transform.SetParent (displayParent, false);
		inventoryDisplayPrefab.FillItemDisplay (listItems);
	}
}
