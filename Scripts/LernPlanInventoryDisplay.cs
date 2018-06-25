using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LernPlanInventoryDisplay : MonoBehaviour {

	public Transform displayPanel;
	public InventoryItemDisplay itemDisplayPrefab;
	public Text heading;

	/// <summary>
	/// Fills the item display: loads inventory items in panel
	/// </summary>
	/// <param name="items">Items.</param>
	public void FillItemDisplay(List<InventoryItem> items)
	{
		foreach (InventoryItem item in items) {
			if (item != null) {
				InventoryItemDisplay itemToDisplay = (InventoryItemDisplay)Instantiate (itemDisplayPrefab);
				itemToDisplay.transform.SetParent (displayPanel, false);
				itemToDisplay.SetDisplayValuesCost (item);
			}
		}
	}
		

	/// <summary>
	/// Sets the heading for the inventory items
	/// </summary>
	/// <param name="titel">Titel.</param>
	public void SetHeading(string titel)
	{
		heading.text = titel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
