using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SheetInventoryDisplay : InventoryDisplay {

	public InventoryItemDisplay itemDisplayPrefab;

	/// <summary>
	/// Fills the item display: loads inventory items in panel
	/// </summary>
	/// <param name="items">Items.</param>
	public override void FillItemDisplay(List<InventoryItem> items)
	{
		foreach (InventoryItem item in items) {
			if (item != null) {
				InventoryItemDisplay itemToDisplay = (InventoryItemDisplay)Instantiate (itemDisplayPrefab);
				itemToDisplay.transform.SetParent (displayPanel, false);
				itemToDisplay.SetDisplayValuesWert (item);
			}
		}
	}
}
