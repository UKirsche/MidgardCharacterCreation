using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllgemeinWissenLoader {

	private MidgardCharakter mCharacter;
	private MidgardCharacterHelper mCharacterHelper;
	private Fertigkeiten midgardFertigkeiten;
	private Waffenfertigkeiten midgardWaffenFertigkeiten;
	private Allgemeinwissen allgemeinWissen;
	private LernPlanFachWaffen lernPlanFachWaffen;
	private LernplanModify lernPlanModifier;
	private List<FachkenntnisRefAllgemein> fachkenntnisse;
	private List<WaffenfertigkeitRef> waffen;


	public AllgemeinWissenLoader(){
		mCharacterHelper = Toolbox.Instance.mCharacterHelper;
		mCharacter = Toolbox.Instance.mCharacter;
		midgardFertigkeiten = Toolbox.Instance.MidgardFertigkeiten;
		midgardWaffenFertigkeiten = Toolbox.Instance.MidgardWaffenFertigkeiten;
		allgemeinWissen = Toolbox.Instance.MidgardAllgemeinwissen;
		lernPlanModifier = new LernplanModify ();
		lernPlanFachWaffen = new LernPlanFachWaffen ();
	}


	/// <summary>
	/// Gets the allgemeinwissen items. 
	/// Bemerkung: Haben Charakter in den Leiteigenschaften Werte zw. 81-95 erhöht sich die Fähigkeit im +1, bei 96-100 um +2
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetAllgemeinFachkenntnisse(){

		GetAllgemeinWissenStadtLand ();

		//Allgemeinwissen Fachkenntnisse nur Bonus
		lernPlanFachWaffen.IsBonusLeiteigenschaft = true;
		lernPlanFachWaffen.IsMalusLeiteigenschaft = false;

		//Verbinde die fachkenntnisse
		List<InventoryItem> returnListFach = lernPlanFachWaffen.GetRelevantFertigkeit<FachkenntnisRefAllgemein, Fachkenntnis> (fachkenntnisse, midgardFertigkeiten.fachKenntnisse);
		CheckAllgemeinWissen (fachkenntnisse, returnListFach);

		return returnListFach;
	}

	public List<InventoryItem> GetAllgemeinWaffen(){

		//AllgemeinWissen Waffen weder Bonus noch Malus
		lernPlanFachWaffen.IsBonusLeiteigenschaft = false;
		lernPlanFachWaffen.IsMalusLeiteigenschaft = false;
		List<InventoryItem> returnListWaffen = lernPlanFachWaffen.GetRelevantFertigkeit<WaffenfertigkeitRef, Waffenfertigkeit> (waffen,  midgardWaffenFertigkeiten.waffenFertigkeiten);

		//Teste Waffenfertigkeit auf Doubles
		//FilterOutFertigkeiten(returnListWaffen, false);	

		//Concatenate
		return returnListWaffen;
	}

	void CheckAllgemeinWissen (List<FachkenntnisRefAllgemein> fachkenntnisse, List<InventoryItem> returnListFach)
	{
		foreach (var item in returnListFach.ToArray ()) {
			item.type = "Fach";
			lernPlanModifier.ModifySpracheAllgemeinWissen (fachkenntnisse, item, returnListFach);
			if (mCharacterHelper.GetCharacterFachkenntnis (item.name) != null) {
				returnListFach.Remove (item);
			}
		}
	}

	private void GetAllgemeinWissenStadtLand ()
	{
		if (mCharacter.StadtLand == StadtLandFluss.Stadt) {
			fachkenntnisse = allgemeinWissen.stadtAllgemeinWissen.fachkenntnisse;
			waffen = allgemeinWissen.stadtAllgemeinWissen.waffen;
		}
		else {
			fachkenntnisse = allgemeinWissen.landAllgemeinWissen.fachkenntnisse;
			waffen = allgemeinWissen.landAllgemeinWissen.waffen;
		}
	}
}
