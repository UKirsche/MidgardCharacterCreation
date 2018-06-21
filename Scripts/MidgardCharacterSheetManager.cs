using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MidgardCharacterSheetManager : MonoBehaviour {

	public Text typ, stand, beruf, gestalt, hand, alter, groesse, gewicht, staerke, konsitution, aussehen, selbstbeherrschung, geschicklichkeit, intelligenz, 
	ausstrahlung, willenskraft, gewandtheit, zaubertalent, bewegungsweite, abwehr, raufen, zaubern, resistenz, ap, lp,ausdauerbonus,
	abwehrbonus,schadensbonus,zauberbonus,angriffsbonus, sehen, hören, riechen, schmecken, tasten, sechstersinn, sonst, sonstUeberschrift;


	MidgardCharakter mCharacter;
	MidgardCharacterHelper characterHelper;

	// Use this for initialization
	void Start () {
		Toolbox globalVars = Toolbox.Instance;
		mCharacter = globalVars.mCharacter;
		characterHelper = globalVars.mCharacterHelper;

		SetCharacterValues ();
	}
	

	/// <summary>
	/// Sets the character values für den erzeugten MidgardCharakter
	/// </summary>
	void SetCharacterValues(){
		typ.text = mCharacter.Archetyp.ToString ();
		stand.text = mCharacter.Schicht.ToString ();
		gestalt.text = mCharacter.Gestalt;
		hand.text = mCharacter.hand.ToString ();
		alter.text = mCharacter.Alter.ToString ();
		groesse.text = mCharacter.Groesse.ToString ();
		gewicht.text = mCharacter.Gewicht.ToString ();
		staerke.text = mCharacter.St.ToString ();
		konsitution.text = mCharacter.Ko.ToString ();
	}
}
