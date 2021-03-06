﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BerufInventory : FillInventory<BerufInventoryDisplay> {

	public override void Start(){
		panelName = "Berufe";
	}

	// Use this for initialization
	void OnEnable () {
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
		ConfigurePrefab(listItems);
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