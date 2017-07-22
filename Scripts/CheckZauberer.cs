using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CheckZauberer : MonoBehaviour {

	public Text textZauberer;
	public InputField inputZauberer;
	public Button buttonZauberer;

	private int abKategorie;

	// Use this for initialization
	void Start () {
		abKategorie = CharacterEngine.GetKategorieForAbenteurerTyp ((int)Toolbox.Instance.mCharacter.Archetyp + 1);

		//Kämpfer->kein Zauberer
		if (abKategorie == 2) {
			DeactivateZaubererUI ();
		}
	}

	void DeactivateZaubererUI ()
	{
		textZauberer.gameObject.SetActive (false);
		inputZauberer.gameObject.SetActive (false);
		buttonZauberer.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
