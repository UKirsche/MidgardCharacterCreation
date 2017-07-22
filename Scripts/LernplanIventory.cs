using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LernplanIventory : MonoBehaviour {

	//Verweis auf zu füllendes Panel
	public LernPlanInventoryDisplay inventoryDisplayPrefab;
	public Transform displayParent;

	private const string panelName = "FachPanel";


	// Use this for initialization
	void Start () {

		//Fülle den Mist auf
		FillPanelFachkenntnisse();

	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	private void FillPanelFachkenntnisse()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listItems = lernHelper.GetFachkenntnisseItems();

		//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
		inventoryDisplayPrefab = (LernPlanInventoryDisplay)Instantiate (inventoryDisplayPrefab);
		inventoryDisplayPrefab.name = panelName;
		inventoryDisplayPrefab.transform.SetParent (displayParent, false);
		inventoryDisplayPrefab.FillItemDisplay (listItems);

		//Bei Zwergen muss Baukunde direkt geklickt werden
		MidgardCharakter mCharacter = globalVars.mCharacter;

		if (mCharacter.Spezies == Races.Zwerg) {
			InventoryItemDisplay[] arrayItemDisplayFach = inventoryDisplayPrefab.GetComponentsInChildren<InventoryItemDisplay> ();
			foreach (var itemDisplayFach in arrayItemDisplayFach) {
				if (itemDisplayFach.nameItem.text == "Baukunde") {
					itemDisplayFach.Click ();
				}
			}
		}
	}
}
