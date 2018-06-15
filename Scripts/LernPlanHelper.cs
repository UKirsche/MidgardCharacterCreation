using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

struct LENEValue{
	public bool VZPositiv;
	public int val;
	public string name;
}

public class LernPlanHelper{

	//Hilfskonstruktion für Klasse


	private Toolbox globalVars;
	private Lernplan lernPlan;
	public MidgardCharacterHelper mCharacterHelper { get; set;}
	private LernPlanFachWaffen lernPlanFachWaffen;
	private LernplanModify lernPlanModifier;

	public InventoryItemDisplay itemDisplayPrefab;

	//Zustand, ob Lernpunkte neu gewürfelt wurden
	public bool lernPunkteResetFach { get; set;}
	public bool lernPunkteResetWaffe { get; set;}
	public bool lernPunkteResetZauber { get; set;}
	public bool lernPunkteResetAllgemein { get; set;}
	public bool lernPunkteResetUngewFertigkeiten { get; set;}

	// für das display die entsprechenden lernpunkte 1
	public int LernPunkteFach { get; set;}
	public int LernPunkteWaffen { get; set;}
	public int LernPunkteZauber { get; set;}

	//Lernpunkte Display: AllgemeinWissen
	public int LernPunkteAllgemeinWissen {get; set;}

	//Lernpunkte Display:  Ungewöhnliche Fertigkeiten 
	public int LernPunkteUngewFertigkeiten {get;set;}

	//Lernpunkte Display: Ungewöhnliche Fertigkeiten
	private int lernPunktePool;
	public int LernPunktePool { 
		get{ return lernPunktePool; }
		set{
			lernPunktePool = value;
			if (value < 0) {
				lernPunktePool = 0;		
			}
		}
	}

	//Display Berufswahl
	public int BerufswahlW100{get; set;}

	//Referenz auf Berufe
	public Dictionary<string, List<Beruf>> Berufskenntnisse {get;set;}

	//Anzahl gewählter Berufe in Panel drei
	public int FachBerufClicked {get;set;}
	public int KategorieClicked { get; set;}


	/// <summary>
	/// Initializes a new instance of the <see cref="LernPlanHelper"/> class. 
	/// </summary>
	/// <param name="_characterHelper">Character helper.</param>
	public LernPlanHelper(MidgardCharacterHelper _characterHelper){

		configureClassMembers (_characterHelper);
		lernPlanModifier.ModifyFachKenntnisseForRace(); // Rassen haben unterschiedliche Fachkenntnisse
		lernPlanModifier.ModifyWaffenForRace(); //Rassen haben unterschiedliche Waffe
	}


	/// <summary>
	/// Configures the class members with instantions
	/// </summary>
	/// <param name="_characterHelper">Character helper.</param>
	void configureClassMembers (MidgardCharacterHelper _characterHelper)
	{
		mCharacterHelper = _characterHelper;
		lernPlanFachWaffen = new LernPlanFachWaffen ();
		globalVars = Toolbox.Instance;
		lernPlan = mCharacterHelper.GetLernplanForCharacter ();
		lernPlanModifier = new LernplanModify ();
	}


	#region Fachkenntnisse
	/// <summary>
	/// Gets the fachkenntnisse items.  Methode wird aus den LernplanIntentory Displays heraus gerufen
	/// Bemerkung: Haben Charakter in den Leiteigenschaften Werte zw. 81-95 erhöht sich die Fähigkeit im +1, bei 96-100 um +2
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetFachkenntnisseItems(){
		//Stammdaten- Fachkenntnisse
		Fertigkeiten midgardFertigkeiten = globalVars.MidgardFertigkeiten;
		lernPlanFachWaffen.IsBonusLeiteigenschaft = true;
		lernPlanFachWaffen.IsMalusLeiteigenschaft = false;
		List<InventoryItem> returnList = lernPlanFachWaffen.GetRelevantFertigkeit<FachkenntnisRef, Fachkenntnis> (lernPlan.fachkenntnisse, midgardFertigkeiten.fachKenntnisse);
		SetItemType (returnList, "Fach");
		return returnList;
	}


