using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetSpezialWaffe : MonoBehaviour {


	public Dropdown dropSpezial;

	// Use this for initialization
	void Start () {
	
	}

	void OnEnable() {

		//Clear Dropbox
		dropSpezial.ClearOptions();


		List<InventoryItem> listSpezialWaffen = new List<InventoryItem> ();

		MidgardCharacterHelper mCHelper = Toolbox.Instance.mCharacterHelper;

		//Hole Nahkampfwaffen mit 1 Lernpunkt
		List<InventoryItem> nahkampfWaffen = mCHelper.GetNahkampfWaffen();
		List<InventoryItem> fernkampfWaffen = mCHelper.GetFernkampfWaffen();

		FilterSpezialWaffen ("Nah", nahkampfWaffen, listSpezialWaffen);
		FilterSpezialWaffen ("Fern", fernkampfWaffen, listSpezialWaffen);

		AddSpezialWaffenToDropBox (listSpezialWaffen);

		dropSpezial.RefreshShownValue ();

	}


	private void AddSpezialWaffenToDropBox(List<InventoryItem> listSpezialWaffen){

		foreach (var item in listSpezialWaffen) {
			dropSpezial.options.Add (new Dropdown.OptionData() {text = item.name});
		}
	}

	/// <summary>
	/// Filtert mögliche Spezialwaffen heraus.
	/// Bem: Achtung: Hier muss geprüft werden, was passieren soll, wenn keine Nahkampfwaffen für 1LP vorliegen, bzw. keine Fernkampfwaffen fr 2LP
	/// </summary>
	/// <returns>The spezial waffen.</returns>
	/// <param name="typ">Typ.</param>
	/// <param name="waffen">Waffen.</param>
	/// <param name="listSpezialWaffen">List spezial waffen.</param>
	private List<InventoryItem> FilterSpezialWaffen(string typ, List<InventoryItem> waffen, List<InventoryItem> listSpezialWaffen){
		if (typ == "Nah") {
			foreach (var item in waffen) {
				if (item.cost == 1) {
					listSpezialWaffen.Add (item);
				}
			}
		} else if (typ == "Fern") {
			foreach (var item in waffen) {
				if (item.cost == 2) {
					listSpezialWaffen.Add (item);
				}
			}
		}

		return listSpezialWaffen;
	}
		
}
