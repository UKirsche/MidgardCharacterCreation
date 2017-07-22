using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PageNavigator : PageNavigatorBasic {

	#region pageobjekte
	public GameObject PageRasse, PageAbenteuerTyp, PageBasisEigenschaften, PageAbgeleiteteEigenschaften, PageErscheinung, PageHand, PagePsyche, PageAPLP, PageNatur, PageResistenzen, PageSinne, PageStand, PageLernPunkte, PageLernplan, PageStadtLand, 
	PageSpezialWaffe, PageLernPunkte2, PageAllgemeinwissen, PageLernPunkte3, PageUngewFertigkeiten, PageLernPunkte4, PageBeruf, PageUngelernte;
	public static int activePageIndex;
	protected List<GameObject> orderPages = new List<GameObject>();
	#endregion

	public void Awake()
	{
		orderPages.Add (PageRasse);
		orderPages.Add (PageAbenteuerTyp);
		orderPages.Add (PageBasisEigenschaften);
        orderPages.Add(PageAbgeleiteteEigenschaften);
        orderPages.Add(PageErscheinung);
        orderPages.Add(PageHand);
		orderPages.Add (PagePsyche);
		orderPages.Add (PageAPLP);
		orderPages.Add (PageNatur);
		orderPages.Add (PageResistenzen);
		orderPages.Add (PageSinne);
		orderPages.Add (PageStand);
		orderPages.Add (PageLernPunkte);
		orderPages.Add (PageLernplan);
		orderPages.Add (PageStadtLand);
		orderPages.Add (PageSpezialWaffe);
		orderPages.Add (PageLernPunkte2);
		orderPages.Add (PageAllgemeinwissen);
		orderPages.Add (PageLernPunkte3);
		orderPages.Add (PageUngewFertigkeiten);
		orderPages.Add (PageLernPunkte4);
		orderPages.Add (PageBeruf);
		orderPages.Add (PageUngelernte);
    }

	#region Sondernavis
	public void GetNextPageAngeborene()
	{
		Toolbox globalVars = Toolbox.Instance;
		bool ang100 = globalVars.angeborene100;
		
		if (!ang100) {
			orderPages [activePageIndex].SetActive (false);
			activePageIndex++;
			orderPages [activePageIndex].SetActive (true);
		}

	}

	public void GetNextPageBerufe(){
		Toolbox globalVars = Toolbox.Instance;
		int berufsWurf = globalVars.lernHelper.BerufswahlW100;
		orderPages [activePageIndex].SetActive (false);
		if (berufsWurf <= 20) {
			activePageIndex += 2;
		} else {
			activePageIndex += 1;
		}
		orderPages [activePageIndex].SetActive (true);

	}
	#endregion

	#region standardnavi
	public void GetNextPage()
	{
		if (activePageIndex < orderPages.Count - 1) {
			orderPages [activePageIndex].SetActive (false);
			activePageIndex++;
			orderPages [activePageIndex].SetActive (true);
		}

	}


	public void GetPreviousPage()
	{
		if (activePageIndex > 0) {
			orderPages [activePageIndex].SetActive (false);
			activePageIndex--;
			orderPages [activePageIndex].SetActive (true);
		}

	}
	#endregion
}
