using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class HandleFachBeruf : MonoBehaviour {

	public InventoryItemDisplay itemDisplayPrefab;
	LernPlanHelper lpHelper;
	private int lastKatClicked;

	void OnEnable(){
		InventoryItemDisplay.onClick +=	HandleOnItemClick;
	}

	void OnDisable(){
		InventoryItemDisplay.onClick -= HandleOnItemClick;
	}


	void OnDestroy ()
	{
		Debug.Log ("usigned for Click");
		InventoryItemDisplay.onClick -= HandleOnItemClick;
	}


	/// <summary>
	/// Handles the on item click. 
	/// </summary>
	/// <param name="itemDisplay">Item display.</param>
	public void HandleOnItemClick (InventoryItemDisplay itemDisplay)
	{
		//Display aus dem der Click stammt
		string contextItemDisplay = itemDisplay.transform.parent.name;
		lpHelper = Toolbox.Instance.lernHelper;

		//Hier sind die Arten der Panels zu speicher, die vom Click betroffen sein können
		Transform allgemeinPanelDisplay=null;

		string righPanelShort = "Gewählt";
		GameObject fachAllgemein = GameObject.Find ("FachGewählt");
		allgemeinPanelDisplay = fachAllgemein.transform;
		int aktKategorieClicked = itemDisplay.item.cost;

		//Geklicktes Item schon aktiviert -> deaktivieren
		if (itemDisplay.item.activated) {
			if (contextItemDisplay.Contains(righPanelShort)) {
				itemDisplay.item.activated = false;
				itemDisplay.gameObject.SetActive (false);
				lpHelper.FachBerufClicked -= 1;
				lpHelper.KategorieClicked -= aktKategorieClicked;
				DeleteFertigkeitFromCharacter (itemDisplay.item);
			}
		} else { //Neues Item einfügen, falls nicht aktiv-> verschiedene Zielpanels 
			bool addMore = CheckBerufFachOk(itemDisplay);
			if(addMore){
				lastKatClicked = aktKategorieClicked;
				lpHelper.FachBerufClicked += 1;
				lpHelper.KategorieClicked += aktKategorieClicked;
				InsertNewItem (itemDisplay, contextItemDisplay,lpHelper, allgemeinPanelDisplay); 
				AddFertigkeitToCharacter (itemDisplay.item);
				lpHelper.lernPunkteResetUngewFertigkeiten = false; //jetzt kann das Panel mit Inhalt bleiben
			}
		}
	}
		
	public 


	/// <summary>
	/// Inserts the new item. Dazu werden übergeben: QuellItem, auf das geklickt wurde, QuellPanel, abzuziehende LPs sowie das Zielpanel
	/// </summary>
	/// <param name="itemDisplay">Item display.</param>
	/// <param name="contextItemDisplay">Context item display.</param>
	/// <param name="lernpunkteDelta">Lernpunkte delta.</param>
	/// <param name="lpHelper">Lp helper.</param>
	/// <param name="rightPanelDisplay">Right panel display.</param>
	void InsertNewItem (InventoryItemDisplay itemDisplay, string contextItemDisplay, LernPlanHelper lpHelper, Transform rightPanelDisplay)
	{
		itemDisplay.item.activated = true;
		//erzeuge neues item
		CreateInventoryItem (itemDisplay, rightPanelDisplay);
	}


	/// <summary>
	/// Checks the beruf fach ok. 
	/// 
	/// </summary>
	private bool CheckBerufFachOk(InventoryItemDisplay itemClicked){
		int beruf100 = lpHelper.BerufswahlW100;
		int katAktuell = itemClicked.item.cost;
		if (beruf100 <= 95) {
			if (lpHelper.FachBerufClicked == 0) {
				return true;
			} 
		} else {
			if (lpHelper.FachBerufClicked  == 0 ||(lpHelper.FachBerufClicked  == 1 && lpHelper.KategorieClicked <3 && katAktuell<3)  ) {
				return true;
			}
		}
		return false;
	}


	/// <summary>
	/// Creates the inventory item aus dem Prefab, setzt parent und values ein
	/// </summary>
	/// <param name="itemDisplay">Item display.</param>
	/// <param name="rightPanelDisplay">Right panel display.</param>
	void CreateInventoryItem (InventoryItemDisplay itemDisplay, Transform rightPanelDisplay)
	{
		InventoryItemDisplay itemToDisplay = (InventoryItemDisplay)Instantiate (itemDisplayPrefab);

		itemToDisplay.transform.SetParent (rightPanelDisplay, false);
		itemToDisplay.SetDisplayValuesCost (itemDisplay.item);
	}

	void AddFertigkeitToCharacter (InventoryItem item)
	{
		string type = item.type;
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;

		if (type == "Fach") {
			mCharacter.fertigkeiten.Add (item);
		} else if(type.Contains("waffe")) {
			mCharacter.waffenFertigkeiten.Add (item);
		} 
	}


	void DeleteFertigkeitFromCharacter (InventoryItem item)
	{
		string type = item.type;
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;

		if (type == "Fach") {
			mCharacter.fertigkeiten.Remove (item);
		} else if(type.Contains("waffe")) {
			mCharacter.waffenFertigkeiten.Remove (item);
		} 
	}
}
