using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class MidgardCharakter
{
	#region Beschreibung
	public Geschlecht Sex;
	public Races Spezies;
	public AbenteuerTyp Archetyp;
	public int Grad;
	public Stand Schicht;
	public string Glaube;
	public Herkunft Land;
	public int Alter;
	public string Gestalt;
	public int Gewicht;
	public int Groesse;
	public Berufe Beruf;
	public int Aussehen;
	#endregion

	#region Lebens- Ausdauerpunkte
	public int LPMax, AP, LP;
	#endregion


	#region Basiseigenschaften
	public int St, Gs, Gw, Ko, In, Zt;
	#endregion

	#region Abgel. Eigenschaften I
	public int pA, Sb, Wk;
    #endregion

    #region Händigkeit
    public Hand hand;
    #endregion

    #region Boni I
    public int SchB, AusB, B;
	#endregion

	#region Kamfboni
	public int AnB, AbB, ZauB, Raufen, Abwehr, Zaubern;
	#endregion

	#region Resistenzen
	public int resPhy, resPsy, resPhk;
	#endregion


	#region AngeboreneFertigkeiten
	public List<Sinn> listSinne = new List<Sinn>();
	public List<AngeboreneFertigkeit> listAngeboren = new List<AngeboreneFertigkeit>();
	#endregion

	#region Fertigkeiten, Waffen, Zauber
	public List<InventoryItem> fertigkeiten = new List<InventoryItem> ();
	public List<InventoryItem> waffenFertigkeiten = new List<InventoryItem> ();
	public List<InventoryItem> zauberFormeln = new List<InventoryItem> ();
	public List<InventoryItem> zauberLieder = new List<InventoryItem> ();
	public List<InventoryItem> zauberSalze = new List<InventoryItem> ();
	#endregion


	#region Herkunft
	public StadtLandFluss StadtLand;
	#endregion

	#region Berufe


	#endregion

	public override string ToString()
	{
		return "St:" + St + " Gs:" + Gs + " Gw:" + Gw + " Ko:" + Ko + " In:" + In + " Zt:" + Zt + " pA:" + pA + " Sb:" + Sb + " Wk:" + Wk + " SchB:" + SchB +
			" AusB:" + AusB + " B:" + B + " AnB:" + AnB + " AbB:" + AbB + " ZauB:" + ZauB + " Raufen:" + Raufen + " Abwehr:" + Abwehr + " Zaubern:" + Zaubern +
			" LP:" + LP + " AP:" + AP + " Aussehen:" + Aussehen + " Gewicht:" + Gewicht + " Groesse:" + Groesse + " Händigkeit: " + hand.ToString();
	}
}

