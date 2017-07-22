using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class LernPlanFachWaffen {

	private bool leOk, neOk;
	private MidgardCharacterHelper mCharacterHelper;
	public  List<InventoryItem> listItems { get; set;}
	public bool IsMalusLeiteigenschaft { get; set;}
	public bool IsBonusLeiteigenschaft { get; set;}


	/// <summary>
	/// Initializes a new instance of the <see cref="LernPlanFachWaffen"/> class.
	/// </summary>
	public LernPlanFachWaffen(){
		mCharacterHelper = Toolbox.Instance.mCharacterHelper;
	}

	/// <summary>
	/// Gets the relevant fertigkeit.
	/// </summary>
	/// <returns>The relevant fertigkeit.</returns>
	/// <param name="charakterFertigkeit">Charakter fertigkeit.</param>
	/// <param name="fertigkeiten">Fertigkeiten.</param>
	/// <param name="leBonus">If set to <c>true</c> le bonus.</param>
	/// <param name="leMalus">If set to <c>true</c> le malus.</param>
	/// <typeparam name="U">The 1st type parameter.</typeparam>
	/// <typeparam name="T">The 2nd type parameter.</typeparam>
	public List<InventoryItem> GetRelevantFertigkeit<U,T>(List<U> charakterFertigkeit, List<T> fertigkeiten) where U:ILernplan where T: IFertigkeit{
		listItems = new List<InventoryItem> ();
		foreach (var fertigkeitRef in charakterFertigkeit) {
			
			modifyForTrinken (fertigkeitRef); //1 Filter....
			IFertigkeit fertObject = GetIFertigkeitFromRef (fertigkeitRef, fertigkeiten);

			List<LENEValue> listRestriktionenLE = GetRestriktionen (fertObject.Leiteigenschaft);
			List<LENEValue> listRestriktionenNE = GetRestriktionen (fertObject.Nebeneigenschaft);

			leOk = CheckRelevanceFertigkeit (listRestriktionenLE); //Hier eventull noch kleiner Bug, weil nicht ganze List durchlaufen
			neOk = CheckRelevanceFertigkeit (listRestriktionenNE);

			//Fachkenntnis kann zur Liste gefügt werden, falls Bedingungen erfüllt sind
			AddToInventoryList(fertigkeitRef, fertObject, listRestriktionenLE);

		}
		return listItems;
	}


	/// <summary>
	/// Adds to inventory list.
	/// </summary>
	/// <param name="fertigkeitRef">Fertigkeit reference.</param>
	/// <param name="fertObject">Fert object.</param>
	/// <param name="listRestriktionenLE">List restriktionen L.</param>
	/// <typeparam name="U">The 1st type parameter.</typeparam>
	private void AddToInventoryList<U>(U fertigkeitRef, IFertigkeit fertObject, List<LENEValue> listRestriktionenLE) where U:ILernplan{

		if (IsLeitAndNebenEigenschaftOk()) {
			InventoryItem item = CreateInventoryItemFromObjectAndRef (fertigkeitRef, fertObject);
			if (IsLeitEigenschaftBonus ()) {
				AddLEBonus (item, listRestriktionenLE);
			}

			listItems.Add (item);
		} 
		if (IsUngelernteFertigkeit()) { //nur bei ungelernte Fertigkeiten
			InventoryItem item = CreateInventoryItemFromObjectAndRef (fertigkeitRef, fertObject);
			SubLEBonus (item, listRestriktionenLE);
			//Füge der Liste hinzu
			listItems.Add (item);
		}
	}

	/// <summary>
	/// Creates the inventory item from object and reference.
	/// </summary>
	/// <returns>The inventory item from object and reference.</returns>
	/// <param name="fertigkeitRef">Fertigkeit reference.</param>
	/// <param name="fertObject">Fert object.</param>
	/// <typeparam name="U">The 1st type parameter.</typeparam>
	private InventoryItem CreateInventoryItemFromObjectAndRef<U>(U fertigkeitRef, IFertigkeit fertObject) where U:ILernplan{
		var item = new InventoryItem(fertigkeitRef.id, fertObject.name, fertigkeitRef.val, fertigkeitRef.cost, fertObject.typ);
		item.SetDependency (fertObject.Dependency); //Für Waffen und Fachkenntnisse kann es Abhängigkeiten geben
		return item;
	}

	/// <summary>
	/// Modifies for trinken.
	/// </summary>
	/// <param name="charakterFertigkeitRef">Charakter fertigkeit reference.</param>
	/// <param name="leBonus">If set to <c>true</c> le bonus.</param>
	/// <param name="leMalus">If set to <c>true</c> le malus.</param>
	/// <typeparam name="U">The 1st type parameter.</typeparam>
	private void modifyForTrinken<U>(U charakterFertigkeitRef) where U:ILernplan{
		//Achtung: falls Fertigkeit Trinken:
		if (charakterFertigkeitRef.id == 82 && (this.IsBonusLeiteigenschaft==true || this.IsMalusLeiteigenschaft==true)) {
			int valueTrinken;
			if (int.TryParse (charakterFertigkeitRef.val, out valueTrinken) == false) { //in diesem Fall über Konstitution berechnen:
				int mKo = mCharacterHelper.mCharacter.Ko;
				valueTrinken = Mathf.RoundToInt (mKo / 10) + 1;
				charakterFertigkeitRef.val = valueTrinken.ToString ();
			}
		}

	}

	/// <summary>
	/// Gets the I fertigkeit from reference.
	/// </summary>
	/// <returns>The I fertigkeit from reference.</returns>
	/// <param name="charakterFertigkeit">Charakter fertigkeit.</param>
	/// <param name="fertigkeiten">Fertigkeiten.</param>
	/// <typeparam name="U">The 1st type parameter.</typeparam>
	/// <typeparam name="T">The 2nd type parameter.</typeparam>
	private IFertigkeit GetIFertigkeitFromRef<U,T>(U charakterFertigkeit, List<T> fertigkeiten) where U:ILernplan where T: IFertigkeit{

		int fertId = charakterFertigkeit.id;
		IFertigkeit fertObject = ObjectXMLHelper.GetMidgardObjectById<T>(fertigkeiten , fertId);
		return fertObject;

	}

	/// <summary>
	/// Determines whether this instance is leit eigenschaft bonus.
	/// </summary>
	/// <returns><c>true</c> if this instance is leit eigenschaft bonus; otherwise, <c>false</c>.</returns>
	private bool IsLeitEigenschaftBonus(){
		return (this.IsBonusLeiteigenschaft == true);
	}

	/// <summary>
	/// Determines whether this instance is ungelernte fertigkeit.
	/// </summary>
	/// <returns><c>true</c> if this instance is ungelernte fertigkeit; otherwise, <c>false</c>.</returns>
	private bool IsUngelernteFertigkeit(){
		return (this.IsMalusLeiteigenschaft == true);
	}


	/// <summary>
	/// Determines whether this instance is leit and neben eigenschaft ok.
	/// </summary>
	/// <returns><c>true</c> if this instance is leit and neben eigenschaft ok; otherwise, <c>false</c>.</returns>
	private bool IsLeitAndNebenEigenschaftOk(){
		return (leOk == true && neOk == true && this.IsMalusLeiteigenschaft == false);	
	}


	/// <summary>
	/// Durchläuft alle Restriktionen der Leiteigenschaften. Fall Restriktionen vorhanden, extrahiere dieses und gebe sie mit Vorzeichen sowie Wert zurück
	/// </summary>
	/// <returns>The LE bonus.</returns>
	/// <param name="restrictionEigenschaften">Restriction eigenschaften.</param>
	private List<LENEValue> GetRestriktionen(string restrictionEigenschaften)
	{
		List<LENEValue> retVal = new List<LENEValue>();
		//Falls keine Restriktion vorliegt, ist die Eigenschaft relevant!
		if (restrictionEigenschaften == null) {
			return null;
		}
		string[]LEs = restrictionEigenschaften.Split(';');

		foreach (var item in LEs) {
			string[] NameByVals = item.Split (':');
			if (NameByVals.Length == 1) { //Hier nur eine Angabe enthalten
				return null;
			}

			bool valuePositiv = true; //Untergrenze für Wert bei positiv, Obergerenze für Wert bei negativ
			string name = NameByVals [0];
			string valueString = NameByVals [1];
			if (valueString.Contains ("-")) {
				valuePositiv = false;
				valueString = valueString.Substring (0, valueString.Length - 2); //nehme vorzeichen raus
			}
			int val = Convert.ToInt32 (valueString);

			LENEValue addToListVal;
			addToListVal.VZPositiv = valuePositiv;
			addToListVal.val = val;
			addToListVal.name = name;

			retVal.Add (addToListVal);
		}
		return retVal;
	}


	/// <summary>
	/// Adds the LE bonus. Achtung: hier nur eine Restriktion betrachtet, vermutlich aber korrekt.
	/// Fügt dem Charakter +1 (bei 80-95), +2 (ab 96 in Leiteigenschaft) hinzu
	/// </summary>
	/// <param name="item">Item.</param>
	/// <param name="leRestriktionen">Le restriktionen.</param>
	private void AddLEBonus(InventoryItem item, List<LENEValue> leRestriktionen){
		if (leRestriktionen.Count > 0) {
			//Hole Reflektionen der Charaktereigenschaften
			List<FieldInfo> eigenschaftenCharakterReflect =  mCharacterHelper.GetCharacterFieldValues();
			foreach (FieldInfo eigenschaftReflect in eigenschaftenCharakterReflect) {
				int? characterValue = eigenschaftReflect.GetValue (mCharacterHelper.mCharacter) as int?;
				int itemValue = Convert.ToInt32 (item.val);
				//Teste auf Bonus
				if (eigenschaftReflect.Name == leRestriktionen[0].name && characterValue >=80 && characterValue <=95) {
					itemValue += 1;
					item.val = itemValue.ToString ();
					return;
				} else if (eigenschaftReflect.Name == leRestriktionen[0].name && characterValue >=96 && characterValue <=100) {
					itemValue +=2;
					item.val = itemValue.ToString();
					return;
				}

			}
		}
	}

	/// <summary>
	/// Subtracts the LE bonus. Achtung: hier nur eine Restriktion betrachtet, vermutlich aber korrekt.
	/// Subtrahiert vom ungelernten Fertigkeitswert -2 falls CharakterWert niederer als LE-Wert
	/// </summary>
	/// <param name="item">Item.</param>
	/// <param name="leRestriktionen">Le restriktionen.</param>
	private void SubLEBonus(InventoryItem item, List<LENEValue> leRestriktionen){
		if (leRestriktionen.Count > 0) {
			//Hole Reflektionen der Charaktereigenschaften
			List<FieldInfo> eigenschaftenCharakterReflect =  mCharacterHelper.GetCharacterFieldValues();
			foreach (FieldInfo eigenschaftReflect in eigenschaftenCharakterReflect) {
				int? characterValue = eigenschaftReflect.GetValue (mCharacterHelper.mCharacter) as int?;
				int itemValue = Convert.ToInt32 (item.val);
				//Teste auf Bonus
				if (eigenschaftReflect.Name == leRestriktionen[0].name && characterValue < leRestriktionen[0].val) {
					itemValue -= 2;
					item.val = itemValue.ToString ();
					return;
				}

			}
		}
	}


	/// <summary>
	/// Checks the relevance fachkenntnis: Falls der Charakter einen höheren Wert besitzt, als die Restriktionen, wird true zurück gegeben
	/// Bemerkung: Zu prüfenden Charakterwerte können auch die Sinne sein, welche als Liste übergeben werden. Dann müssen alle Sinne des Charakters mit Restriktion geprüft werden.
	/// <returns><c>true</c>, if relevance fachkenntnis was checked, <c>false</c> otherwise.</returns>
	/// <param name="fkObject">Fk object.</param>
	private bool CheckRelevanceFertigkeit(List<LENEValue> restrictionEigenschaften)
	{
		//Hole Reflektionen der Charaktereigenschaften
		List<FieldInfo> eigenschaftenCharakterReflect =  mCharacterHelper.GetCharacterFieldValues();

		//Falls keine Restriktion vorliegt, ist die Eigenschaft relevant!
		if (restrictionEigenschaften == null || restrictionEigenschaften.Count==0) {
			return true;
		}

		foreach (var item in restrictionEigenschaften) {
			foreach (FieldInfo eigenschaftReflect in eigenschaftenCharakterReflect) {
				int? characterValue = eigenschaftReflect.GetValue (mCharacterHelper.mCharacter) as int?;
				if (characterValue != null) {
					//Fertigkeit zurückgeben
					LENEValue newObject = new LENEValue ();
					newObject.name = eigenschaftReflect.Name;
					newObject.val = characterValue.Value;
					if (ReflectValue (item, newObject) == true) {
						return true;
					}
				} else { //könnte liste sein
					if (typeof(IList).IsAssignableFrom (eigenschaftReflect.FieldType)) {
						var listValues = eigenschaftReflect.GetValue (mCharacterHelper.mCharacter);
						// By now, we know that this is assignable from IList, so we can safely cast it.
						foreach (var element in listValues as IList) {
							Type t = element.GetType ();
							if (t.Equals (typeof(Sinn))) {
								Sinn sElement = element as Sinn;
								LENEValue newElement = new LENEValue ();
								newElement.name = sElement.name;
								newElement.val = sElement.value;
								if (ReflectValue (item, newElement) == true) {
									return true;
								}

							}

						}
					}
				}
			}
		}
		//Falls nichts gefunden, Test fehlgeschlagen
		return false;
	}


	/// <summary>
	/// Reflects the value.
	/// Prüft, der Charakterwert die Restriktion übertrifft
	/// </summary>
	/// <returns><c>true</c>, if value was reflected, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	/// <param name="eigenschaftReflect">Eigenschaft reflect.</param>
	private bool ReflectValue (LENEValue item, LENEValue eigenschaftReflect)
	{
		if (item.VZPositiv == true && eigenschaftReflect.name == item.name && eigenschaftReflect.val >= item.val) {
			return true;
		}
		else{
			if (item.VZPositiv == false && eigenschaftReflect.name == item.name && eigenschaftReflect.val <= item.val) {
				return true;
			}
		}

		return false;
	}
}
