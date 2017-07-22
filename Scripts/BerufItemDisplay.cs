using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class BerufItemDisplay : MonoBehaviour {

	public Text nameItem; //Referenz auf UI-Elemente

	public InventoryItem item;


	public delegate void BerufItemDisplayDelegate(BerufItemDisplay item);
	public static event BerufItemDisplayDelegate  onClick;

	// Use this for initialization
	void Start () {

	}

	public void SetDisplayValues(InventoryItem _item)
	{
		nameItem.text = _item.name;
		item = _item;
	}


	public void Click()
	{
		if (onClick != null && item!=null) {
			onClick.Invoke (this);
		} else {
			Debug.Log("IBeruf " + nameItem.text + " was clicked");
		}

	}

	// Update is called once per frame
	void Update () {

	}
}
