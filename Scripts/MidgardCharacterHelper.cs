using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class MidgardCharacterHelper{

	private MidgardCharakter _mCharacter;
	public MidgardCharakter mCharacter{
		set{
			_mCharacter = value;
		}
		get{
			return _mCharacter;
		}
	}

	//Konstruktor
	public MidgardCharacterHelper(MidgardCharakter _character){
		_mCharacter = _character;
	}



	#region lernplan abholen
	/// <summary>
	/// Gets the lernplan for character. Enthält Fachkenntnisse, Waffenfertigkeiten und Zauber
	/// </summary>
	/// <returns>The lernplan for character.</returns>
	/// <param name="listPlaene">List plaene.</param>
	/// <param name="shortName">Short name.</param>
	public Lernplan GetLernplanForCharacter(){
		Toolbox globalVars = Toolbox.Instance;
		List<Lernplan> listPlaene = globalVars.MidgardLernplaene.listLernPlaene;
		string shortName = mCharacter.Archetyp.ToString ();
		//Hole passenden Lernplan für Charakter
		Lernplan charLernplan = null;
		foreach (Lernplan lPlan in listPlaene) {
			if (lPlan.shortname == shortName) {
				charLernplan = lPlan;
			}
		}
		return charLernplan;
	}
	#endregion


	#region fachkenntnisse prüfen
	/// <summary>
	/// Gets the character fachkenntnis by name
	/// </summary>
	/// <returns>The character waffe.</returns>
	/// <param name="name">Name.</param>
	public InventoryItem GetCharacterFachkenntnis(string name){
		foreach (var fachkenntnis in _mCharacter.fertigkeiten) {
			if (name == fachkenntnis.name) {
				return fachkenntnis;
			}
		}
		return null;
	}

	#endregion


	#region waffen abholen
	/// <summary>
	/// Gets the character waffe by Waffenname
	/// </summary>
	/// <returns>The character waffe.</returns>
	/// <param name="name">Name.</param>
	public InventoryItem GetCharacterWaffe(string name){
		foreach (var waffe in _mCharacter.waffenFertigkeiten) {
			if (name == waffe.name) {
				return waffe;
			}
		}
		return null;
	}

	/// <summary>
	/// Gets the nahkampf waffen. Waffengattung holen
	/// </summary>
	/// <returns>The nahkampf waffen.</returns>
	public List<InventoryItem> GetNahkampfWaffen(){
		return GetWaffenTyp ("Nah");
	}

	/// <summary>
	/// Gets the fernkampf waffen. Waffengattung holen
	/// </summary>
	/// <returns>The fernkampf waffen.</returns>
	public List<InventoryItem> GetFernkampfWaffen(){
		return GetWaffenTyp ("Fern");
	}

	/// <summary>
	/// Gets the waffen typ.
	/// </summary>
	/// <returns>The waffen typ.</returns>
	/// <param name="selector">Selector.</param>
	private List<InventoryItem> GetWaffenTyp(string selector){
		List<InventoryItem> waffen = new List<InventoryItem> ();
		foreach (var item in _mCharacter.waffenFertigkeiten) {
			if(item.type.Contains(selector)){
				waffen.Add(item);
			}
		}

		return waffen;
	}
	#endregion


	#region reflection
	/// <summary>
	/// Gets the character field values by Reflection
	/// </summary>
	/// <returns>The character field values.</returns>
	public List<FieldInfo> GetCharacterFieldValues ()
	{
		FieldInfo infoSt = mCharacter.GetType ().GetField ("St");
		FieldInfo infoGs = mCharacter.GetType ().GetField ("Gs");
		FieldInfo infoGw = mCharacter.GetType ().GetField ("Gw");
		FieldInfo infoKo = mCharacter.GetType ().GetField ("Ko");
		FieldInfo infoIn = mCharacter.GetType ().GetField ("In");
		FieldInfo infoZt = mCharacter.GetType ().GetField ("Zt");
		FieldInfo infopA = mCharacter.GetType ().GetField ("pA");
		FieldInfo infoSinne = mCharacter.GetType ().GetField ("listSinne");


		List<FieldInfo> characterInfos = new List<FieldInfo> ();
		characterInfos.Add (infoSt);
		characterInfos.Add (infoGs);
		characterInfos.Add (infoGw);
		characterInfos.Add (infoKo);
		characterInfos.Add (infoIn);
		characterInfos.Add (infoZt);
		characterInfos.Add (infopA);
		characterInfos.Add (infoSinne);

		return characterInfos;
	}
	#endregion

}