using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetATyp : MonoBehaviour {

    public Dropdown AbTyp, SexTyp;

    public void SetTypAndSex()
    {
        Toolbox globalVars = Toolbox.Instance;
        MidgardCharakter mCharacter = globalVars.mCharacter;
		//Lädt die relevante ID für die ausgewählten optionstext
		int AbID = ObjectXMLHelper.GetChosenOptionIndex (AbTyp.captionText.text, MidgardResourceReader.GetMidgardResource<AbenteurerTypen> (MidgardResourceReader.MidgardAbenteurerTypen).listAbenteurerTypen);

		mCharacter.Archetyp = (AbenteuerTyp) AbID-1; //Achtung enum nullbasiert
        mCharacter.Sex = (Geschlecht)SexTyp.value;
    }

}
