using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MidgardCharacterSheetManager : MonoBehaviour {

	public Text typ, stand, beruf, gestalt, hand, alter, groesse, gewicht, staerke, konsitution, aussehen, selbstbeherrschung, geschicklichkeit, intelligenz, 
	ausstrahlung, willenskraft, gewandtheit, zaubertalent, bewegungsweite, abwehr, raufen, zaubern, resPsy, resPhy, resPhk, ap, lp,ausdauerbonus,
	abwehrbonus,schadensbonus,zauberbonus,angriffsbonus, sehen, hoeren, riechen, schmecken, tasten, sechstersinn, sonst, sonstUeberschrift;


	protected MidgardCharakter mCharacter;
	MidgardCharacterHelper characterHelper;

	// Use this for initialization
	public virtual void Start () {
		Toolbox globalVars = Toolbox.Instance;
		mCharacter = globalVars.mCharacter;
		characterHelper = globalVars.mCharacterHelper;

		SetCharacterValues ();
	}
	

	/// <summary>
	/// Sets the character values für den erzeugten MidgardCharakter
	/// </summary>
	protected void SetCharacterValues(){
		SetDemoskopieWerte ();
		SetBasiseigenschaften ();
		SetKampfWerte ();
		SetBoni ();
		SetSinne ();
		SetAngeboren ();
	}

	void SetDemoskopieWerte ()
	{
		typ.text = mCharacter.Archetyp.ToString ();
		stand.text = mCharacter.Schicht.ToString ();
		gestalt.text = mCharacter.Gestalt;
		hand.text = mCharacter.hand.ToString ();
		alter.text = mCharacter.Alter.ToString ();
		groesse.text = mCharacter.Groesse.ToString ();
		gewicht.text = mCharacter.Gewicht.ToString ();
	}

	void SetBasiseigenschaften ()
	{
		staerke.text = mCharacter.St.ToString ();
		konsitution.text = mCharacter.Ko.ToString ();
		aussehen.text = mCharacter.Aussehen.ToString ();
		selbstbeherrschung.text = mCharacter.Sb.ToString ();
		geschicklichkeit.text = mCharacter.Gs.ToString ();
		intelligenz.text = mCharacter.In.ToString ();
		ausstrahlung.text = mCharacter.pA.ToString ();
		willenskraft.text = mCharacter.Wk.ToString ();
		gewandtheit.text = mCharacter.Gw.ToString ();
		zaubertalent.text = mCharacter.Zt.ToString ();
		bewegungsweite.text = mCharacter.B.ToString ();
	}

	void SetKampfWerte ()
	{
		abwehr.text = mCharacter.Abwehr.ToString ();
		raufen.text = mCharacter.Raufen.ToString ();
		zaubern.text = mCharacter.Zaubern.ToString ();

		resPsy.text = mCharacter.resPsy.ToString();
		resPhy.text = mCharacter.resPhy.ToString();
		resPhk.text = mCharacter.resPhk.ToString();

		ap.text = mCharacter.AP.ToString ();
		lp.text = mCharacter.LP.ToString ();
	}

	void SetBoni ()
	{
		ausdauerbonus.text = mCharacter.AusB.ToString ();
		abwehrbonus.text = mCharacter.AbB.ToString ();
		schadensbonus.text = mCharacter.SchB.ToString ();
		zauberbonus.text = mCharacter.ZauB.ToString ();
		angriffsbonus.text = mCharacter.AnB.ToString ();
	}


	void SetSinne(){
		List<Sinn> sinne = mCharacter.listSinne;
		foreach (var sinn in sinne) {
			if (sinn.name == "Sehen") {
				sehen.text = sinn.value.ToString();
			} else if (sinn.name == "Hören") {
				hoeren.text = sinn.value.ToString();
			} else if (sinn.name == "Riechen") {
				riechen.text = sinn.value.ToString();
			} else if (sinn.name == "Schmecken") {
				schmecken.text = sinn.value.ToString ();
			} else if (sinn.name == "Tasten") {
				tasten.text = sinn.value.ToString();
			} else if (sinn.name == "Sechster Sinn") {
				sechstersinn.text = sinn.value.ToString();
			} 
		}
	}

	void SetAngeboren(){
		List<AngeboreneFertigkeit> angeboreneFertigkeite = mCharacter.listAngeboren;
		foreach (var fertigkeit in angeboreneFertigkeite) {
			sonstUeberschrift.text = fertigkeit.name + ":";
			sonst.text = fertigkeit.value.ToString ();
		}
			
	}
}
