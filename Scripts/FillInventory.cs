using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class FillInventory : MonoBehaviour {

	//Verweis auf zu füllendes Panel
	public LernPlanInventoryDisplay inventoryDisplayPrefab;
	public Transform displayParent;

	protected string panelName = "PanelName";


	// Use this for initialization
	public virtual void Start () {

		//Fülle den Mist auf
		FillPanel();

	}

	/// <summary>
	/// Takes list and creates and fills display prefab
	/// </summary>
	/// <param name="listItems">List items.</param>
	protected void ConfigurePrefab(List<InventoryItem> listItems){
		//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
		inventoryDisplayPrefab = (LernPlanInventoryDisplay)Instantiate (inventoryDisplayPrefab);
		inventoryDisplayPrefab.name = panelName;
		inventoryDisplayPrefab.transform.SetParent (displayParent, false);
		inventoryDisplayPrefab.FillItemDisplay (listItems);
	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	public abstract void FillPanel();
}