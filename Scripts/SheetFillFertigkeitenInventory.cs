using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SheetFillFertigkeitenInventory : FillInventory<SheetInventoryDisplay> {

	// Use this for initialization
	public override void Start () {
		panelName = "SheetFertigkeiten";
		base.Start ();
	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	public override void FillPanel()
	{
		Toolbox globalVars = Toolbox.Instance;
		MidgardCharakter mCharacter = globalVars.mCharacter;

		//Prepare listItems 
		List<InventoryItem> listItems = mCharacter.fertigkeiten;
		ConfigurePrefab (listItems);
	}
}