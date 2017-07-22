using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UngelernteFertigkeitenInvetory : MonoBehaviour {

	//Verweis auf zu füllendes Panel
	public LernPlanInventoryDisplay inventoryDisplayPrefab;
	public Transform displayParent;

	private const string panelName = "UngelFertigkeiten";


	// Use this for initialization
	void Start () {

		//Fülle den Mist auf
		FillPanelUngelernteFertigkeiten();

	}

	/// <summary>
	/// Fills the panel fachkenntnisse. 
	/// </summary>
	private void FillPanelUngelernteFertigkeiten()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Prepare listItems 
		List<InventoryItem> listItems = lernHelper.GetUngelernteFertigkeiten();


		//Fachkenntnisse werden entsprechend der Lernpunkte aufgelistet
		inventoryDisplayPrefab = (LernPlanInventoryDisplay)Instantiate (inventoryDisplayPrefab);
		inventoryDisplayPrefab.name = panelName;
		inventoryDisplayPrefab.transform.SetParent (displayParent, false);
		inventoryDisplayPrefab.FillItemDisplay (listItems);
	}
}
