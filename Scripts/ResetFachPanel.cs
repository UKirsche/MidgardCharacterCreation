using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResetFachPanel : MonoBehaviour {

	public InputField inputPointsLeft, inputPointsPool;
	private LernPlanHelper lernHelper;
	// Use this for initialization
	void Start () {
	
	}

	// Resettet das Fachpanel, falls neu gewürfelt wurde
	void OnEnable(){

		Toolbox globalVars = Toolbox.Instance;
		lernHelper = globalVars.lernHelper;

		if (lernHelper.lernPunkteResetFach == true && gameObject.name =="GewähltFach") {
			//setze panel zurück
			RemoveItemDisplay ();
			//Setze Fertigkeiten zurück
			//Setze Lernpunkte zurück
		} else if (lernHelper.lernPunkteResetWaffe == true && gameObject.name =="GewähltWaffen") {
			RemoveItemDisplay ();
		} else if (lernHelper.lernPunkteResetZauber == true && gameObject.name =="GewähltZauber") {
			RemoveItemDisplay ();
		}
	}

	void RemoveItemDisplay ()
	{
		InventoryItemDisplay[] displayItems = gameObject.GetComponentsInChildren<InventoryItemDisplay> ();
		foreach (var itemDisplay in displayItems) {
			itemDisplay.item.activated = false;
			itemDisplay.gameObject.SetActive (false);
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
