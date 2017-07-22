using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LernplanModify {

	private Toolbox instance;
	private Lernplan lernPlan;
	public MidgardCharacterHelper mCharacterHelper { get; set;}

	public LernplanModify(){
		instance = Toolbox.Instance;
		mCharacterHelper = instance.mCharacterHelper; 
		lernPlan = mCharacterHelper.GetLernplanForCharacter ();
	}


	#region Fachkenntnisse
	/// <summary>
	/// Modifies the lern plan for race.
	/// Verweis: Midgard, DFR, S.38
	///  Elf: Fachkenntnisse zu entfernen: 
	/// 	Ballista, id=5
	/// 	Bogenkampf Pferd, id=10
	/// 	Fechten, id=19
	/// 	Gassenwissen, id=21
	/// 	Geschäftstüchtig, id=26
	/// 	Giftmischer, id=27
	/// 	Glücksspiel, id=28
	/// 	Kampf in Voll, id=33
	/// 	Katapult bed., id=37
	/// 	Meucheln, id=46
	/// 	Schlösser, id=62
	/// 	Stehlen, id=72
	/// 	Trinken, id=82
	/// 	Überleben außer Wald, id= 84,85
	/// 
	/// 	Hinzufügen für 1 LP -> müssen daher auch erst entfernt werden
	/// 	Schleichen, id=60
	/// 	Spurenlesen, id=71
	/// 	Tarnen, id=78
	/// 	Wahrnehmung, id=91
	/// 
	/// 
	/// 	Überlebensfertigkeiten bei Elfen darf nur Wald sein, muss daher passend getauscht werden
	/// </summary>
	public void ModifyFachKenntnisseForRace(){
		MidgardCharakter mCharacter = mCharacterHelper.mCharacter;
		if (mCharacter.Spezies == Races.Elf) {
			int[] deleteFachFromLernplan = { 5, 10, 19, 21, 26, 27, 28, 33, 37, 46, 62, 72, 82, 84, 85 };//s.o.
			int[] addFachFromLernplan = { 60, 71, 78, 91 }; //s.o.

			//Lösche die Fertigkeiten vom Plan auch diejenigen, die neu hinzu sollen		
			DeleteFachkenntnisFromLernPlan (deleteFachFromLernplan);
			DeleteFachkenntnisFromLernPlan (addFachFromLernplan);

			//Erzeuge Lernplan-Elemente
			FachkenntnisRef fachSchleichen = new FachkenntnisRef ();
			FachkenntnisRef fachSpuren = new FachkenntnisRef ();
			FachkenntnisRef fachTarnen = new FachkenntnisRef ();
			FachkenntnisRef fachWahrnehumg = new FachkenntnisRef ();

			SetFertigkeitValues (fachSchleichen, 60, "8", 1);
			SetFertigkeitValues (fachSpuren, 71, "6", 1);
			SetFertigkeitValues (fachTarnen, 78, "8", 1);
			SetFertigkeitValues (fachWahrnehumg, 91, "4", 1);

			lernPlan.fachkenntnisse.Add (fachSchleichen);
			lernPlan.fachkenntnisse.Add (fachSpuren);
			lernPlan.fachkenntnisse.Add (fachTarnen);
			lernPlan.fachkenntnisse.Add (fachWahrnehumg);

		} else if (mCharacter.Spezies == Races.Halbling) {

			int[] deleteFachFromLernplan = { 83, 84, 85 };
			int[] addFachFromLernplan = { 60, 78 }; //s.o.

			DeleteFachkenntnisFromLernPlan (deleteFachFromLernplan);
			DeleteFachkenntnisFromLernPlan (addFachFromLernplan);

			//Erzeuge Lernplan-Elemente
			FachkenntnisRef fachSchleichen = new FachkenntnisRef ();
			FachkenntnisRef fachTarnen = new FachkenntnisRef ();

			SetFertigkeitValues (fachSchleichen, 60, "10", 1);
			SetFertigkeitValues (fachTarnen, 78, "10", 1);

			lernPlan.fachkenntnisse.Add (fachSchleichen);
			lernPlan.fachkenntnisse.Add (fachTarnen);

		} else if (mCharacter.Spezies == Races.Zwerg) {
			int[] deleteFachFromLernplan = { 83, 85 };
			int[] addFachFromLernplan = { 6, 82 }; //s.o.

			DeleteFachkenntnisFromLernPlan (deleteFachFromLernplan);
			DeleteFachkenntnisFromLernPlan (addFachFromLernplan);

			//Erzeuge Lernplan-Elemente
			FachkenntnisRef fachBaukunde = new FachkenntnisRef ();
			FachkenntnisRef fachTrinken = new FachkenntnisRef ();

			SetFertigkeitValues (fachBaukunde, 6, "8", 1);
			SetFertigkeitValues (fachTrinken, 82, "12", 1);

			lernPlan.fachkenntnisse.Add (fachBaukunde);
			lernPlan.fachkenntnisse.Add (fachTrinken);

		} else if (mCharacter.Spezies == Races.Berggnom) {
			int[] deleteFachFromLernplan = { 83, 85 };
			int[] addFachFromLernplan = {23,15 }; //s.o.

			DeleteFachkenntnisFromLernPlan(deleteFachFromLernplan);
			DeleteFachkenntnisFromLernPlan (addFachFromLernplan);

			//Erzeuge Lernplan-Elemente
			FachkenntnisRef fachGeheimmechanismen = new FachkenntnisRef ();
			FachkenntnisRef fachFallenentdecken = new FachkenntnisRef ();

			SetFertigkeitValues (fachGeheimmechanismen, 23, "6", 1);
			SetFertigkeitValues (fachFallenentdecken, 15, "6", 1);

			lernPlan.fachkenntnisse.Add (fachGeheimmechanismen);
			lernPlan.fachkenntnisse.Add (fachFallenentdecken);

		} else if (mCharacter.Spezies == Races.Waldgnom) {
			int[] deleteFachFromLernplan = { 84, 85 };
			int[] addFachFromLernplan = {17,71,78 }; //s.o.

			DeleteFachkenntnisFromLernPlan(deleteFachFromLernplan);
			DeleteFachkenntnisFromLernPlan (addFachFromLernplan);

			//Erzeuge Lernplan-Elemente
			FachkenntnisRef fachFallenstellen = new FachkenntnisRef ();
			FachkenntnisRef fachSpurenlesen = new FachkenntnisRef ();
			FachkenntnisRef fachTarnen = new FachkenntnisRef ();

			SetFertigkeitValues (fachFallenstellen, 17, "4", 1);
			SetFertigkeitValues (fachSpurenlesen, 71, "6", 1);
			SetFertigkeitValues (fachTarnen, 78, "8", 1);

			lernPlan.fachkenntnisse.Add (fachFallenstellen);
			lernPlan.fachkenntnisse.Add (fachSpurenlesen);
			lernPlan.fachkenntnisse.Add (fachTarnen);
		}
	}


	/// <summary>
	/// Löscht die Fachkenntnisse mit angegeben Indizes aus dem Lernplan
	/// </summary>
	/// <param name="indizes">Indizes.</param>
	public void DeleteFachkenntnisFromLernPlan(int[] indizes){
		foreach (var index in indizes) {
			lernPlan.fachkenntnisse.RemoveAll(x => x.id == index );
		}
	}
	#endregion


	#region waffen

	/// <summary>
	/// Modifies the waffen for race.
	/// 
	/// Elfen nutzen zu Beginn auschließlich folgende Waffen {1,2,7,13,14,15,21,23,24,27,58,57,40,42,43,44,45,47,48,51,4,20,22,35,11,16}:
	/// 	Anderthalbhänder, id=1
	/// 	Bihänder, id=2
	/// 	Dolch, id=7,
	/// 	Handaxt, id=13
	/// 	Kampfstab, id=14
	/// 	Keule, id=15
	/// 	Kurzschwert, id=21
	/// 	Langschwert, id=23
	/// 	Lanze, id=24
	/// 	leichter Speer, id=27
	/// 	Magierstab,id=58
	/// 	Magierstecken, id=57
	/// 	Stoßspeer,id=40
	/// 	Streitkolben,id=42
	/// 	waffenloser Kampf, id=43
	/// 	Werfen, id=44
	/// 	Wurfaxt, id=45
	/// 	Wurfkeule, id=47
	/// 	Wurfmesser, id=48
	/// 	Wurspeer, id=51
	/// 	Bogen, id=4
	/// 	Kurzbogen, id=20
	/// 	Langbogen, id=22
	/// 	Schleuder, id=35
	/// 	großer Schild, id=11
	/// 	kleiner Schild, id=16
	/// 
	/// Gnome: {7,13,15,21,27,58,40,42,43,44,45,47,48,49,51,26,3,4,20,35,6,16}
	/// 	Dolch, id=7
	/// 	Handaxt,id=13
	/// 	Keule,id=15
	/// 	Kurzschwert, id=21
	/// 	leichter Speer, id=27
	/// 	Magierstab, id=58
	/// 	Stoßspeer, id=40
	/// 	Streitkolben, id=42
	/// 	waffenloser Kampf, id=43
	/// 	Werfen, id=44
	/// 	Wurfaxt,id=45
	/// 	Wurfkeule, id=47
	/// 	Wurfmesser, id=48
	/// 	Wurfpfeil, id=49
	/// 	Wurfspeer, id=51
	/// 	leichte Armbrust, id=26
	/// 	Blasrohr, id=3
	/// 	Bogen, id=4
	/// 	Kurzbogen,id=20
	/// 	Schleuder,id=35
	/// 	Buckler, id=6
	/// 	kleiner Schild, id=16
	/// 
	/// Halblinge: {7,13,15,18,21,23,27,58,40,41,43,44,45,47,51,4,20,35,16}
	/// 	Dolch, id=7
	/// 	Handaxt, id=13
	/// 	Keule, id=15
	/// 	Kriegshammer,id=18
	/// 	Kurzschwert, id=21
	/// 	Langschwert,id=23
	/// 	leichter Speer,id=27
	/// 	Magierstab, id=58
	/// 	Stoßspeer,id=40
	/// 	Streitaxt, id=41
	/// 	waffenloser Kampf,id=43
	/// 	Werfen, id=44
	/// 	Wurfaxt, id=45
	/// 	Wurfkeule, id=47
	/// 	Wurfmesser, id=48
	/// 	Wurfspeer, id=51
	/// 	Bogen, id=4
	/// 	Kurzbogen, id=20
	/// 	Schleuder, id=35
	/// 	kleiner Schild, id=16
	/// 
	/// Zwerge: {1,7,8,12,13,15,54,18,21,23,24,58,28,32,34,38,40,41,39,42,43,44,45,46,47,26,36,3,35,11,16}
	/// 	Anderhalbhänder, id=1
	/// 	Dolch, id=7
	/// 	Faustkampf, id=8
	/// 	Handaxt, id=13
	/// 	Hellebarde, id=12
	/// 	Keule,id=15
	/// 	Kriegsflegel, id=54
	/// 	Kriegshammer, id=18
	/// 	Kurzschwert,id=21
	/// 	Langschwert,id=23
	/// 	Lanze,id=24
	/// 	Magierstab, id=58
	/// 	Morgenstern, id=28
	/// 	Peitsche, id=32
	/// 	Schlachtbeil, id=34
	/// 	Stabkeule,id=38
	/// 	Stoßspeer,id=40
	/// 	Streitaxt,id=41
	/// 	Stielhammer, id=39
	/// 	Streitkolben, id=42
	/// 	waffenloser Kampf, id=43
	/// 	Werfen, id=44 
	/// 	Wurfaxt,id=45
	/// 	Wurfhammer, id=46
	/// 	Wurfkeule, id=47
	/// 	leichte Armbrust,id=26
	/// 	schwere Armbrust, id=36
	/// 	Blasrohr, id=3
	/// 	Schleuder, id=35
	/// 	großer Schild,id=11
	/// 	kleiner Schild, id=16
	/// 
	/// </summary>
	public void ModifyWaffenForRace(){
		MidgardCharakter mCharacter = mCharacterHelper.mCharacter;

		if (mCharacter.Spezies == Races.Elf) {
			int[] deleteWaffenIds = { 1, 2, 7, 13, 14, 15, 21, 23, 24, 27, 58, 57, 40, 42, 43, 44, 45, 47, 48, 51, 4, 20, 22, 35, 11, 16 };
			DeleteWaffenFromLernplan (deleteWaffenIds);
		} else if(mCharacter.Spezies == Races.Halbling) {
			int[] deleteWaffenIds = { 7, 13, 15, 18, 21, 23, 27, 58, 40, 41, 43, 44, 45, 47, 51, 4, 20, 35, 16 };
			DeleteWaffenFromLernplan (deleteWaffenIds);
		} else if(mCharacter.Spezies == Races.Zwerg) {
			int[] deleteWaffenIds = { 1, 7, 8, 12, 13, 15, 54, 18, 21, 23, 24, 58, 28, 32, 34, 38, 40, 41, 39, 42, 43, 44, 45, 46, 47, 26, 36, 3, 35, 11, 16 };
			DeleteWaffenFromLernplan (deleteWaffenIds);
		} else if(mCharacter.Spezies == Races.Berggnom || mCharacter.Spezies == Races.Waldgnom) {
			int[] deleteWaffenIds = { 7, 13, 15, 21, 27, 58, 40, 42, 43, 44, 45, 47, 48, 49, 51, 26, 3, 4, 20, 35, 6, 16 };
			DeleteWaffenFromLernplan (deleteWaffenIds);
		}
	}

	/// <summary>
	/// Deletes the waffen from lernplan.
	/// </summary>
	private void DeleteWaffenFromLernplan(int[] indizes){
		List<WaffenfertigkeitRef> waffenFertigkeitenRef = lernPlan.waffenfertigkeiten;
		foreach (var waffeRef in waffenFertigkeitenRef.ToArray()) {
			if (!indizes.Contains (waffeRef.id)) {
				waffenFertigkeitenRef.Remove (waffeRef);
			}
		}
	}
	#endregion

	#region AllgemeinWissen, Sprache
	/// <summary>
	/// Modifies the sprache allgemein wissen. id=63: Schreiben, id=69= Sprechen
	/// </summary>
	/// <param name="fachkenntnisse">Fachkenntnisse.</param>
	/// <param name="item">Item.</param>
	/// <param name="returnListFach">Return list fach.</param>
	public void ModifySpracheAllgemeinWissen(List<FachkenntnisRefAllgemein> fachkenntnisse, InventoryItem item, List<InventoryItem> returnListFach ){
		MidgardCharakter mCharacter = mCharacterHelper.mCharacter;
		if (item.id == 63 || item.id == 69) {
			FachkenntnisRefAllgemein fachRef = fachkenntnisse.Where (e => e.id == item.id).ToList()[0];
			fachkenntnisse.Remove (fachRef);
			if (fachRef.sprache != null) {
				item.name += "-" + fachRef.sprache;
				if (item.id == 69 && fachRef.variabel == true) {//Sprechen: Hier sind die Item-Wert je nach Intelligenzgrad unterschiedlich
					//Sprachwertanpassung entsprechend Intelligenz
					if (fachRef.sprache.Contains ("Mutter")) {
						if (mCharacter.In >= 1 && mCharacter.In < 31) {
							item.val = "10";
						} else if (mCharacter.In >= 31 && mCharacter.In < 61) {
							item.val = "14";
						} else if (mCharacter.In >= 61) {
							item.val = "18";
						}
					} else {
						if (mCharacter.In >= 1 && mCharacter.In < 31) {
							item.val = "9";
						} else if (mCharacter.In >= 31 && mCharacter.In < 61) {
							item.val = "12";
						} else if (mCharacter.In >= 61) {
							item.val = "12";
						}
					}

				} else if (item.id == 63 && fachRef.variabel == true) {//Schreiben: Hier sind die Item-Werte je nach Intelligenzgrad unterschiedlich
					//Sprachwertanpassung entsprechend Intelligenz
					if (mCharacter.In >= 21 && mCharacter.In < 61) {
						item.val = "9";
					} else if (mCharacter.In >= 61) {
						item.val = "12";
					}
				} else if (item.id == 69) { //Sprechen: Lösche Sprach items bei zu geringer intelligenz: Sprechen
					if (item.cost == 3 && mCharacter.In < 31) {
						returnListFach.Remove (item);
					}
				} else if (item.id == 63) {//Schreiben: Lösche Sprache bei zu geringer INtelligenz: Schreiben
					if ((item.cost == 1 || item.cost==2) && mCharacter.In < 21) {
						returnListFach.Remove (item);
					} else if (item.cost == 3 && mCharacter.In < 61) {
						returnListFach.Remove (item);
					}
				}
			}
		}
	}
	#endregion


	#region ungewöhnliche Fertikgeiten
	public void ModifySpracheUngewoehnlicheFertigkeiten(List<FachkenntnisRefAllgemein> fachkenntnisse, InventoryItem item, List<InventoryItem> returnListFach ){
		MidgardCharakter mCharacter = mCharacterHelper.mCharacter;
		if (item.id == 63 || item.id == 69) {
			FachkenntnisRefAllgemein fachRef = fachkenntnisse.Where (e => e.id == item.id).ToList()[0];
			fachkenntnisse.Remove (fachRef);
			if (fachRef.sprache != null) {
				item.name += "-" + fachRef.sprache;
				if (item.id == 69) { //Sprechen: Lösche Sprach items bei zu geringer intelligenz: Sprechen
					if (item.cost == 3 && mCharacter.In < 31) {
						returnListFach.Remove (item);
					}
				} else if (item.id == 63) {//Schreiben: Lösche Sprache bei zu geringer INtelligenz: Schreiben
					if ((item.cost == 1 || item.cost==2) && mCharacter.In < 21) {
						returnListFach.Remove (item);
					} else if (item.cost == 3 && mCharacter.In < 61) {
						returnListFach.Remove (item);
					}
				}
			}
		}
	}


	#endregion


	#region Berufe
	/// <summary>
	/// Modifies the berufe from wurf.
	/// Reduziert die Fachkenntnisse der Berufe entsprechend dem Wurf
	/// </summary>
	/// <param name="berufname">Berufname.</param>
	/// <param name="wuerfelWurf">Wuerfel wurf.</param>
	/// <param name="berufskenntnisse">Berufskenntnisse.</param>
	public void ModifyBerufeFromWurf(int wuerfelWurf, List<Beruf> wahlFach){
		foreach (var item in wahlFach.ToArray()) {
			int itemKategorie = Convert.ToInt32 (item.kategorie);
			if (wuerfelWurf <= 20) {
				wahlFach.Remove (item);
			}
			if (wuerfelWurf >20 && wuerfelWurf<=50) {
				if (itemKategorie > 1) {
					wahlFach.Remove (item);
				}
			} else if(wuerfelWurf > 50 && wuerfelWurf<=80) {
				if (itemKategorie > 2) {
					wahlFach.Remove (item);
				}
			} else if(wuerfelWurf > 80 && wuerfelWurf<=95) {
				if (itemKategorie > 3) {
					wahlFach.Remove (item);
				}
			} 
		}
	}
	#endregion

	#region hilfsmethoden

	/// <summary>
	/// Creates a FertigkeitRef (bedeutet eine Waffe, bzw. Fachkenntnis)
	/// </summary>
	/// <returns>The inventory item.</returns>
	/// <param name="name">Name.</param>
	/// <param name="val">Value.</param>
	/// <param name="cost">Cost.</param>
	private ILernplan SetFertigkeitValues (ILernplan item, int id, string val, int cost)
	{
		item.id = id;
		item.val = val;
		item.cost = cost;
		return item;
	}

	#endregion

}
