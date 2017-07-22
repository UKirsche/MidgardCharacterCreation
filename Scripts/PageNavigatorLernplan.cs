using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageNavigatorLernplan : PageNavigatorBasic {

	public GameObject LernFachPage, LernWaffePage, LernZauberPage, FachChosenPage, WaffenChosenPage, ZauberChosenPage;
	public static int activePageIndex;
	protected List<GameObject> orderPages = new List<GameObject>();
	protected List<GameObject> twinPages = new List<GameObject>();
	private int orderPagesMax;

	public void Awake()
	{
		Toolbox globalVars = Toolbox.Instance;
		MidgardCharakter mCharacter = globalVars.mCharacter;

		orderPages.Add (LernFachPage);
		orderPages.Add (LernWaffePage);
		orderPages.Add (LernZauberPage);
		twinPages.Add (FachChosenPage);
		twinPages.Add (WaffenChosenPage);
		twinPages.Add (ZauberChosenPage);

		//Hier lässt sich die Maximalzahl festlegen
		int katIdCharacter = CharacterEngine.GetKategorieForAbenteurerTyp( (int) mCharacter.Archetyp+1);

		//Falls der Zauber mächtig
		if (katIdCharacter == 1 || katIdCharacter == 3) {
			orderPagesMax = orderPages.Count - 1;
		} else {
			orderPagesMax = orderPages.Count - 2;	
		}


	}

	public void GetNextPage()
	{
		if (activePageIndex < orderPagesMax) {
			orderPages [activePageIndex].SetActive (false);
			twinPages [activePageIndex].SetActive (false);
			activePageIndex++;
			orderPages [activePageIndex].SetActive (true);
			twinPages [activePageIndex].SetActive (true);
		}
	}


	public void GetPreviousPage()
	{
		if (activePageIndex > 0) {
			orderPages [activePageIndex].SetActive (false);
			twinPages [activePageIndex].SetActive (false);
			activePageIndex--;
			orderPages [activePageIndex].SetActive (true);
			twinPages [activePageIndex].SetActive (true);
		}
	}

}
