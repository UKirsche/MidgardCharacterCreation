using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UngewFertigkeitenLoader {

	private MidgardCharacterHelper mCharacterHelper;
	private Fertigkeiten midgardFertigkeiten;
	private LernPlanFachWaffen lernPlanFachWaffen;
	private LernplanModify lernPlanModifier;
	private List<FachkenntnisRefAllgemein> fachkenntnisse;


	public UngewFertigkeitenLoader(){
		mCharacterHelper = Toolbox.Instance.mCharacterHelper;
		midgardFertigkeiten = Toolbox.Instance.MidgardFertigkeiten;
		UngewoehnlicheFertigkeiten ungewFertigkeiten = Toolbox.Instance.MidgardUngewoehnlicheFertigkeiten;
		fachkenntnisse = ungewFertigkeiten.fachkenntnisse;
		lernPlanModifier = new LernplanModify ();
		lernPlanFachWaffen = new LernPlanFachWaffen ();
	}


	/// <summary>
	/// Gets the allgemeinwissen items. 
	/// Bemerkung: Haben Charakter in den Leiteigenschaften Werte zw. 81-95 erhöht sich die Fähigkeit im +1, bei 96-100 um +2
	/// </summary>
	/// <returns>The fachkenntnisse items.</returns>
	public List<InventoryItem> GetUngewoehnlicheFertigkeiten(){


		lernPlanFachWaffen.IsBonusLeiteigenschaft = true;
		lernPlanFachWaffen.IsMalusLeiteigenschaft = false;

		//Verbinde die fachkenntnisse
		List<InventoryItem> returnListFach = lernPlanFachWaffen.GetRelevantFertigkeit<FachkenntnisRefAllgemein, Fachkenntnis> (fachkenntnisse, midgardFertigkeiten.fachKenntnisse);
		CheckAllgemeinWissen (fachkenntnisse, returnListFach);

		return returnListFach;
	}
		

	void CheckAllgemeinWissen (List<FachkenntnisRefAllgemein> fachkenntnisse, List<InventoryItem> returnListFach)
	{
		foreach (var item in returnListFach.ToArray ()) {
			item.type = "Fach";
			lernPlanModifier.ModifySpracheUngewoehnlicheFertigkeiten (fachkenntnisse, item, returnListFach);
			if (mCharacterHelper.GetCharacterFachkenntnis (item.name) != null) {
				returnListFach.Remove (item);
			}
		}
	}
}