	/// Gets the fachkenntnisse items für einen Beruf. 
	/// Der Beruf enthält jeweils die passenden Fachkenntnisse, die nur umkonvertierte werden müssen
	/// Bemerkung: Haben Charakter in den Leiteigenschaften Werte zw. 81-95 erhöht sich die Fähigkeit im +1, bei 96-100 um +2
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetFachkenntnisseItemsForBeruf(List<Beruf> fachKenntnisseBeruf){
		Fertigkeiten midgardFertigkeiten = globalVars.MidgardFertigkeiten;

		//Extrahiere Fachkenntnisse aus den Berufen:
		var listFachKenntnisse = CreateFachkenntnisseBeruf (fachKenntnisseBeruf);
		lernPlanFachWaffen.IsBonusLeiteigenschaft = true;
		lernPlanFachWaffen.IsMalusLeiteigenschaft = false;
		List<InventoryItem> returnList = lernPlanFachWaffen.GetRelevantFertigkeit<FachkenntnisRef, Fachkenntnis> (listFachKenntnisse, midgardFertigkeiten.fachKenntnisse);
		SetItemType (returnList, "Fach");
		return returnList;
	}
		
	#endregion


	#region waffen
	/// <summary>
	/// Gets the Waffenfertigkeit items.
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetWaffenfertigkeitItems(){
		//Stammdaten- Fachkenntnisse
		Waffenfertigkeiten midgardWaffenFertigkeiten = globalVars.MidgardWaffenFertigkeiten;
		lernPlanFachWaffen.IsBonusLeiteigenschaft = false;
		lernPlanFachWaffen.IsMalusLeiteigenschaft = false;
		List<InventoryItem> returnList = lernPlanFachWaffen.GetRelevantFertigkeit<WaffenfertigkeitRef, Waffenfertigkeit> (lernPlan.waffenfertigkeiten, midgardWaffenFertigkeiten.waffenFertigkeiten);
		return returnList;
	}
	#endregion


	#region zauber

	/// <summary>
	/// Gets the Zauber items.
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetZauberFormeln(){
		//Stammdaten- Fachkenntnisse
		Zauberformeln midgardZauberformeln = globalVars.MidgardZauberformeln;
		List<InventoryItem> returnList = GetRelevantZauber<ZauberformelRef, Zauberformel> (lernPlan.zauberformeln, midgardZauberformeln.zauberformeln);
		SetItemType (returnList, "Zauberformel");
		return returnList;
	}


	/// <summary>
	/// Gets the Zauberlieder for bards.
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetZauberLieder(){
		//Stammdaten- Fachkenntnisse
		Zauberlieder midgardZauberlieder = globalVars.MidgardZauberlieder;
		List<InventoryItem> returnList = GetRelevantZauber<ZauberliedRef, Zauberlied> (lernPlan.zauberlieder, midgardZauberlieder.zauberlieder);
		SetItemType (returnList, "Zauberlied");
		return returnList;
	}

	/// <summary>
	/// Gets the Zaubersalze for thaumaturgs.
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetZauberSalze(){
		//Stammdaten- Fachkenntnisse
		Zaubersalze midgardZaubersalze = globalVars.MidgardZaubersalze;
		List<InventoryItem> returnList = GetRelevantZauber<ZaubersalzRef, Zaubersalz> (lernPlan.zaubersalze, midgardZaubersalze.zaubersalze);
		SetItemType (returnList, "Zaubersalz");
		return returnList;
	}


	/// <summary>
	/// Gets the relevant fachkenntnisse.
	/// </summary>
	/// <param name="charakterFachkenntnisse">Charakter fachkenntnisse.</param>
	private List<InventoryItem> GetRelevantZauber<U,T>(List<U> charakterZauber, List<T> allZauber) where U:IDisplay where T: IZauber{
		List<InventoryItem> listItems = new List<InventoryItem> ();
		//Fachkenntnisse für Lernplan
		foreach (var zauberRef in charakterZauber) {
			int zauberID = zauberRef.id;
			IZauber fertObject = ObjectXMLHelper.GetMidgardObjectById<T>(allZauber , zauberID);
			InventoryItem item =new InventoryItem ();
			item.name = fertObject.name;
			item.val = "-";
			item.cost = zauberRef.cost;
			listItems.Add (item);
		}
		return listItems;
	}

