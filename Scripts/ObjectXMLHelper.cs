using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectXMLHelper 
{
	/// <summary>
	/// Gets the index of the midgardobjects for chosen optiontext
	/// </summary>
	/// <returns>The chosen option index.</returns>
	/// <param name="OptionText">Option text.</param>
	/// <param name="baseList">Base list.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static int GetChosenOptionIndex<T> (string OptionText, List<T> baseList)
		where T: IName, IID
	{
		int aTypID=0;
		foreach (T item in baseList) {
			if (item.name == OptionText) {
				aTypID = item.id;
				break;
			}
		}
		return aTypID;
	}


	/// <summary>
	/// Sammelt die Abenteuertypen abhängig von der Rasse
	/// </summary>
	/// <param name="speziesInt">Spezies int.</param>
	public static List<T> GetMidgardObjectAByIndexB<T,U> (List<T> allMidgardObjects, int speziesInt) where T:IRestriktion where U:IID
	{
		List<T> listTypenSel = new List<T> ();
		foreach (T item in allMidgardObjects) {
			List<U> rassenRefs = item.restriktionen.rasseIDs as List<U>;
			foreach (U rasse in rassenRefs) {
				if (rasse.id == speziesInt) {
					listTypenSel.Add (item);
				}
			}
		}

		return listTypenSel;
	}


	/// <summary>
	/// Gets the midgard object by identifier.
	/// </summary>
	/// <returns>The midgard object by identifier.</returns>
	/// <param name="allObjects">All objects.</param>
	/// <param name="id">Identifier.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T GetMidgardObjectById<T> (List<T> allObjects, int id) where T:IID{

		T returnObject= default(T);

		foreach (T item in allObjects) {
			if (item.id == id) {
				returnObject = item;
				break;
			}
		}

		return returnObject;

	}



	/// <summary>
	/// Fülle die Dropbox mit den relevanten MidgardObjekte
	/// </summary>
	/// <param name="listTypenSel">List typen sel.</param>
	public static void FillDropBoxMidgardObject<T>(List<T> listTypenSel, Dropdown DropDMidgard) where T: IName
	{
		DropDMidgard.ClearOptions ();
		List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData> ();
		//füge neu hinzu
		foreach (T newItem in listTypenSel) {
			Dropdown.OptionData newOption = new Dropdown.OptionData ();
			newOption.text = newItem.name;
			optionList.Add (newOption);
		}
		DropDMidgard.AddOptions (optionList);
	}

}
