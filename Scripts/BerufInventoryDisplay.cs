using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class BerufInventoryDisplay : MonoBehaviour {

	// Use this for initialization
	public Transform displayPanel;
	public InventoryItemDisplay berufDisplayPrefab;
	public Text heading;

	/// <summary>
	/// Fills the item display: loads inventory items in panel
	/// </summary>
	/// <param name="items">Items.</param>
	public void FillBerufeDisplay(List<InventoryItem> items)
	{
		foreach (InventoryItem item in items) {
			if (item != null) {
				InventoryItemDisplay itemToDisplay = (InventoryItemDisplay)Instantiate (berufDisplayPrefab);
				itemToDisplay.transform.SetParent (displayPanel, false);
				itemToDisplay.SetDisplayValuesName (item);
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
