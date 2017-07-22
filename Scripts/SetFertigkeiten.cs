using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SetFertigkeiten : MonoBehaviour {

	public InputField inRestLP;

	public void SetRestLP(){

		Toolbox globalVars = Toolbox.Instance;
		LernPlanHelper lpHelper = globalVars.lernHelper;

		GameObject fachPanel = GameObject.Find ("GewähltFach");
		GameObject waffenPanel = GameObject.Find ("GewähltWaffen");
		GameObject zauberPanel = GameObject.Find ("GewähltZauber");

		if (fachPanel != null) {
			inRestLP.text = lpHelper.LernPunkteFach.ToString ();
		} else if (waffenPanel != null) {
			inRestLP.text = lpHelper.LernPunkteWaffen.ToString ();
		} else if (zauberPanel != null) {
			inRestLP.text = lpHelper.LernPunkteZauber.ToString ();
		}
	}
}
