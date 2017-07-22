using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public enum AbenteuerTyp
{
	As,
	BN,
	BS,
	BW,
	Er,
	Gl,
	Hä,
	Kr,
	Ku,
	Se,
	Soe,
	Sp,
	Wa,
	Ba,
	Or,
	Tm,
	Be,
	Dr,
	Hl,
	Hx,
	Ma,
	PF,
	PHa,
	PHe,
	PK,
	PM,
	PT,
	PW,
	Sc,
	Th
}

public enum Races
{
	Mensch,
	Elf,
	Halbling,
	Zwerg,
	Berggnom,
	Waldgnom
}

public enum Geschlecht
{
	Mann,
	Frau
}

public enum Stand
{
	Unfrei,
	Volk,
	Mittelschicht,
	Adel
}

public enum Herkunft
{
	Alba,
	Aran,
	Buluga,
	Chryseia,
	Clanngadarn,
	Erainn,
	Eschar,
	Fuardain,
	Ikengabecken,
	KanThaiPan,
	Minangaphit,
	Moravod,
	Nahuatlan,
	Rawindra,
	Valian,
	Waeland,
	Ywerddon
}

public enum Hand
{
	links,
	rechts,
	beide
}

public enum Jobs
{
	Aufseher,
	Bauer,
	Bergmann,
	Bote,
	Brauer,
	Falkner,
	Fallensteller,
	Fischer,
	Flußschiffer,
	Fuhrmann,
	Grobschmied,
	Hirte,
	Hundeführer,
	Jäger,
	Karawanenführer,
	Köhler,
	Krämer
}


public enum Senses
{
	Sehen, Hoeren, Riechen, Schmecken, Tasten, SechsterSinn
}

public enum Natur{
	Berserkergang, Einpraegen, GuteReflexe, Nachtsicht, Richtungssinn, Robustheit, Wachgabe
}


public enum StadtLandFluss{
	Stadt, Land
}


/// <summary>
/// Errechnet die Werte des Charakters
/// </summary>
public class CharacterEngine
{
	#region Helper

	/// <summary>
	/// Gets the minimum value for abenteuer typ entsprechend der verlangen basiseigenschaft
	/// </summary>
	/// <returns>The minimum value for abenteuer typ.</returns>
	/// <param name="aTyp">A typ.</param>
	/// <param name="basisRef">Basis reference.</param>
	private static int GetMinValueForAbenteuerTyp (int aTyp, int basisRef)
	{
		//Hole empfohlene Mindestwerte für den AbenteurerTyp
		List<AbenteurerTyp> listATypen = MidgardResourceReader.GetMidgardResource<AbenteurerTypen> (MidgardResourceReader.MidgardAbenteurerTypen).listAbenteurerTypen;
		int minStATyp = 0;

		foreach (AbenteurerTyp item in listATypen) {
			if (item.id == aTyp) {
				List<BasiseigenschaftRef> minWerteATyp = item.restriktionen.basisEigenschaften;
				foreach (BasiseigenschaftRef minWert in minWerteATyp) {
					if (minWert.id == basisRef) {
						minStATyp = minWert.minValue;
					}
				}
			}
		}

		return minStATyp;
	}


	/// <summary>
	/// Gets the kategorieid for Abenteurertyp
	/// </summary>
	/// <returns>The minimum value for abenteuer typ.</returns>
	/// <param name="aTyp">A typ.</param>
	/// <param name="basisRef">Basis reference.</param>
	public static int GetKategorieForAbenteurerTyp (int aTyp)
	{
		//Hole KategorieId
		List<AbenteurerTyp> listATypen = MidgardResourceReader.GetMidgardResource<AbenteurerTypen> (MidgardResourceReader.MidgardAbenteurerTypen).listAbenteurerTypen;
		int kategorieId = 0;
		foreach (AbenteurerTyp item in listATypen) {
			if (item.id == aTyp) {
				kategorieId = item.katID.id;
				break;
			}
		}

		return kategorieId;
	}


	/// <summary>
	/// Gets the sinn by identifier.
	/// </summary>
	/// <returns>The sinn by identifier.</returns>
	private static T GetSinnAngeborenById<T>(int naturId) where T:IID
	{
		List<T> naturSinne;

		Type itemType = typeof(T);
		if (itemType == typeof(Sinn)) {
			naturSinne = MidgardResourceReader.GetMidgardResource<AngeboreneFertigkeiten> (MidgardResourceReader.MidgardAngeboren).listSinne as List<T>;
		} else {
			naturSinne = MidgardResourceReader.GetMidgardResource<AngeboreneFertigkeiten> (MidgardResourceReader.MidgardAngeboren).listAngFertigkeiten as List<T>;
		}

		T retSinn = naturSinne [0];

		foreach (T sinn in naturSinne) {
			if (sinn.id == naturId) {
				retSinn =  sinn;
				break;
			}
		}

		return retSinn;
	}


