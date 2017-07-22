﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class InventoryItemDisplay : MonoBehaviour {

	public Text nameItem; //Referenz auf UI-Elemente
	public Text wertItem; //Referenz auf UI-Lemente
	public Text costItem;
	public InventoryItem item;


	public delegate void InventoryItemDisplayDelegate(InventoryItemDisplay item);
	public static event InventoryItemDisplayDelegate  onClick;

	// Use this for initialization
	void Start () {

	}

	public void SetDisplayValues(InventoryItem _item)
	{
		nameItem.text = _item.name;
		wertItem.text = _item.val;
		costItem.text = _item.cost.ToString ();
		item = _item;
	}


	public void Click()
	{
		if (onClick != null && item!=null) {
			onClick.Invoke(this);
		} else {
			Debug.Log("I " + nameItem.text + " was clicked");
		}
			
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
