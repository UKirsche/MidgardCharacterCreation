using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SetEigenschaften : MonoBehaviour {



	public InputField inB, inGroesse, inGewicht, inAussehen, inHandWurf, inHandText, inpA, inSb, inWk, inAP, inLP, inAnB, inAbB, inZauB, inAbwehr, inZaubern, inRaufen, inResPsy, 
		inResPhy, inResPhk, inAngeboren, inStandWurf, inStandText, inLernPunkteFach, inLernPunkteWaffen, inLernPunkteZauber, inResteLP, inPoolLP, inLernPunkteAllgemeinWissen, 
		inRestAllgemeinWissen, inLernPunkteUngewFertigkeiten, inRestUngewFertigkeiten, inBerufsWurf, inCharBeschreibung, inCharName;
	public GameObject hilfePanelW100;
	public Dropdown dropAngeboren, dropHerkunft, dropSpezialWaffe;

	#region Erscheinung
    // Use this for initialization
    public void SetErscheinungCompute()
    {
        Toolbox globalVars = Toolbox.Instance;

        //Berechne-> und speicher in Charakter
        CharacterEngine.ComputeErscheinung(globalVars.mCharacter);

        inB.text = globalVars.mCharacter.B.ToString();
        inGroesse.text =globalVars.mCharacter.Groesse.ToString();
        inGewicht.text= globalVars.mCharacter.Gewicht.ToString();
        inAussehen.text= globalVars.mCharacter.Aussehen.ToString();

        
        Debug.Log(globalVars.mCharacter.ToString());
    }

    // Use this for initialization
    public void SetErscheinungFromInput()
    {
        Toolbox globalVars = Toolbox.Instance;

        int B  = DiceCreator.ConvertInFieldToInt(inB);
        int Groesse =DiceCreator.ConvertInFieldToInt(inGroesse);
        int Gewicht= DiceCreator.ConvertInFieldToInt(inGewicht);
        int Aussehen = DiceCreator.ConvertInFieldToInt(inAussehen);

        globalVars.mCharacter.B = B;
        globalVars.mCharacter.Groesse = Groesse;
        globalVars.mCharacter.Gewicht = Gewicht;
        globalVars.mCharacter.Aussehen = Aussehen;

        Debug.Log(globalVars.mCharacter.ToString());
    }
	#endregion

	#region Hand
    public void SetHandComputed()
    {
		Toolbox globalVars = Toolbox.Instance;
		int w20 = CharacterEngine.ComputeHandWurf (globalVars.mCharacter);
        inHandWurf.text = w20.ToString();

        Hand hand = CharacterEngine.GetHandfromWurf(w20);

        switch (hand)
        {
            case Hand.links:
                inHandText.text = "links";
                break;
            case Hand.rechts:
                inHandText.text = "rechts";
                break;
            case Hand.beide:
                inHandText.text = "beidhändig";
                break;
            default:
                break;
        }
    }

    public void SetHandCharacter()
    {
        Toolbox globalVars = Toolbox.Instance;

        if (inHandText.text != "")
        {
			int w20 = Convert.ToInt32(inHandWurf.text);
            globalVars.mCharacter.hand = CharacterEngine.GetHandfromWurf(w20);
			Debug.Log (globalVars.mCharacter.ToString ());
        }
    }
	#endregion

	#region psyche
	public void SetPsycheCompute()
	{
		Toolbox globalVars = Toolbox.Instance;

		//Berechne-> und speicher in Charakter
		CharacterEngine.ComputePsychisch(globalVars.mCharacter);

		inpA.text = globalVars.mCharacter.pA.ToString();
		inSb.text =globalVars.mCharacter.Sb.ToString();
		inWk.text= globalVars.mCharacter.Wk.ToString();

		Debug.Log(globalVars.mCharacter.ToString());
	}

	public void SetPsycheFromInput()
	{
		Toolbox globalVars = Toolbox.Instance;

		int pA  = DiceCreator.ConvertInFieldToInt(inpA);
		int Sb =DiceCreator.ConvertInFieldToInt(inSb);
		int Wk= DiceCreator.ConvertInFieldToInt(inWk);

		globalVars.mCharacter.pA = pA;
		globalVars.mCharacter.Sb = Sb;
		globalVars.mCharacter.Wk = Wk;

		Debug.Log(globalVars.mCharacter.ToString());
	}

	#endregion

	#region LPAP
	public void SetAPLPCompute()
	{
		Toolbox globalVars = Toolbox.Instance;

		//Berechne-> und speicher in Charakter
		CharacterEngine.ComputeAPLP(globalVars.mCharacter);

		inAP.text = globalVars.mCharacter.AP.ToString();
		inLP.text =globalVars.mCharacter.LP.ToString();

		Debug.Log(globalVars.mCharacter.ToString());
	}

	public void SetAPLPFromInput()
	{
		Toolbox globalVars = Toolbox.Instance;

		int AP  = DiceCreator.ConvertInFieldToInt(inAP);
		int LP =DiceCreator.ConvertInFieldToInt(inLP);

		globalVars.mCharacter.AP = AP;
		globalVars.mCharacter.LP = LP;

		Debug.Log(globalVars.mCharacter.ToString());
	}

	#endregion

	#region Naturgegebene
	public void SetNaturCompute()
	{
		Toolbox globalVars = Toolbox.Instance;

		//Berechne-> und speicher in Charakter
		CharacterEngine.ComputeNaturGegebenI(globalVars.mCharacter);

		inAnB.text = globalVars.mCharacter.AnB.ToString();
		inAbB.text =globalVars.mCharacter.AbB.ToString();
		inZauB.text= globalVars.mCharacter.ZauB.ToString();
		inZaubern.text= globalVars.mCharacter.Zaubern.ToString();
		inAbwehr.text= globalVars.mCharacter.Abwehr.ToString();
		inRaufen.text= globalVars.mCharacter.Raufen.ToString();

		Debug.Log(globalVars.mCharacter.ToString());

	}
	#endregion


	#region Naturgegebene
	public void SetResistenzenCompute()
	{
		Toolbox globalVars = Toolbox.Instance;

		//Berechne-> und speicher in Charakter
		CharacterEngine.ComputeResistenzen(globalVars.mCharacter);

		inResPsy.text =globalVars.mCharacter.resPsy.ToString();
		inResPhy.text= globalVars.mCharacter.resPhy.ToString();
		inResPhk.text = globalVars.mCharacter.resPhk.ToString();

		Debug.Log(globalVars.mCharacter.ToString());

	}

	public void PrepareSinne(){
		Toolbox globalVars = Toolbox.Instance;

		//Berechne-> und speicher in Charakter
		CharacterEngine.PrepareAngeboreneFertigkeiten(globalVars.mCharacter);
	}

	/// <summary>
	/// Automatischer Wurf. Setze Wert in Textbox, bei 100: Hilfe anzeigen
	/// </summary>
	public void ThrowDiceAngeboren(){
		
		int w100 = UnityEngine.Random.Range (1, 101);
		inAngeboren.text = w100.ToString();

		if (w100 == 100) {
			//1. Show-Hilfetext
			hilfePanelW100.SetActive(true);
		} 

		//Zeige korrekte Dropbox-Eingabe, falls möglich
		ReadDiceToDrobBox ();

	}

	/// <summary>
	/// Reads the dice angeboren. Notwewndig, falls Spieler selber wählt
	/// </summary>
	public void ReadDiceToDrobBox(){
		int w100=DiceCreator.ConvertInFieldToInt (inAngeboren);
		if (w100 < 100) {
			SetDropBoxAngeboren (w100);
		} else {
			hilfePanelW100.SetActive (true);
		}

	}


	/// <summary>
	/// Sets the charakter value from drop box of angeborene Fertigkeiten
	/// </summary>
	public void SetCharakterValueFromDropBox(){


		int w100=DiceCreator.ConvertInFieldToInt (inAngeboren);

		Toolbox globalVars = Toolbox.Instance;
		MidgardCharakter mChar = globalVars.mCharacter;
		globalVars.angeborene100 = false;

		//Selektierte Option:
		int selOption = dropAngeboren.value;

		//Setze den Charakterwert, der sich aus Dropbox-value ergibt
		switch (selOption) {
		case 0:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 2);
			break;
		case 1:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 4);
			break;
		case 2: 
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 6);
			break;
		case 3:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 8);
			break;
		case 4:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 10);
			break;
		case 5:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 20);
			break;
		case 6:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 30);
			break;
		case 7:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 40);
			break;
		case 8:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 50);
			break;
		case 9:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 60);
			break;
		case 10:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 65);
			break;
		case 11:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar,70);
			break;
		case 12:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 75);
			break;
		case 13:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 80);
			break;
		case 14:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 85);
			break;
		case 15:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 90);
			break;
		case 16:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 95);
			break;
		case 17:
			CharacterEngine.ModifyAngeboreneFertigkeiten (mChar, 99);
			break;
		default:
			break;
		}

		//Bleibe auf Seite, setze Variablen zurück
		if (w100 == 100) {
			hilfePanelW100.SetActive (false);
			globalVars.angeborene100 = true;
			dropAngeboren.value = 0;
			inAngeboren.text = "";

		} 
	}



	/// <summary>
	/// Sets the sinn modifikation: im Charakter und wählt des entsprechende Element in der Dropdown
	/// </summary>
	/// <returns><c>true</c>, if sinn modifikation was set, <c>false</c> otherwise.</returns>
	/// <param name="w100">W100.</param>
	public void SetDropBoxAngeboren(int w100){

		if (w100 <= 2) {
			dropAngeboren.value = 0;
		} else if (w100 > 2 && w100 <= 4) {
			dropAngeboren.value = 1;
		} else if (w100 > 4 && w100 <= 6) {
			dropAngeboren.value = 2;
		} else if (w100 > 6 && w100 <= 8) {
			dropAngeboren.value = 3;
		} else if (w100 > 8 && w100 <= 10) {
			dropAngeboren.value = 4;
		} else if (w100 > 10 && w100 <= 20) {
			dropAngeboren.value = 5;
		} else if (w100 > 20 && w100 <= 30) {
			dropAngeboren.value = 6;
		} else if (w100 > 30 && w100 <= 40) {
			dropAngeboren.value = 7;
		} else if (w100 > 40 && w100 <= 50) {
			dropAngeboren.value = 8;
		} else if (w100 > 50 && w100 <= 60) {
			dropAngeboren.value = 9;
		} else if (w100 > 60 && w100 <= 65) {
			dropAngeboren.value = 10;
		} else if (w100 > 65 && w100 <= 70) {
			dropAngeboren.value = 11;
		} else if (w100 > 70 && w100 <= 75) {
			dropAngeboren.value = 12;
		} else if (w100 > 75 && w100 <= 80) {
			dropAngeboren.value = 13;
		} else if (w100 > 80 && w100 <= 85) {
			dropAngeboren.value = 134;
		} else if (w100 > 85 && w100 <= 90) {
			dropAngeboren.value = 15;
		} else if (w100 > 90 && w100 <= 95) {
			dropAngeboren.value = 16;
		} else if (w100 > 95 && w100 <= 99) {
			dropAngeboren.value = 17;
		} 

	}
	#endregion

	#region Stand
	public void SetStandComputed()
	{

		int w100 = CharacterEngine.ComputeStandWurf();
		inStandWurf.text = w100.ToString();

		Stand stand = CharacterEngine.GetStandfromWurf(w100);

		switch (stand)
		{
		case Stand.Unfrei:
			inStandText.text = "unfrei";
			break;
		case Stand.Volk:
			inStandText.text = "Volk";
			break;
		case Stand.Mittelschicht:
			inStandText.text = "Mittelschicht";
			break;
		case Stand.Adel:
			inStandText.text = "Adel";
			break;
		default:
			break;
		}
	}

	public void SetStandCharacter()
	{
		Toolbox globalVars = Toolbox.Instance;
		globalVars.InstantiateLernplanHelpers (); //Bereite Lernplanhelper vor!

		if (inStandText.text != "")
		{
			int w100 = Convert.ToInt32(inStandWurf.text);
			globalVars.mCharacter.Schicht = CharacterEngine.GetStandfromWurf(w100);
			Debug.Log (globalVars.mCharacter.ToString ());
		}
	}
	#endregion

	#region Lernplan
	public void SetLernPlanPunkteFach(){

		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		lernHelper.LernPunkteFach = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7);
		inLernPunkteFach.text = lernHelper.LernPunkteFach.ToString ();

		lernHelper.lernPunkteResetFach = true; //neu gewürfelt 
	}

	public void SetLernPlanPunkteWaffen(){

		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		lernHelper.LernPunkteWaffen = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7);
		inLernPunkteWaffen.text = lernHelper.LernPunkteWaffen.ToString ();

		lernHelper.lernPunkteResetWaffe = true; //neu gewürfelt
	}

	public void SetLernPlanPunkteZauber(){

		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		lernHelper.LernPunkteZauber = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7);
		inLernPunkteZauber.text = lernHelper.LernPunkteZauber.ToString ();

		lernHelper.lernPunkteResetZauber = true; //neu gewürfelt
	}

	public void FillLernPunkteFields()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Zuerst werden nur die Fachkenntnissse eingefüllt:
		inResteLP.text = lernHelper.LernPunkteFach.ToString();

		//Restepool:
		lernHelper.LernPunktePool = lernHelper.LernPunkteFach + lernHelper.LernPunkteWaffen + lernHelper.LernPunkteZauber;
		inPoolLP.text = lernHelper.LernPunktePool.ToString();
	}

	#endregion


	#region Herkunft
	public void SetHerkunft(){

		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;
		string herkunft = dropHerkunft.captionText.text;

		if (herkunft == "Stadt") {
			mCharacter.StadtLand = StadtLandFluss.Stadt;
		} else {
			mCharacter.StadtLand = StadtLandFluss.Land;
		}
	}
	#endregion


	#region SpezialWaffe
	/// <summary>
	/// Sets the value spezial waffe. Dazu wird die ausgewählte Waffe im Character angepasst
	/// </summary>
	public void SetValueSpezialWaffe(){

		MidgardCharacterHelper mCHelper = Toolbox.Instance.mCharacterHelper;
		string waffeSelected = dropSpezialWaffe.captionText.text;
		InventoryItem waffeCharacterSpezial = mCHelper.GetCharacterWaffe (waffeSelected);
		if (waffeCharacterSpezial != null) {
			waffeCharacterSpezial.val = "7";
		}

	}
	#endregion
	

	#region Lernplan Allgmeinwissen
	public void SetLernPlanPunkteAllgemeinwissen(){

		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		lernHelper.LernPunkteAllgemeinWissen = UnityEngine.Random.Range (1, 7) + 1;
		inLernPunkteAllgemeinWissen.text = lernHelper.LernPunkteAllgemeinWissen.ToString ();

		lernHelper.lernPunkteResetAllgemein = true; //neu gewürfelt 
	}

	public void FillLernPunkteAllgemeinWissen()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		//Zuerst werden nur die Fachkenntnissse eingefüllt:
		inRestAllgemeinWissen.text = lernHelper.LernPunkteAllgemeinWissen.ToString();

	}

	#endregion

	#region Lernplan Ungewöhnliche Fertigkeiten
	public void SetLernPunkteUngewFertigkeiten(){

		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		lernHelper.LernPunkteUngewFertigkeiten = UnityEngine.Random.Range (1, 7);
		inLernPunkteUngewFertigkeiten.text = lernHelper.LernPunkteUngewFertigkeiten.ToString ();

		lernHelper.lernPunkteResetUngewFertigkeiten = true; //neu gewürfelt 
	}

	public void FillLernPunkteUngewFertigkeiten()
	{
		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;
		int gesamtUngewFertigkeiten = lernHelper.LernPunkteUngewFertigkeiten + lernHelper.LernPunktePool;

		//Zuerst werden nur die Fachkenntnissse eingefüllt:
		inRestUngewFertigkeiten.text = gesamtUngewFertigkeiten.ToString();

		lernHelper.LernPunkteUngewFertigkeiten = gesamtUngewFertigkeiten;

	}

	#endregion

	#region Berufswahl W100
	/// <summary>
	/// Sets the berufs wahl dice.
	/// </summary>
	public void SetBerufsWahlDice(){

		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lernHelper = globalVars.lernHelper;

		lernHelper.BerufswahlW100 = UnityEngine.Random.Range (1, 101); //W100 für die Berufswahl
		inBerufsWurf.text = lernHelper.BerufswahlW100.ToString();

	}
	#endregion

	#region Überblick
	public void SetCharacterDescription(){
		
		Toolbox globalVars = Toolbox.Instance;
		MidgardCharakter mChar = globalVars.mCharacter;

		mChar.CharacterName = inCharName.text;
		mChar.CharacterBeschreibung = inCharBeschreibung.text; 
	}


	public void SerializeMidgardCharacter(){
		Toolbox globalVars = Toolbox.Instance;
		MidgardCharakter mChar = globalVars.mCharacter;
		MidgardCharacterSaveLoad.Save (mChar);
	}
	#endregion
}
