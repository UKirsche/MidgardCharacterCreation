using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BerufInventory : MonoBehaviour {

	//Verweis auf zu füllendes Panel
	public BerufInventoryDisplay inventoryDisplayPrefab;
	public Transform displayParent;

	private const string panelName = "Berufe";


	// Use this for initialization
	void OnEnable () {

		//Fülle den Mist auf
		FillPanelBerufe();

	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	private void FillPanelBerufe()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Lösche alte Einträge
		RemoveItemDisplay();

		//Prepare listItems 
		List<InventoryItem> listItems =lernHelper.GetBerufe();


		//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
		inventoryDisplayPrefab = (BerufInventoryDisplay)Instantiate (inventoryDisplayPrefab);
		inventoryDisplayPrefab.name = panelName;
		inventoryDisplayPrefab.transform.SetParent (displayParent, false);
		inventoryDisplayPrefab.FillBerufeDisplay (listItems);
	}

	void RemoveItemDisplay ()
	{
		InventoryItemDisplay[] displayItems = gameObject.GetComponentsInChildren<InventoryItemDisplay> ();
		foreach (var itemDisplay in displayItems) {
			itemDisplay.item.activated = false;
			itemDisplay.gameObject.SetActive (false);
		}
	}
}
