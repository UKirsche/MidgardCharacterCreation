using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


/// <summary>
/// Handle beruf. Registriert den Klick auf Berufs-Items. Diesen werden
/// </summary>
public class HandleBeruf : MonoBehaviour {
	public InventoryItemDisplay itemDisplayPrefab;
	private LernPlanHelper lpHelper;
	void OnEnable(){
		BerufItemDisplay.onClick +=	HandleOnBerufClick;
	}

	void OnDisable(){
		BerufItemDisplay.onClick -= HandleOnBerufClick;
	}


	void OnDestroy ()
	{
		Debug.Log ("usigned for Click");
		BerufItemDisplay.onClick -= HandleOnBerufClick;
	}


	/// <summary>
	/// Handles the on item click. 
	/// </summary>
	/// <param name="itemDisplay">Item display.</param>
	public void HandleOnBerufClick (BerufItemDisplay itemDisplay)
	{
		Toolbox globalVars = Toolbox.Instance;
		lpHelper = globalVars.lernHelper;
		string berufsWahl = itemDisplay.item.name;
		Dictionary<string, List<Beruf>> berufsKenntnisse = lpHelper.Berufskenntnisse;
		List<Beruf> wahlFach;
		berufsKenntnisse.TryGetValue (berufsWahl, out wahlFach); //Hole Fachkenntnisse für den Beruf

		Transform berufPanelDisplay=null;
		GameObject fachAllgemein = GameObject.Find ("FachBeruf");
		GameObject fachGewählt = GameObject.Find ("FachGewählt");
		berufPanelDisplay = fachAllgemein.transform;

		int berufsWurf = lpHelper.BerufswahlW100; 


		//ClearScreen und fülle auf
		lpHelper.ResetPanelFachbBerufeClicked();
		RemoveItemDisplay(fachGewählt);
		RemoveItemDisplay(fachAllgemein);
		CreateFachItemsForBeruf(wahlFach, berufPanelDisplay); //Zeige Items
	}
		

	/// <summary>
	/// Removes the items from the panel
	/// </summary>
	/// <param name="_gameObject">Game object.</param>
	void RemoveItemDisplay (GameObject _gameObject)
	{
		InventoryItemDisplay[] displayItems = _gameObject.GetComponentsInChildren<InventoryItemDisplay> ();
		foreach (var itemDisplay in displayItems) {
			itemDisplay.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Creates the fach items for beruf.
	/// Erzeug InventoryItems
	/// </summary>
	/// <returns>The fach items for beruf.</returns>
	/// <param name="wahlFach">Wahl fach.</param>
	private void CreateFachItemsForBeruf(List<Beruf> wahlFach, Transform berufPanelDisplay){
		List<InventoryItem> listItems = lpHelper.GetFachkenntnisseItemsForBeruf (wahlFach);
		foreach (var item in listItems) {
			CreateInventoryItemDisplay (item, berufPanelDisplay);
		}
	}

	/// <summary>
	/// Creates the inventory item aus dem Prefab, setzt parent und values ein
	/// </summary>
	/// <param name="itemDisplay">Item display.</param>
	/// <param name="rightPanelDisplay">Right panel display.</param>
	void CreateInventoryItemDisplay (InventoryItem item, Transform berufPanelDisplay)
	{
		InventoryItemDisplay itemToDisplay = (InventoryItemDisplay)Instantiate (itemDisplayPrefab);
		itemToDisplay.transform.SetParent (berufPanelDisplay, false);
		itemToDisplay.SetDisplayValues (item);
	}
}
