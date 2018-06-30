using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class InventoryItemBerufDisplay : MonoBehaviour {

	public Text nameItem; //Referenz auf UI-Elemente
	public Text wertItem; //Referenz auf UI-Lemente
	public InventoryItem item;


	public delegate void InventoryItemBerufDisplayDelegate(InventoryItemBerufDisplay item);
	public static event InventoryItemBerufDisplayDelegate  onClick;

	// Use this for initialization
	void Start () {

	}

	public void SetDisplayValuesCost(InventoryItem _item)
	{
		nameItem.text = _item.name;
		wertItem.text = _item.val;
		item = _item;
	}

	public void SetDisplayValuesWert(InventoryItem _item)
	{
		nameItem.text = _item.name;
		wertItem.text = _item.val;
		item = _item;
	}

	public void SetDisplayValuesName(InventoryItem _item)
	{
		nameItem.text = _item.name;
		item = _item;
	}

	public void Click()
	{
		if (onClick != null && item!=null) {
			onClick.Invoke(this);
		} 	
		Debug.Log("I Fach " + nameItem.text + " was clicked");
	}
}
