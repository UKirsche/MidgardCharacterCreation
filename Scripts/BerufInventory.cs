﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BerufInventory : FillInventory {

	//Verweis auf zu füllendes Panel
	public BerufInventoryDisplay inventoryBerufDisplayPrefab;




	// Use this for initialization
	void OnEnable () {
		panelName = "Berufe";
		//Fülle den Mist auf
		FillPanel();
	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	public override void FillPanel()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Lösche alte Einträge
		RemoveItemDisplay();

		//Prepare listItems 
		List<InventoryItem> listItems =lernHelper.GetBerufe();


		//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
		inventoryBerufDisplayPrefab = (BerufInventoryDisplay)Instantiate (inventoryBerufDisplayPrefab);
		inventoryBerufDisplayPrefab.name = panelName;
		inventoryBerufDisplayPrefab.transform.SetParent (displayParent, false);
		inventoryBerufDisplayPrefab.FillBerufeDisplay (listItems);
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
