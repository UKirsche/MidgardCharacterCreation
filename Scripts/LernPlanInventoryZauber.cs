using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LernPlanInventoryZauber : MonoBehaviour {

	//Verweis auf zu füllendes Panel
	public LernPlanInventoryDisplay inventoryDisplayPrefab;
	public Transform displayParent;

	private const string panelName = "ZauberPanel";


	// Use this for initialization
	void Start () {

		//Fülle den Mist auf
		FillPanelZauber();

	}

	/// <summary>
	/// Fills the panel with Zauber. 
	/// </summary>
	private void FillPanelZauber()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listZauberformeln = lernHelper.GetZauberFormeln();
		List<InventoryItem> listZaubersalze = lernHelper.GetZauberSalze();
		List<InventoryItem> listZauberlieder = lernHelper.GetZauberLieder();


		//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
		inventoryDisplayPrefab = (LernPlanInventoryDisplay)Instantiate (inventoryDisplayPrefab);
		inventoryDisplayPrefab.name = panelName;
		inventoryDisplayPrefab.transform.SetParent (displayParent, false);
		inventoryDisplayPrefab.FillItemDisplay (listZauberformeln);
		inventoryDisplayPrefab.FillItemDisplay (listZauberlieder);
		inventoryDisplayPrefab.FillItemDisplay (listZaubersalze);
	}
		
}