	/// <summary>
	/// Repeats the throw basis eigenschaft. tritt ein, wenn min wert unterschritten wird
	/// </summary>
	/// <returns>The throw basis eigenschaft.</returns>
	/// <param name="Eigenschaft">Eigenschaft.</param>
	/// <param name="minValue">Minimum value.</param>
	private static int RepeatThrowBasisEigenschaft (int Eigenschaft, int minValue)
	{
		//Fall ATyp Minimum vorliegt: wiederholen
		while (Eigenschaft < minValue) {
			Eigenschaft =UnityEngine.Random.Range (1, 100);
		}

		return Eigenschaft;

	}

	#endregion

	#region BasisSt

	/// <summary>
	/// Basiseigenschaft: Stärke
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static int ComputeBasisSt (MidgardCharakter mChar)
	{
		const int maxElf = 90;
		const int maxHalbling = 80;
		const int minZwerg = 61;
		const int maxGnom = 60;

		//Hole min-Wert für Leiteigenschaft
		int aTypID = (int)mChar.Archetyp + 1; //enum nullbasiert 
		int minValueStFromLE = GetMinValueForAbenteuerTyp (aTypID, 1); //1==St

		//Grundwurf -> 2 Würfe simuliert
		mChar.St = Mathf.Max (UnityEngine.Random.Range (1, 101), UnityEngine.Random.Range (1, 101));

		//Leiteigenschaften
		mChar.St = RepeatThrowBasisEigenschaft (mChar.St, minValueStFromLE);


		//Anpassung
		switch (mChar.Spezies) {
		case Races.Elf:
			mChar.St = Mathf.Min (mChar.St, maxElf);
			break;
		case Races.Halbling:
			mChar.St = Mathf.Min (mChar.St, maxHalbling);
			break;
		case Races.Zwerg:
			mChar.St = RepeatThrowBasisEigenschaft (mChar.St, minZwerg);
			break;
		case Races.Berggnom:
		case Races.Waldgnom:
			mChar.St = Mathf.Min (maxGnom, mChar.St);
			break;
		default:
			break;
		}

		return mChar.St;
	}

	#endregion

	#region BasisGs

	/// <summary>
	/// Basiseigenschaft: Geschicklichkeit
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static int ComputeBasisGs (MidgardCharakter mChar)
	{
		const int minHalbling = 61;
		const int minGnom = 81;

		//Hole min-Wert für Leiteigenschaft
		int aTypID = (int)mChar.Archetyp + 1; //enum nullbasiert 
		int minValueGsFromLE = GetMinValueForAbenteuerTyp (aTypID, 2); //2==Gs

		//Grundwurf
		mChar.Gs = Mathf.Max (UnityEngine.Random.Range (1, 101), UnityEngine.Random.Range (1, 101));

		//Leiteigenschaften
		mChar.Gs = RepeatThrowBasisEigenschaft (mChar.Gs, minValueGsFromLE);

		//Anpassung
		switch (mChar.Spezies) {
		case Races.Halbling:
			mChar.Gs = RepeatThrowBasisEigenschaft (mChar.Gs, minHalbling);
			break;
		case Races.Berggnom:
		case Races.Waldgnom:
			mChar.Gs = RepeatThrowBasisEigenschaft (mChar.Gs, minGnom);
			break;
		default:
			break;
		}

		return mChar.Gs;

	}

	#endregion

	#region BasisGW

