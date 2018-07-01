using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LernplanIventory : FillInventory<LernPlanInventoryDisplay> {

	// Use this for initialization
	public override void Start () {

		panelName= "Fachpanel";
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
		List<InventoryItem> listItems = lernHelper.GetFachkenntnisseItems();
		ConfigurePrefab (listItems);
		PreSelectZwerge (globalVars);
	}

	/// <summary>
	/// Bei Zwergen muss Baukunde geklickt werden.
	/// </summary>
	/// <param name="globalVars">Global variables.</param>
	void PreSelectZwerge (Toolbox globalVars)
	{
		//Bei Zwergen muss Baukunde direkt geklickt werden
		MidgardCharakter mCharacter = globalVars.mCharacter;
		if (mCharacter.Spezies == Races.Zwerg) {
			InventoryItemDisplay[] arrayItemDisplayFach = inventoryDisplayPrefab.GetComponentsInChildren<InventoryItemDisplay> ();
			foreach (var itemDisplayFach in arrayItemDisplayFach) {
				if (itemDisplayFach.nameItem.text == "Baukunde") {
					itemDisplayFach.Click ();
				}
			}
		}
	}
}