	#endregion


	#region Allgemeinwissen
	public List<InventoryItem> GetAllgemeinWissen(){

		AllgemeinWissenLoader allgWissenLoader = new AllgemeinWissenLoader ();
		List<InventoryItem> listFachAllgemein = allgWissenLoader.GetAllgemeinFachkenntnisse ();
		List<InventoryItem> listWaffenAllgemein = allgWissenLoader.GetAllgemeinWaffen ();
		FilterOutFertigkeiten (listWaffenAllgemein, false);

		List<InventoryItem> retVal = listFachAllgemein;
		retVal.AddRange (listWaffenAllgemein);

		return retVal;

	}

	#endregion

	#region Ungewöhnliche Fertigkeiten

	/// <summary>
	/// Gets the ungewöhnlichen items. 
	/// Bemerkung: Haben Charakter in den Leiteigenschaften Werte zw. 81-95 erhöht sich die Fähigkeit im +1, bei 96-100 um +2
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetUngewFertigkeiten(){
		UngewFertigkeitenLoader ungewLoader = new UngewFertigkeitenLoader ();
		List<InventoryItem> listUngewFertigkeiten = ungewLoader.GetUngewoehnlicheFertigkeiten ();
		return listUngewFertigkeiten;
	}



	/// <summary>
	/// Filters the out fertigkeiten. Entfernt schon vorhandene Charakterfertigkeit aus Liste der Fertigkeiten
	/// </summary>
	/// <param name="filterFertigkeiten">Filter fertigkeiten.</param>
	/// <param name="fach">If set to <c>true</c> fach.</param>
	private void FilterOutFertigkeiten(List<InventoryItem> filterFertigkeiten, bool fach){

		MidgardCharacterHelper mCHelper = globalVars.mCharacterHelper;
		foreach (var item in filterFertigkeiten.ToArray()) {
			if (fach == true) {
				if (mCHelper.GetCharacterFachkenntnis (item.name) != null) {
					filterFertigkeiten.Remove (item);
				}
			} else {
				if (mCHelper.GetCharacterWaffe (item.name) != null) {
					filterFertigkeiten.Remove (item);
				}
			}
		}
	}
	#endregion



