using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HandleLernItemClick : MonoBehaviour
{

	public InventoryItemDisplay itemDisplayPrefab;
	public InputField inputPointsLeft, inputPointsPool;
	private List<InventoryItem> listDependendItemDisplay;
	private List<InventoryItem> listIndependentItemDisplay;
	private Transform contextPanelDisplay; //This can be either left or right panel, depending where item was clicked
	private InventoryItemDisplay clickedItemDisplay;
	private MaskenType maskenType;

	//TODO: 
	// * Aktiviert: Falls eines dieser Items aktiviert wird: checke, ob das entsprechende Item schon vorhanden

	void Awake(){
		
	}

	// Use this for initialization
	void Start ()
	{
		
	}

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
	/// Handles the on item click. Controls whole process of adding and removing items.
	/// </summary>
	/// <param name="itemDisplay">Item display.</param>
	public void HandleOnItemClick (InventoryItemDisplay itemDisplay)
	{
		ConfigContextAndItem (itemDisplay);
		GetItemDependencies ();

		//Geklicktes Item schon aktiviert -> löschen, sonst auf rechts panel pushen
		if (itemDisplay.item.activated) {
			RemoveItem ();
		} else { 
			PushItem ();
		}
	}





	#region Move Item on right panel
	/// <summary>
	/// Adds item to chosen right panel. If needed, independed items must be loaded as well
	/// </summary>
	/// <param name="rightPanelDisplay">Right panel display.</param>
	private void PushItem (){
		AddIndependentItem ();
		bool added = AddItem (this.clickedItemDisplay.item);
		if (added) {
			maskenType.ResetLernPunkte (false); //heißt: zeige immer wieder dieselben items auf maske
		}
	}


	/// <summary>
	/// Adds the item: Sowohl zum Panel als auch zum Charakter!
	/// </summary>
	/// <returns><c>true</c>, if item was added, <c>false</c> otherwise.</returns>
	/// <param name="item">item.</param>
	private bool AddItem(InventoryItem item){
		int lernPunkteDelta = item.cost;
		int lernPunkteRest = maskenType.GetLernPunkteRest ();
		bool addMore = ((lernPunkteRest- lernPunkteDelta) >= 0) ? true : false;
		if (addMore) {
			InsertNewItem (item); 
			maskenType.AddFertigkeitToCharacter (item);
		}

		return addMore;
	}



	/// <summary>
	/// Adds the independent item. Fügt zu jeder Fertigkeit ein abhängiges Item hinzu falls es vorgesehen ist
	/// </summary>
	private void AddIndependentItem(){
		InventoryItem item = this.clickedItemDisplay.item;
		if(listDependendItemDisplay.Contains(item)){
			//Hole Unabhängiges Item
			int[] depIvs = item.dependency;
			List<InventoryItem> toAdd = listIndependentItemDisplay.Where (iVI => depIvs.Contains(iVI.id)).Select(i=>i).ToList();
			foreach (var iItem in toAdd) {
				if (!iItem.ReverseDependency.Contains (iItem.id)) {
					iItem.ReverseDependency.Add (item.id); //Füge Info hinzu, dass Item aktiviert wurde
				}
				if (iItem.activated == false) { //Item bisher noch nicht anderweitig aktiviert:
					AddItem (iItem);
				} 
			}
		}
	}

	/// <summary>
	/// Inserts the new item. Dazu werden übergeben: QuellItem, auf das geklickt wurde, QuellPanel, abzuziehende LPs sowie das Zielpanel
	/// </summary>
	/// <param name="item">Item.</param>
	void InsertNewItem (InventoryItem item)
	{
		item.activated = true;
		//erzeuge neues item
		CreateInventoryItem (item);
		SubstractLearningPoints (item);
	}

	/// <summary>
	/// Creates the inventory item aus dem Prefab, setzt parent und values ein
	/// </summary>
	/// <param name="item">Item .</param>
	void CreateInventoryItem (InventoryItem item)
	{
		Transform rightPanelDisplay = maskenType.GetRightPanel ();
		InventoryItemDisplay itemToDisplay = (InventoryItemDisplay)Instantiate (itemDisplayPrefab);
		itemToDisplay.transform.SetParent (rightPanelDisplay, false);
		itemToDisplay.SetDisplayValuesCost (item);
	}

	#endregion




	#region delete item from right panel
	/// <summary>
	/// Deactivate the chosen item if clicked on right panel. Remove dependent items if necessary
	/// </summary>
	private void RemoveItem ()
	{
		string contextItemDisplayName = contextPanelDisplay.name;
		string righPanelShort = "Gewählt";

		if (contextItemDisplayName.Contains (righPanelShort)) {
			if (this.clickedItemDisplay.item.ReverseDependency.Count > 0) {
				RemoveDependentItems (this.clickedItemDisplay.item.ReverseDependency);
			}
			DeActivateItem (this.clickedItemDisplay);
		}
	}


	/// <summary>
	/// Des the activate item.
	/// </summary>
	private void DeActivateItem(InventoryItemDisplay itemDisplay){
		itemDisplay.item.activated = false;
		itemDisplay.gameObject.SetActive (false);
		AddLearningPoints (itemDisplay.item);
		maskenType.DeleteFertigkeitFromCharacter (itemDisplay.item);
	}

	private void RemoveDependentItems(List<int> toRemoveItems){
		Transform rightPanelDisplay = maskenType.GetRightPanel ();
		InventoryItemDisplay[] rightInventoryItems = rightPanelDisplay.GetComponentsInChildren<InventoryItemDisplay> ();
		List<InventoryItemDisplay> toDeactivate = rightInventoryItems.Where (iVI => toRemoveItems.Contains(iVI.item.id)).Select(i=>i).ToList();
		foreach (var itemDisplay in toDeactivate) {
			DeActivateItem (itemDisplay);
		}
	}
	#endregion






	#region item dependency-structures
	/// <summary>
	/// Some items depend on other items, that must be reloaded as well. They are stored in separate lists
	/// </summary>
	void GetItemDependencies ()
	{
		GetDependentItems ();
		GetIndependentItems ();
	}

	/// <summary>
	/// Configs the context and item, i.e. an item can be clicked from the panel on the left and the panel on the right. Der are three different categories of left and right panels
	/// Fachkenntnisse, Waffen und Zauber. These contexts are retrieved and stored
	/// </summary>
	/// <param name="itemDisplay">Item display.</param>
	private void ConfigContextAndItem (InventoryItemDisplay itemDisplay)
	{
		this.clickedItemDisplay = itemDisplay;
		this.contextPanelDisplay = this.clickedItemDisplay.transform.parent;
 		maskenType = HandleMaskTypeFactory.GetMaskType (this.contextPanelDisplay.name);
	}

	/// <summary>
	/// Gets the dependent items.
	/// Items, die nur gewählt werden können, wenn andere Items schon ausgewählt sind
	/// </summary>
	private void GetDependentItems(){
		InventoryItemDisplay[] allInventoryItems = this.contextPanelDisplay.GetComponentsInChildren<InventoryItemDisplay> ();
		listDependendItemDisplay = allInventoryItems.Where (iVI => iVI.item.dependency != null).Select(i=>i.item).ToList();
	}


	/// <summary>
	/// Gets the independent items.
	/// </summary>
	private void GetIndependentItems(){
		var list = listDependendItemDisplay.Select (i => i.dependency).ToList();
		List<int> returnIntegers = new List<int> ();
		foreach (int[] item in list) {
			for (int i = 0; i < item.Length; i++) {
				if(!returnIntegers.Contains(item[i])){
					returnIntegers.Add (item [i]);
				}
			}
		}

		InventoryItemDisplay[] allInventoryItems = this.contextPanelDisplay.GetComponentsInChildren<InventoryItemDisplay> ();
		listIndependentItemDisplay = allInventoryItems.Where (iVI => returnIntegers.Contains(iVI.item.id)).Select(i=>i.item).ToList();

	}
	#endregion




	#region lernpunkte
	void SubstractLearningPoints (InventoryItem item)
	{
		int deltaPoints = item.cost;
		this.ModifyLernPunkteRest (-deltaPoints);
	}

	void AddLearningPoints (InventoryItem item)
	{

		int deltaPoints = item.cost;
		this.ModifyLernPunkteRest (deltaPoints);
	}

	private void ModifyLernPunkteRest (int deltaPoints)
	{
		LernPlanHelper lpHelper = Toolbox.Instance.lernHelper;
		int lernPunkteRest = maskenType.GetLernPunkteRest ();
		lernPunkteRest += deltaPoints;
		maskenType.SetLernPunkteRest (lernPunkteRest);
		lpHelper.LernPunktePool += deltaPoints;
		inputPointsLeft.text = maskenType.GetLernPunkteRest ().ToString();
		inputPointsPool.text = lpHelper.LernPunktePool.ToString ();
	}
	#endregion
		
}