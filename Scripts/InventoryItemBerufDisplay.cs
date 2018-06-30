using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class InventoryItemBerufDisplay : InventoryItemDisplay {



	public delegate void InventoryItemBerufDisplayDelegate(InventoryItemBerufDisplay item);
	new public static event InventoryItemBerufDisplayDelegate  onClick;

	new public void Click()
	{
		if (onClick != null && item!=null) {
			onClick.Invoke(this);
		} 	
		Debug.Log("I Beruf " + nameItem.text + " was clicked");
	}
}