	#region Berufe
	/// <summary>
	/// Gets the berufe.
	/// 	Achtung: Rassen und Berufe, die nicht kompatibel sind:
	/// 	Elfen:
	/// 		{Bergmann, Krämer, Schmuggler, Sklavenhändler, Winzer, Wirt}
	/// 	Gnome:
	/// 		{Bettler, Flussschiffer, Grobschmied, Kapitän, Karawanenführer, Kurtisane, Räuber, Scharfrichter, Seemannn, Schlachter, Sklavenhändler, Steuermann, Träger, Winzer, Zureiter}
	/// 	Halbling:
	/// 		{Bergmann, Bootsbauer, Falkner, Flussschiffer, Kapitän, Karawanenführer, Räuber, Rechtsgelehrter, Sattler, Scharfrichter, Seemannn, Sklavenhändler, Stadtwache, Steuermann, Waffenschmied, Waldarbeiter, Zureiter }
	///		Zwerge:
	/// 		{Akrobat, Barbier, Bettler, Bootsbauer, Dieb, Falkner, Fallensteller, Fischer, Flussschiffer, Jäger, Kapitän, Kurtisane, Sattler, Seemannn, Sklavenhändler, Steuermann, Waldarbeiter, Zureiter } 
	/// </summary>
	public List<InventoryItem> GetBerufe(){

		Berufe berufeAlle = globalVars.MidgardeBerufe;
		MidgardCharakter mCharacter = mCharacterHelper.mCharacter;
		LernPlanHelper lpHelper = globalVars.lernHelper;
		List<Beruf> berufe;
		Dictionary<string, List<Beruf>> berufeDict = new Dictionary<string, List<Beruf>> ();
		List<InventoryItem> returnList = new List<InventoryItem> ();

		AbenteuerTyp[] kämpfer = { AbenteuerTyp.As, AbenteuerTyp.Kr, AbenteuerTyp.Or, AbenteuerTyp.Soe };
		AbenteuerTyp[] zauberer = {
			AbenteuerTyp.Be,
			AbenteuerTyp.Dr,
			AbenteuerTyp.Ma,
			AbenteuerTyp.Hx,
			AbenteuerTyp.PF,
			AbenteuerTyp.PHa,
			AbenteuerTyp.PHe,
			AbenteuerTyp.PK,
			AbenteuerTyp.PM,
			AbenteuerTyp.PT,
			AbenteuerTyp.PW,
			AbenteuerTyp.Th
		};

		string[] ElfenNichtBerufe = { "Bergmann", "Krämer", "Schmuggler", "Sklavenhändler", "Winzer", "Wirt" };
		string[] GnomeNichtBerufe = {
			"Bettler",
			"Flussschiffer",
			"Grobschmied",
			"Kapitän",
			"Karawanenführer",
			"Kurtisane",
			"Räuber",
			"Scharfrichter",
			"Seemannn",
			"Schlachter",
			"Sklavenhändler",
			"Steuermann",
			"Träger",
			"Winzer",
			"Zureiter"
		};
		string[] HalblingNichtBerufe = {
			"Bergmann",
			"Bootsbauer",
			"Falkner",
			"Flussschiffer",
			"Kapitän",
			"Karawanenführer",
			"Räuber",
			"Rechtsgelehrter",
			"Sattler",
			"Scharfrichter",
			"Seemannn",
			"Sklavenhändler",
			"Stadtwache",
			"Steuermann",
			"Waffenschmied",
			"Waldarbeiter",
			"Zureiter"
		};

		string[] ZwergeNichtBerufe = {
			"Akrobat",
			"Barbier",
			"Bettler",
			"Bootsbauer",
			"Dieb",
			"Falkner",
			"Fallensteller",
			"Fischer",
			"Flussschiffer",
			"Jäger",
			"Kapitän",
			"Kurtisane",
			"Sattler",
			"Seemannn",
			"Sklavenhändler",
			"Steuermann",
			"Waldarbeiter",
			"Zureiter"
		};

		//Berufe müssen entsprechend und Herkunft gefiltert werden
		if (mCharacter.StadtLand == StadtLandFluss.Stadt) {
			berufe = berufeAlle.stadtBerufe.berufe;
		} else {
			berufe = berufeAlle.landBerufe.berufe;
		}

		//Nun müssen die Berufe gefiltert werden:
		foreach (Beruf beruf in berufe.ToArray()) {
			if (beruf.schicht.Length>0) { //Schicht
				string sCharacter = mCharacter.Schicht.ToString () [0].ToString(); //Hole ersten Buchstaben
				if(!beruf.schicht.Contains(sCharacter)){ //Werfe Beruf, falls nicht gleiche schicht
					berufe.Remove (beruf);
					continue;
				}
			}
			if(kämpfer.Contains(mCharacter.Archetyp)){
				if (!beruf.typus.Contains("k")) {
					berufe.Remove (beruf);
					continue;
				}
			} else if(zauberer.Contains(mCharacter.Archetyp)){
				if (!beruf.typus.Contains("z")) {
					berufe.Remove (beruf);
					continue;
				}
			}

			//Füge Beruf dem Dictionary hinzu
			if (!berufeDict.ContainsKey (beruf.name)) {
				List<Beruf> newList = new List<Beruf> ();
				newList.Add (beruf);
				berufeDict.Add (beruf.name, newList);
			} else {
				List<Beruf> oldList;
				berufeDict.TryGetValue (beruf.name, out oldList);
				oldList.Add (beruf);
			}

		}

		//Berufe müssen für Rassen gefiltert werden bzgl. Rassen
		List<string> keysToRemove= new List<string>();
		if (mCharacter.Spezies == Races.Elf) {
			keysToRemove = ElfenNichtBerufe.OfType<string> ().ToList ();
		} else if (mCharacter.Spezies == Races.Halbling) {
			keysToRemove = HalblingNichtBerufe.OfType<string> ().ToList ();
		} else if (mCharacter.Spezies == Races.Zwerg) {
			keysToRemove = ZwergeNichtBerufe.OfType<string> ().ToList ();
		} else if (mCharacter.Spezies == Races.Berggnom || mCharacter.Spezies == Races.Waldgnom) {
			keysToRemove = GnomeNichtBerufe.OfType<string> ().ToList ();
		}
		foreach (var item in keysToRemove) {
			berufeDict.Remove (item);
		}

		//Erstelle Iventory Items nur, falls genügend Fachkenntnisse im Beruf
		foreach (string key in berufeDict.Keys) {
			int wuerfelWurf = lpHelper.BerufswahlW100;
			List<Beruf> wahlFach;
			berufeDict.TryGetValue (key, out wahlFach);
			lernPlanModifier.ModifyBerufeFromWurf (wuerfelWurf, wahlFach);
			if (wahlFach.Count > 0) {
				returnList.Add(new InventoryItem(0, key,"",0,""));
			}
		}

		Berufskenntnisse = berufeDict;
		return returnList;
	}
		

