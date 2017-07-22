using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class Toolbox : Singleton<Toolbox>
{
    protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!

    //Globale Variablen hier rein!
    public MidgardCharakter mCharacter = new MidgardCharakter();
	public MidgardCharacterHelper mCharacterHelper;
	public LernPlanHelper lernHelper;


	/// <summary>
	/// The midgard fertigkeiten. Aus XML-Datei
	/// </summary>
	private Fertigkeiten midgardFertigkeiten;
	public Fertigkeiten MidgardFertigkeiten{
		get {
			if (midgardFertigkeiten == null) {
				midgardFertigkeiten = MidgardResourceReader.GetMidgardResource<Fertigkeiten> (MidgardResourceReader.MidgardFertigkeiten);
			}
			return midgardFertigkeiten;
		}
	}

	/// <summary>
	/// The midgard waffen fertigkeiten.Aus XML-Datei
	/// </summary>
	private Waffenfertigkeiten midgardWaffenFertigkeiten;
	public Waffenfertigkeiten MidgardWaffenFertigkeiten {
		get {
			if (midgardWaffenFertigkeiten == null) {
				midgardWaffenFertigkeiten = MidgardResourceReader.GetMidgardResource<Waffenfertigkeiten> (MidgardResourceReader.MidgardWaffenFertigkeiten);
			}
			return midgardWaffenFertigkeiten;
		}
	}


	/// <summary>
	/// The midgard zauber.Aus XML-Datei
	/// </summary>
	private Zauberformeln midgardZauber;
	public Zauberformeln MidgardZauberformeln {
		get {
			if (midgardZauber == null) {
				midgardZauber = MidgardResourceReader.GetMidgardResource<Zauberformeln> (MidgardResourceReader.MidgardZauber);
			}
			return midgardZauber;
		}
	}


	/// <summary>
	/// The midgard zauberlieder. Aus XML-Datei
	/// </summary>
	private Zauberlieder midgardZauberlieder;
	public Zauberlieder MidgardZauberlieder {
		get {
			if (midgardZauberlieder == null) {
				midgardZauberlieder = MidgardResourceReader.GetMidgardResource<Zauberlieder> (MidgardResourceReader.MidgardZauberLieder);
			}
			return midgardZauberlieder;
		}
	}


	/// <summary>
	/// The midgard zaubersalze. Aus XML-Datei
	/// </summary>
	private Zaubersalze midgardZaubersalze;
	public Zaubersalze MidgardZaubersalze {
		get {
			if (midgardZaubersalze == null) {
				midgardZaubersalze = MidgardResourceReader.GetMidgardResource<Zaubersalze> (MidgardResourceReader.MidgardZauberSalze);
			}
			return midgardZaubersalze;
		}
	}


	/// <summary>
	/// The midgard lernplaene.
	/// </summary>
	private Lernplaene midgardLernplaene;
	public Lernplaene MidgardLernplaene {
		get {
			if (midgardLernplaene == null) {
				midgardLernplaene = MidgardResourceReader.GetMidgardResource<Lernplaene> (MidgardResourceReader.MidgardLernplaene);
			}
			return midgardLernplaene;
		}
	}


	/// The midgard lernplaene.
	/// </summary>
	private Allgemeinwissen midgardAllgemeinwissen;
	public Allgemeinwissen MidgardAllgemeinwissen {
		get {
			if (midgardAllgemeinwissen == null) {
				midgardAllgemeinwissen = MidgardResourceReader.GetMidgardResource<Allgemeinwissen> (MidgardResourceReader.MidgardAllgemeinWissen);
			}
			return midgardAllgemeinwissen;
		}
	}


	/// <summary>
	/// The midgard ungewoehnliche fertigkeiten.
	/// </summary>
	private UngewoehnlicheFertigkeiten midgardUngewoehnlicheFertigkeiten;
	public UngewoehnlicheFertigkeiten MidgardUngewoehnlicheFertigkeiten {
		get {
			if (midgardUngewoehnlicheFertigkeiten == null) {
				midgardUngewoehnlicheFertigkeiten = MidgardResourceReader.GetMidgardResource<UngewoehnlicheFertigkeiten> (MidgardResourceReader.MidgardUngewoehnlicheFertigkeiten);
			}
			return midgardUngewoehnlicheFertigkeiten;
		}
	}
		

	/// <summary>
	/// The midgard Berufe .
	/// </summary>
	private Berufe midgardBerufe;
	public Berufe MidgardeBerufe {
		get {
			if (midgardBerufe == null) {
				midgardBerufe = MidgardResourceReader.GetMidgardResource<Berufe> (MidgardResourceReader.MidgardBerufe);
			}
			return midgardBerufe;
		}
	}


	/// The midgard Berufe .
	/// </summary>
	private UngelernteFertigkeiten midgardUngelerenteFertigkeiten;
	public UngelernteFertigkeiten MidgardUngelerenteFertigkeiten {
		get {
			if (midgardUngelerenteFertigkeiten == null) {
				midgardUngelerenteFertigkeiten = MidgardResourceReader.GetMidgardResource<UngelernteFertigkeiten> (MidgardResourceReader.MidgardUngelernteFertigkeiten);
			}
			return midgardUngelerenteFertigkeiten;
		}
	}

	//ausnahme für die Page-Navigation: falls bei angeborenen Fertigkeiten 100 geworfen wird, nicht zur nächsten Seite hüpfen
	public bool angeborene100 = false;
    
    void Awake()
    {
        
    }
		

	public void InstantiateLernplanHelpers(){
		// Your initialization code here
		mCharacterHelper = new MidgardCharacterHelper (this.mCharacter);
		lernHelper = new LernPlanHelper (mCharacterHelper);
	}


    // (optional) allow runtime registration of global objects
    static public T RegisterComponent<T>() where T : Component
    {
        return Instance.GetOrAddComponent<T>();
    }
}