	/// <summary>
	/// Basiseigenschaft: Gewandtheit
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static int ComputeBasisGw (MidgardCharakter mChar)
	{
		const int minElf = 81;
		const int minHalbling = 91;
		const int maxZwerg = 80;
		const int minGnom = 81;

		//Hole min-Wert für Leiteigenschaft
		int aTypID = (int)mChar.Archetyp + 1; //enum nullbasiert 
		int minValueGwFromLE = GetMinValueForAbenteuerTyp (aTypID, 3); //3==Gw


		//Grundwurf
		mChar.Gw = Mathf.Max (UnityEngine.Random.Range (1, 101), UnityEngine.Random.Range (1, 101));

		//Leiteigenschaften
		mChar.Gw = RepeatThrowBasisEigenschaft (mChar.Gw, minValueGwFromLE);

		//Anpassung
		switch (mChar.Spezies) {
		case Races.Elf:
			mChar.Gw = RepeatThrowBasisEigenschaft (mChar.Gw, minElf);
			break;
		case Races.Halbling:
			mChar.Gw = RepeatThrowBasisEigenschaft (mChar.Gw, minHalbling);
			break;
		case Races.Zwerg:
			mChar.Gw = Mathf.Min (mChar.Gw, maxZwerg);
			break;
		case Races.Berggnom:
		case Races.Waldgnom:
			mChar.Gw = RepeatThrowBasisEigenschaft (mChar.Gw, minGnom);
			break;
		default:
			break;
		}

		return mChar.Gw;
	}

	#endregion

	#region BasisKo

	/// <summary>
	/// Basiseigenschaft: Konstitution
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static int ComputeBasisKo (MidgardCharakter mChar)
	{
		const int minElf = 61;
		const int minZwerg = 61;
		const int minGnom = 31;

		//Hole min-Wert für Leiteigenschaft
		int aTypID = (int)mChar.Archetyp + 1; //enum nullbasiert 
		int minValueKoFromLE = GetMinValueForAbenteuerTyp (aTypID, 4); //4==Ko

		//Grundwurf
		mChar.Ko = Mathf.Max (UnityEngine.Random.Range (1, 101), UnityEngine.Random.Range (1, 101));

		//Leiteigenschaften
		mChar.Ko = RepeatThrowBasisEigenschaft (mChar.Ko, minValueKoFromLE);

		//Anpassung
		switch (mChar.Spezies) {
		case Races.Elf:
			mChar.Ko = RepeatThrowBasisEigenschaft (mChar.Ko, minElf);
			break;
		case Races.Zwerg:
			mChar.Ko = RepeatThrowBasisEigenschaft (mChar.Ko, minZwerg);
			break;
		case Races.Berggnom:
		case Races.Waldgnom:
			mChar.Ko = RepeatThrowBasisEigenschaft (mChar.Ko, minGnom);
			break;
		default:
			break;
		}

		return mChar.Ko;
	}

	#endregion

	#region BasisIn

	/// <summary>
	/// Basiseigenschaft: Intelligenz
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static int ComputeBasisIn (MidgardCharakter mChar)
	{

		const int minElf = 61;

		//Hole min-Wert für Leiteigenschaft
		int aTypID = (int)mChar.Archetyp + 1; //enum nullbasiert 
		int minValueInFromLE = GetMinValueForAbenteuerTyp (aTypID, 5); //5==in

		//Grundwurf
		mChar.In = Mathf.Max (UnityEngine.Random.Range (1, 101), UnityEngine.Random.Range (1, 101));

		//Leiteigenschaften
		mChar.In = RepeatThrowBasisEigenschaft (mChar.In, minValueInFromLE);

		switch (mChar.Spezies) {
		case Races.Elf:
			mChar.In = RepeatThrowBasisEigenschaft (mChar.In, minElf);
			break;
		default:
			break;
		}

		return mChar.In;

	}

	#endregion

	#region BasisZt

	/// <summary>
	/// Basiseigenschaft: Zaubertalent
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static int ComputeBasisZt (MidgardCharakter mChar)
	{
		const int minElf = 61;

		int aTypID = (int)mChar.Archetyp + 1; //enum nullbasiert 
		int minValueZtFromLE = GetMinValueForAbenteuerTyp (aTypID, 6); //6==Zt

		//Grundwurf
		mChar.Zt = Mathf.Max (UnityEngine.Random.Range (1, 101), UnityEngine.Random.Range (1, 101));

		//Leiteigenschaften
		mChar.Zt = RepeatThrowBasisEigenschaft (mChar.Zt, minValueZtFromLE);

		switch (mChar.Spezies) {
		case Races.Elf:
			mChar.Zt = RepeatThrowBasisEigenschaft (mChar.Zt, minElf);
			break;
		default:
			break;
		}

		return mChar.Zt;

	}

	#endregion

	#region Abgeleitete Eigenschaften

	/// <summary>
	/// Computes the abgeleitete eigenschaften .
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static void ComputeAbgeleiteteEigenschaften (MidgardCharakter mChar)
	{
		
		mChar.SchB = Mathf.FloorToInt (mChar.St / 20) + Mathf.FloorToInt (mChar.Gs / 30) - 3;
		mChar.AusB = Mathf.FloorToInt (mChar.Ko / 10) + Mathf.FloorToInt (mChar.St / 20) - 7;

	}