	/// <summary>
	/// Restes the panel3 clicked.
	/// Setzt ausgewählte Fachberufe zurücke
	/// </summary>
	public void ResetPanelFachbBerufeClicked(){
		FachBerufClicked = 0;
		KategorieClicked = 0;
	}
	#endregion

	#region Ungelernte Fertigkeiten
	/// <summary>
	/// Gets the ungewöhnlichen items. 
	/// Bemerkung: Haben Charakter in den Leiteigenschaften Werte zw. 81-95 erhöht sich die Fähigkeit im +1, bei 96-100 um +2
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetUngelernteFertigkeiten(){
		Fertigkeiten midgardFertigkeiten = globalVars.MidgardFertigkeiten;
		UngelernteFertigkeiten ungelernteFertigkeiten = globalVars.MidgardUngelerenteFertigkeiten;
		List<FachkenntnisRef> fachkenntnisse = ungelernteFertigkeiten.fachkenntnisse;
		//List<WaffenfertigkeitRef> waffen = ungelernteFertigkeiten.waffen;
		lernPlanFachWaffen.IsBonusLeiteigenschaft = false;
		lernPlanFachWaffen.IsMalusLeiteigenschaft = true;

		//Verbinde die fachkenntnisse
		List<InventoryItem> returnListFach = lernPlanFachWaffen.GetRelevantFertigkeit<FachkenntnisRef, Fachkenntnis> (fachkenntnisse, midgardFertigkeiten.fachKenntnisse);

		//Filtern
		FilterOutFertigkeiten(returnListFach, true);

		return returnListFach;
	}

	#endregion

	#region Hilfsmethoden

	/// <summary>
	/// Sets the item type fache.
	/// </summary>
	/// <param name="returnList">Return list.</param>
	private void SetItemType (List<InventoryItem> returnList, string typeName)
	{
		foreach (var item in returnList) {
			item.type = typeName;
		}
	}


	/// <summary>
	/// Creates the fachkenntnisse für einen  Beruf.
	/// </summary>
	/// <returns>The fachkenntnisse beruf.</returns>
	/// <param name="fachKenntnisseBeruf">Fach kenntnisse beruf.</param>
	private List<FachkenntnisRef> CreateFachkenntnisseBeruf (List<Beruf> fachKenntnisseBeruf)
	{
		List<FachkenntnisRef> listFachKenntnisse = new List<FachkenntnisRef> ();
		foreach (var item in fachKenntnisseBeruf) {
			FachkenntnisRef fRef = new FachkenntnisRef ();
			fRef.id = item.id;
			fRef.val = item.val;
			fRef.cost = Convert.ToInt32 (item.kategorie);
			listFachKenntnisse.Add (fRef);
		}
		return listFachKenntnisse;
	}
	#endregion

}
