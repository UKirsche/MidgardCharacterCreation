using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UngewFertInventory : MonoBehaviour {

	//Verweis auf zu füllendes Panel
	public LernPlanInventoryDisplay inventoryDisplayPrefab;
	public Transform displayParent;

	private const string panelName = "UngewFertigkeiten";


	// Use this for initialization
	void Start () {

		//Fülle den Mist auf
		FillPanelUngewFertigkeiten();

	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	private void FillPanelUngewFertigkeiten()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listItems = lernHelper.GetUngewFertigkeiten();


		//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
		inventoryDisplayPrefab = (LernPlanInventoryDisplay)Instantiate (inventoryDisplayPrefab);
		inventoryDisplayPrefab.name = panelName;
		inventoryDisplayPrefab.transform.SetParent (displayParent, false);
		inventoryDisplayPrefab.FillItemDisplay (listItems);
	}
}
