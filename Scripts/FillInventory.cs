using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class FillInventory<T> : FillInventoryBase where T:InventoryDisplay{

	//Verweis auf zu füllendes Panel
	public T inventoryDisplayPrefab;

	// Use this for initialization
	public virtual void Start () {

		//Fülle den Mist auf
		FillPanel();

	}

	/// <summary>
	/// Takes list and creates and fills display
	/// </summary>
	/// <param name="listItems">List items.</param>
	protected void ConfigurePrefab(List<InventoryItem> listItems){

		InventoryDisplay _inventoryDisplayPrefab=null;

		if (typeof(T) == typeof(LernPlanInventoryDisplay)) {
			//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
			_inventoryDisplayPrefab = Instantiate (inventoryDisplayPrefab) as LernPlanInventoryDisplay;
		} else if (typeof(T) == typeof(SheetInventoryDisplay)) {
			_inventoryDisplayPrefab = Instantiate (inventoryDisplayPrefab) as SheetInventoryDisplay;
		} else if (typeof(T) == typeof(BerufInventoryDisplay)) {
			_inventoryDisplayPrefab = Instantiate (inventoryDisplayPrefab) as BerufInventoryDisplay;
		} 

		_inventoryDisplayPrefab.name = panelName;
		_inventoryDisplayPrefab.transform.SetParent (displayParent, false);
		_inventoryDisplayPrefab.FillItemDisplay (listItems);
	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	public abstract void FillPanel();
}