	#endregion

	#region Erscheinung

	/// <summary>
	/// Compute the erscheinung
	/// </summary>
	/// <param name="mChar"></param>
	public static void ComputeErscheinung (MidgardCharakter mChar)
	{
		mChar.Aussehen = UnityEngine.Random.Range (1, 101);

		switch (mChar.Spezies) {
		case Races.Mensch:
                //3W3 + 16
			mChar.B = UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 3) + 16;
			if (mChar.Sex == Geschlecht.Mann) {
				mChar.Groesse = UnityEngine.Random.Range (1, 21) + UnityEngine.Random.Range (1, 21) + Mathf.FloorToInt (mChar.St / 10) + 150;
				mChar.Gewicht = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) +
				Mathf.FloorToInt (mChar.St / 10) + mChar.Groesse - 120;
			} else {
				mChar.Groesse = UnityEngine.Random.Range (1, 21) + UnityEngine.Random.Range (1, 21) + Mathf.FloorToInt (mChar.St / 10) + 140;
				mChar.Gewicht = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) - 4 +
				Mathf.FloorToInt (mChar.St / 10) + mChar.Groesse - 120;
			}
			break;

		case Races.Elf:
			mChar.B = UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + 16;
			mChar.Groesse = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + Mathf.FloorToInt (mChar.St / 10) + 160;
			mChar.Gewicht = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) - 8 +
			Mathf.FloorToInt (mChar.St / 10) + mChar.Groesse - 120;
			mChar.Aussehen = Mathf.Max (81, mChar.Aussehen);
			break;
		case Races.Halbling:
			mChar.B = UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + 8;
			mChar.Groesse = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + Mathf.FloorToInt (mChar.St / 10) + 100;
			mChar.Gewicht = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + 3 +
			Mathf.FloorToInt (mChar.St / 10) + mChar.Groesse - 90;
			break;
		case Races.Zwerg:
			mChar.B = UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + 12;
			mChar.Groesse = UnityEngine.Random.Range (1, 7) + Mathf.FloorToInt (mChar.St / 10) + 130;
			mChar.Gewicht = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) +
			Mathf.FloorToInt (mChar.St / 10) + mChar.Groesse - 90;
			mChar.Aussehen = Mathf.Min (80, mChar.Aussehen);

			break;
		case Races.Berggnom:
		case Races.Waldgnom:
			mChar.B = UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + 8;
			mChar.Groesse = UnityEngine.Random.Range (1, 7) + Mathf.FloorToInt (mChar.St / 10) + 90;
			mChar.Gewicht = UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) + UnityEngine.Random.Range (1, 7) +
			Mathf.FloorToInt (mChar.St / 10) + mChar.Groesse - 90;
			mChar.Aussehen = Mathf.Min (80, mChar.Aussehen);
			break;
		default:
			mChar.B = UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + UnityEngine.Random.Range (1, 4) + 16;
			break;
		}

	}

	#endregion


	#region Händigkeit

	/// <summary>
	/// Würfle Händigkeit ohne sie im Charakter zu setzen
	/// </summary>
	/// <returns></returns>
	public static int ComputeHandWurf (MidgardCharakter mChar)
	{
		int handW20 = UnityEngine.Random.Range (1, 21);
		if (mChar.Spezies == Races.Berggnom || mChar.Spezies == Races.Waldgnom) {
			handW20 = 20;
		}
		return handW20;
	}

	/// <summary>
	/// Gets the handenum from int wurf.
	/// </summary>
	/// <returns>The handfrom wurf.</returns>
	/// <param name="w20">W20.</param>
	public static Hand GetHandfromWurf (int w20)
	{
		Hand result;

		if (w20 < 16) {
			result = Hand.rechts;
		} else if (w20 >= 16 && w20 < 20) {
			result = Hand.links;
		} else {
			result = Hand.beide;
		}

		return result;
	}

	#endregion


	#region Stand

	/// <summary>
	/// Würfle Stand ohne sie im Charakter zu setzen
	/// </summary>
	/// <returns></returns>
	public static int ComputeStandWurf ()
	{
		int handW100 = UnityEngine.Random.Range (1, 101);
		return handW100;
	}

	/// <summary>
	/// Gets the Stand from int wurf.
	/// </summary>
	/// <returns>The handfrom wurf.</returns>
	/// <param name="w20">W20.</param>
	public static Stand GetStandfromWurf (int w100)
	{
		Stand result;

		if (w100 < 11) {
			result = Stand.Unfrei;
		} else if (w100 >=11 && w100 < 51) {
			result = Stand.Volk;
		} else if(w100>=51 && w100<91){
			result = Stand.Mittelschicht;
		} else {
			result = Stand.Adel;
		}

		return result;
	}

	#endregion


	#region Psyche

	/// <summary>
	/// Computes the erscheinung quick.
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static void ComputePsychisch (MidgardCharakter mChar)
	{
		ComputePA (mChar);
		ComputeWkSb (mChar);

		//Minimum immer -> Achtung pA muss bei gewissen A-Typen-Min-Werte einhalten
		mChar.pA = Mathf.Max (1, mChar.pA);
		mChar.Sb = Mathf.Max (1, mChar.Sb);
		mChar.Wk = Mathf.Max (1, mChar.Wk);

	}

	/// <summary>
	/// Computes the persönliche Ausstrahlung of the Character. 
	/// Achtung bei Händler (Hl), Glücksritter (Gl), Ba (Barde) oder Tiermeister (Tm) muss die pA mind. 61 betragen
	/// </summary>
	/// <param name="mChar">M char.</param>
	static void ComputePA (MidgardCharakter mChar)
	{
		const int minPA = 61;
		mChar.pA = Mathf.Min (UnityEngine.Random.Range (1, 101) + 3 * (Mathf.FloorToInt (mChar.In / 10 + mChar.Aussehen / 10)) - 30, 100);

		switch (mChar.Archetyp) {
		case AbenteuerTyp.Hä:
		case AbenteuerTyp.Gl:
		case AbenteuerTyp.Ba:
		case AbenteuerTyp.Tm:
			mChar.pA=Mathf.Max (minPA, mChar.pA);
			break;
		default:
			break;
		}
	}

	/// <summary>
	/// Computes the Willenskraft of the Charakter
	/// </summary>
	/// <param name="mChar">M char.</param>
	static void ComputeWkSb (MidgardCharakter mChar)
	{

		mChar.Wk = Mathf.Min (UnityEngine.Random.Range (1, 101) + 3 * (Mathf.FloorToInt (mChar.Ko / 10 + mChar.In / 10)) - 40, 100);

		switch (mChar.Archetyp) {
		case AbenteuerTyp.Gl:
		case AbenteuerTyp.Sp:
			mChar.Sb = Mathf.Min (UnityEngine.Random.Range (1, 101) + 3 * (Mathf.FloorToInt (mChar.In / 10 + mChar.Wk / 10)) - 50, 100);
			break;
		case AbenteuerTyp.As:
		case AbenteuerTyp.Be:
		case AbenteuerTyp.Dr:
			mChar.Sb = Mathf.Min (UnityEngine.Random.Range (1, 101) + 3 * (Mathf.FloorToInt (mChar.In / 10 + mChar.Wk / 10)), 100);
			break;
		default:
			mChar.Sb = Mathf.Min (UnityEngine.Random.Range (1, 101) + 3 * (Mathf.FloorToInt (mChar.In / 10 + mChar.Wk / 10)) - 30, 100);
			break;
		}
	}

	#endregion


	#region APLP

	/// <summary>
	/// Computes the APLP quick.
	/// </summary>
	/// <param name="mChar">M char.</param>

	public static void ComputeAPLP (MidgardCharakter mChar)
	{
		const int APMIN = 4;

		mChar.AP = UnityEngine.Random.Range (1, 7) + 3 + mChar.AusB;

		if (mChar.Archetyp == AbenteuerTyp.BN || mChar.Archetyp == AbenteuerTyp.BS || mChar.Archetyp == AbenteuerTyp.BW || mChar.Archetyp == AbenteuerTyp.Kr ||
		   mChar.Archetyp == AbenteuerTyp.Soe || mChar.Archetyp == AbenteuerTyp.Wa) {
			mChar.AP += 1;
		} else if (mChar.Archetyp == AbenteuerTyp.Be || mChar.Archetyp == AbenteuerTyp.Dr || mChar.Archetyp == AbenteuerTyp.Hl || mChar.Archetyp == AbenteuerTyp.Hx ||
		           mChar.Archetyp == AbenteuerTyp.Ma || mChar.Archetyp == AbenteuerTyp.PF || mChar.Archetyp == AbenteuerTyp.PHa || mChar.Archetyp == AbenteuerTyp.PHe ||
		           mChar.Archetyp == AbenteuerTyp.PK || mChar.Archetyp == AbenteuerTyp.PM || mChar.Archetyp == AbenteuerTyp.PT || mChar.Archetyp == AbenteuerTyp.PW
		           || mChar.Archetyp == AbenteuerTyp.Th) {
			mChar.AP -= 1;
		}

		mChar.LP = Mathf.FloorToInt (mChar.Ko / 10) + UnityEngine.Random.Range (1, 7) + 4;
		switch (mChar.Spezies) {
		case Races.Elf:
			mChar.LP += 1;
			break;
		case Races.Halbling:
			mChar.AP -= 1;
			mChar.LP -= 2;
			break;
		case Races.Zwerg:
			mChar.LP += 1;
			break;
		case Races.Berggnom:
		case Races.Waldgnom:
			mChar.AP -= 2;
			mChar.LP -= 4;
			break;
		default:
			break;
		}

		//Mindestens 4 AP!
		mChar.AP = Mathf.Max (mChar.AP, APMIN);
	}

	#endregion

	#region Natur1

	/// <summary>
	/// Computes the natur gegeben I quick.
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static void ComputeNaturGegebenI (MidgardCharakter mChar)
	{
		//Angriff
		if (mChar.Gs <= 5) {
			mChar.AnB = -2;
		} else if (mChar.Gs > 5 && mChar.Gs <= 20) {
			mChar.AnB = -1;
		} else if (mChar.Gs > 20 && mChar.Gs <= 80) {
			mChar.AnB = 0;
		} else if (mChar.Gs > 80 && mChar.Gs <= 95) {
			mChar.AnB = 1;
		} else {
			mChar.AnB = 2;
		}

		//Raufen
		mChar.Raufen = Mathf.FloorToInt ((mChar.St + mChar.Gw) / 20) + mChar.AnB;

		if (mChar.Spezies == Races.Zwerg) {
			mChar.Raufen += 1;
		}

		//Abwehr
		mChar.Abwehr = 11;

		if (mChar.Gw <= 5) {
			mChar.AbB = -2;
		} else if (mChar.Gw > 5 && mChar.Gw <= 20) {
			mChar.AbB = -1;
		} else if (mChar.Gw > 20 && mChar.Gw <= 80) {
			mChar.AbB = 0;
		} else if (mChar.Gw > 80 && mChar.Gw <= 95) {
			mChar.AbB = 1;
		} else {
			mChar.AbB = 2;
		}

		mChar.Abwehr = mChar.Abwehr + mChar.AbB;

		//Zaubern
		mChar.Zaubern = 2;
		mChar.ZauB = 0;

		if (mChar.Archetyp == AbenteuerTyp.Be || mChar.Archetyp == AbenteuerTyp.Dr || mChar.Archetyp == AbenteuerTyp.Hl || mChar.Archetyp == AbenteuerTyp.Hx ||
		    mChar.Archetyp == AbenteuerTyp.Ma || mChar.Archetyp == AbenteuerTyp.PF || mChar.Archetyp == AbenteuerTyp.PHa || mChar.Archetyp == AbenteuerTyp.PHe ||
		    mChar.Archetyp == AbenteuerTyp.PK || mChar.Archetyp == AbenteuerTyp.PM || mChar.Archetyp == AbenteuerTyp.PT || mChar.Archetyp == AbenteuerTyp.PW
		    || mChar.Archetyp == AbenteuerTyp.Th || mChar.Archetyp == AbenteuerTyp.Sc || mChar.Archetyp == AbenteuerTyp.Or || mChar.Archetyp == AbenteuerTyp.Ba ||
		    mChar.Archetyp == AbenteuerTyp.Tm) {
			mChar.Zaubern += 8;

			if (mChar.Zt <= 5) {
				mChar.ZauB = -3;
			} else if (mChar.Zt > 5 && mChar.Zt <= 20) {
				mChar.ZauB = -2;
			} else if (mChar.Zt > 20 && mChar.Zt <= 40) {
				mChar.ZauB = -1;
			} else if (mChar.Zt > 40 && mChar.Zt <= 60) {
				mChar.ZauB = 0;
			} else if (mChar.Zt > 60 && mChar.Zt <= 80) {
				mChar.ZauB = 1;
			} else if (mChar.Zt > 80 && mChar.Zt <= 95) {
				mChar.ZauB = 2;
			} else if (mChar.Zt > 95 && mChar.Zt <= 99) {
				mChar.ZauB = 3;
			} else {
				mChar.ZauB = 4;
			}
		}

		mChar.Zaubern = mChar.Zaubern + mChar.ZauB;
	}

	#endregion

	#region Resistenzen

	/// <summary>
	/// Computes the resistenzen.
	/// </summary>
	/// <param name="m_Char">M char.</param>
	public static void ComputeResistenzen (MidgardCharakter mChar)
	{
		mChar.resPsy = 10;
		mChar.resPhk = 10;
		mChar.resPhy = 10;

		//Hole die Kategorie für den Abenteurer:
		int aTypId = (int) mChar.Archetyp + 1;
		int katId = GetKategorieForAbenteurerTyp (aTypId);

		//Kämpfer geringeren Bonus als Zauber, zauberkundige Kämpfer
		switch (katId) {
		case 1:
		case 3:
			mChar.resPsy += 3;
			mChar.resPhy += 3;
			mChar.resPhk += 3;
			break;
		case 2:
			mChar.resPhk += 2;
			break;
		default:
			break;
		}

		//Basisresistenzen werden erweitert:
		switch (mChar.Spezies) {
		case Races.Elf:
			mChar.resPsy += 2;
			mChar.resPhy += 2;
			mChar.resPhk += 2;
			if (mChar.Zt == 100) {
				mChar.resPsy += 1;
				mChar.resPhy += 1;
			}
			break;
		case Races.Halbling:
			mChar.resPsy += 5;
			mChar.resPhy += 5;
			mChar.resPhk += 5; 
			break;
		case Races.Zwerg:
			mChar.resPsy += 4;
			mChar.resPhy += 4;
			break;
		case Races.Berggnom:
		case Races.Waldgnom:
			mChar.resPsy += 5;
			mChar.resPhy += 5;
			mChar.resPhk += 5; 
			break;
		default:
			//Falls Mensch
			mChar.resPsy += GetResistenzBonusMenschEigenschaft (mChar.Zt, mChar.In);
			mChar.resPhy += GetResistenzBonusMenschEigenschaft (mChar.Zt, mChar.Ko);
			mChar.resPhk += Mathf.Min (getResistenzBonusFromEigenschaft (mChar.Gw), 2); //hier maximal 2!
			break;
		}
	}




	/// <summary>
	/// Gets the resistenz bonus mensch eigenschaft.
	/// </summary>
	/// <param name="E1">E1.</param>
	/// <param name="E2">E2.</param>
	private static int GetResistenzBonusMenschEigenschaft (int E1, int E2)
	{
		int resE1 = getResistenzBonusFromEigenschaft (E1);
		int resE2 = getResistenzBonusFromEigenschaft (E2);
		int signUp = resE1 * resE2;
		int resBonus = 0;

		if (signUp <= 0) {
			resBonus = resE1 + resE2;
		} else {
				resBonus = Mathf.Max (resE1, resE2);
		}

		return resBonus;
	}


	/// <summary>
	/// Gets the resistenz bonus from eigenschaft.
	/// </summary>
	/// <returns>The resistenz bonus from eigenschaft.</returns>
	/// <param name="Eigenschaft">Eigenschaft.</param>
	private static int getResistenzBonusFromEigenschaft (int Eigenschaft)
	{
		int returnBonus=0;
		if (Eigenschaft < 6) {
			returnBonus = -2;
		} else if (Eigenschaft >= 6 && Eigenschaft < 21) {
			returnBonus = -1;
		} else if (Eigenschaft >= 21 && Eigenschaft < 81) {
			returnBonus = 0;
		} else if (Eigenschaft >= 81 && Eigenschaft < 96) {
			returnBonus = 1;
		} else if (Eigenschaft >= 96 && Eigenschaft < 101) {
			returnBonus = 2;
		}

		return returnBonus;
	}

	#endregion

	#region Angeborene Fertigkeiten

	/// <summary>
	/// Prepares the angeborene fertigkeiten mit den Standardwerten.
	/// </summary>
	public static void PrepareAngeboreneFertigkeiten(MidgardCharakter mChar)
	{
		//Hole die angeborenen Eigenschaften
		List<Sinn> sinne = MidgardResourceReader.GetMidgardResource<AngeboreneFertigkeiten>(MidgardResourceReader.MidgardAngeboren).listSinne;
		mChar.listSinne = sinne;

	}

	/// <summary>
	/// Modifies the angeborene fertigkeiten durch Würfelwurf
	/// </summary>
	/// <param name="mChar">M char.</param>
	public static void ModifyAngeboreneFertigkeiten(MidgardCharakter mChar, int w100){
		List<Sinn> sinne = MidgardResourceReader.GetMidgardResource<AngeboreneFertigkeiten>(MidgardResourceReader.MidgardAngeboren).listSinne;
		mChar.listSinne = sinne;
		if (w100 <= 2) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Sehen, 4);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 2 && w100 <= 4) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Hoeren, 4);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 4 && w100 <= 6) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Riechen, 4);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 6 && w100 <= 8) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Schmecken, 4);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 8 && w100 <= 10) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Tasten, 4);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 10 && w100 <= 20) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Sehen, 10);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 20 && w100 <= 30) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Hoeren, 10);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 30 && w100 <= 40) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Riechen, 10);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 40 && w100 <= 50) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Schmecken, 10);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 50 && w100 <= 60) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.Tasten, 10);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 60 && w100 <= 65) {
			Sinn sinn = ModifyCharakterSinnNatur (Senses.SechsterSinn, 6);
			Sinn mSinn = ObjectXMLHelper.GetMidgardObjectById (mChar.listSinne, sinn.id);
			mSinn.value = sinn.value;
		} else if (w100 > 65 && w100 <= 70) {
			AngeboreneFertigkeit angFert = ModifyCharakterSinnNatur(Natur.Berserkergang, Mathf.FloorToInt(18-mChar.Wk/5));
			mChar.listAngeboren.Add (angFert);
		} else if (w100 > 70 && w100 <= 75) {
			AngeboreneFertigkeit angFert =ModifyCharakterSinnNatur (Natur.GuteReflexe, 9);
			mChar.listAngeboren.Add (angFert);
		} else if (w100 > 75 && w100 <= 80) {
			AngeboreneFertigkeit angFert =ModifyCharakterSinnNatur (Natur.Nachtsicht, 10);
			mChar.listAngeboren.Add (angFert);
		} else if (w100 > 80 && w100 <= 85) {
			AngeboreneFertigkeit angFert =ModifyCharakterSinnNatur (Natur.Richtungssinn, 12);
			mChar.listAngeboren.Add (angFert);
		} else if (w100 > 85 && w100 <= 90) {
			AngeboreneFertigkeit angFert =ModifyCharakterSinnNatur (Natur.Robustheit, 9);
			mChar.listAngeboren.Add (angFert);
		} else if (w100 > 90 && w100 <= 95) {
			AngeboreneFertigkeit angFert =ModifyCharakterSinnNatur (Natur.Wachgabe, 9);
			mChar.listAngeboren.Add (angFert);
		} else if (w100 > 95 && w100 <= 99) {
			AngeboreneFertigkeit angFert =ModifyCharakterSinnNatur (Natur.Einpraegen, 4);
			mChar.listAngeboren.Add (angFert);
		} 
	}



	private static Sinn ModifyCharakterSinnNatur(Senses chosenSinn, int modValue){
		int sinnId = (int)chosenSinn + 1;
		Sinn charSinn = GetSinnAngeborenById<Sinn> (sinnId);
		charSinn.value = modValue; //Kurzsichtig
		return charSinn;

	}

	private static AngeboreneFertigkeit ModifyCharakterSinnNatur(Natur chosenFertigkeit, int modValue){
		int naturId = (int)chosenFertigkeit + 1;
		AngeboreneFertigkeit charNatur = GetSinnAngeborenById<AngeboreneFertigkeit> (naturId);
		charNatur.value = modValue; //Kurzsichtig
		return charNatur;
	}

	#endregion


	#region Eigenschaften automatisch

	//Alle weiteren Eigenschaften
	public static void ComputeEigenschaften (MidgardCharakter mChar)
	{

		//Achtung: BasisEigenschaften werden durch UI bestimmt.

		//1. AbgeleiteteEigenschaften I
		ComputeAbgeleiteteEigenschaften (mChar);

		//2. pA, Sb, Wk
		ComputePsychisch (mChar);

		//3. AP und LP
		ComputeAPLP (mChar);

		//4. AnB, AbB, Raufen, Zaubern, ZauB
		ComputeNaturGegebenI (mChar);

		//5. Resistenzen
		ComputeResistenzen (mChar);
      
	}

	#endregion

}


/// <summary>
/// Charakter bogen: Berechnet aus den gegeben werten den bogen
/// </summary>
public class CharakterBogen : MonoBehaviour
{

	//Enthält alle relevanten werte für den charakter
	public static MidgardCharakter Character;

	public static void BerechneAbgeleiteteEigenchaften ()
	{
		CharacterEngine.ComputeEigenschaften (Character);
	}
}
