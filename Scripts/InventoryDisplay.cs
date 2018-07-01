using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryDisplay: MonoBehaviour {

	public Transform displayPanel;
	public Text heading;

	/// <summary>
	/// Fills the item display: loads inventory items in panel
	/// </summary>
	/// <param name="items">Items.</param>
	public abstract void FillItemDisplay(List<InventoryItem> items);


	/// <summary>
	/// Sets the heading for the inventory items
	/// </summary>
	/// <param name="titel">Titel.</param>
	public void SetHeading(string titel)
	{
		heading.text = titel;
	}

}
