using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class BerufInventoryDisplay : InventoryDisplay {
	public InventoryItemBerufDisplay berufDisplayPrefab;

	/// <summary>
	/// Fills the item display: loads inventory items in panel
	/// </summary>
	/// <param name="items">Items.</param>
	public override void FillItemDisplay(List<InventoryItem> items)
	{
		foreach (InventoryItem item in items) {
			if (item != null) {
				InventoryItemBerufDisplay itemToDisplay = (InventoryItemBerufDisplay)Instantiate (berufDisplayPrefab);
				itemToDisplay.transform.SetParent (displayPanel, false);
				itemToDisplay.SetDisplayValuesName (item);
			}
		}
	}
}
