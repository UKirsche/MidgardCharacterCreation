using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CheckStadtLand : MonoBehaviour {

	public Dropdown selectStadtLand;

	// Use this for initialization
	void Start () {
	
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;
		AbenteuerTyp aTyp = mCharacter.Archetyp;

		switch (aTyp) {
		case AbenteuerTyp.Er:
		case AbenteuerTyp.Sp:
		case AbenteuerTyp.Hä:
		case AbenteuerTyp.Th:
			selectStadtLand.captionText.text = "Stadt";
			selectStadtLand.interactable = false;
			break;
		case AbenteuerTyp.BN:
		case AbenteuerTyp.BS:
		case AbenteuerTyp.BW:
		case AbenteuerTyp.Ku:
		case AbenteuerTyp.Tm:
		case AbenteuerTyp.Wa:
		case AbenteuerTyp.Sc:
			selectStadtLand.captionText.text = "Land";
			selectStadtLand.interactable = false;
			break;
		default:
			break;
		}


		if(mCharacter.Spezies == Races.Elf){

			selectStadtLand.captionText.text = "Land";
			selectStadtLand.interactable = false;
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
