using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SheetFillZauberInventory : FillInventory<SheetInventoryDisplay> {

	// Use this for initialization
	public override void Start () {
		panelName = "SheetZauber";
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
		List<InventoryItem> listItems = mCharacter.zauberFormeln;
		listItems.AddRange (mCharacter.zauberLieder);
		listItems.AddRange (mCharacter.zauberSalze);
		ConfigurePrefab (listItems);
	}
}
