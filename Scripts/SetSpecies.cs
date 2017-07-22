using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetSpecies : MonoBehaviour {

    public Dropdown DropRasse;
    public Dropdown DropATyp;


    public void SetCharacterSpecies()
    {
        Toolbox globalVars = Toolbox.Instance;
        MidgardCharakter mCharacter = globalVars.mCharacter;

		Rassen midgardRassen = MidgardResourceReader.GetMidgardResource<Rassen> (MidgardResourceReader.MidgardRassen);

		//Achtung: Hole die ID der Rasse
		int rassenID = ObjectXMLHelper.GetChosenOptionIndex (DropRasse.captionText.text, midgardRassen.rassenListe);
		mCharacter.Spezies = (Races) rassenID-1; //Achtung: enum o-basiert


        //Jetzt müsssen die Optionen für die nächste Dropdown gesetzt werden: Wähle dazu die Abenteuertypen mit der entsprechenden RassenID
		List<AbenteurerTyp> listeTypen = ObjectXMLHelper.GetMidgardObjectAByIndexB<AbenteurerTyp, RasseRef>(MidgardResourceReader.GetMidgardResource<AbenteurerTypen> (MidgardResourceReader.MidgardAbenteurerTypen).listAbenteurerTypen, rassenID);
		ObjectXMLHelper.FillDropBoxMidgardObject<AbenteurerTyp> (listeTypen, DropATyp);

    }

}
