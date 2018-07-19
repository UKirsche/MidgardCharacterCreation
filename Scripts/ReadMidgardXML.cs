using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;


#region interfaces
public interface IName{

	string name { get; set;}
}
public interface IID{

	int id { get; set;}
}
public interface IRestriktion{

	Restriktionen restriktionen { get; set;}
}
public interface IValue{
	string val{get;set;}
}
public interface ICost{
	int cost{get;set;}
}

public interface ISprache{
	string sprache{ get; set;}
}

public interface IDisplay: IID, ICost
{
}

public interface ILernplan: IDisplay, IValue{
}


public interface IReflect: IName{
	int val { get; set;}
}

public interface IFertigkeit: IID, IName{
	
	string Leiteigenschaft { get; set; }
	string Nebeneigenschaft { get; set; }
	string Dependency{ get; set;}
	string typ{ get; set;}
}

public interface IZauber: IID, IName{
}


#endregion

#region MidgardLaender
public class Laender
{
    [XmlElement("Land")]
    public List<Land> landListe = new List<Land>();
}


public class Land : IName, IID
{
    [XmlAttribute("id")]
    public int id { get; set; }
    public string name { get; set; }
}

public class LandRef : IID
{
    [XmlAttribute("ref")]
    public int id { get; set; }
}
#endregion

#region MidgardStaedte
public class Staedte
{
    [XmlElement("Stadt")]
    public List<Stadt> stadtListe = new List<Stadt>();
}


public class Stadt : IName, IID
{
    [XmlAttribute("id")]
    public int id { get; set; }
    public string name { get; set; }
    public int einwohner { get; set; }
    public string beschreibung { get; set; }
    [XmlAttribute("ref")]
    public int LandID { get; set; }
}
#endregion

#region Restriktionen
public class Restriktionen
{
    [XmlElement("Basiseigenschaft")]
    public List<BasiseigenschaftRef> basisEigenschaften = new List<BasiseigenschaftRef>();
    [XmlElement("AbenteurerTyp")]
    public List<AbenteurerTypRef> abenteurerTypen = new List<AbenteurerTypRef>();
    [XmlElement("Land")]
    public List<LandRef> landIDs = new List<LandRef>();
    [XmlElement("Rasse")]
    public List<RasseRef> rasseIDs = new List<RasseRef>();
}
#endregion

#region MidgardRasse
public class Rassen
{
    [XmlElement("Rasse")]
    public List<Rasse> rassenListe = new List<Rasse>();
}


public class Rasse : IName, IID, IRestriktion
{
    [XmlAttribute("id")]
    public int id { get; set; }
    public string name { get; set; }
    [XmlElement("Restriktionen")]
    public Restriktionen restriktionen { get; set; }

	//Implementiere Vergleich
	public bool Equals (Rasse obj)
	{
		if (obj!=null && name == obj.name) {
			return true;
		}
		return false;
	}
}


public class RasseRef: IID
{
    [XmlAttribute("ref")]
    public int id { get; set; }
}

#endregion

#region Midgard AbenteurerKategorie
public class AbenteurerKategorien
{
    [XmlElement("typus")]
    public List<AbenteurerKategorie> kategorien = new List<AbenteurerKategorie>();
}

public class AbenteurerKategorie : IName, IID
{
    [XmlAttribute("id")]
    public int id { get; set; }
    [XmlText]
    public string name { get; set; }
}

public class AbenteurerKategorieRef : IID
{
    [XmlAttribute("ref")]
    public int id { get; set; }
}

#endregion

#region Basiseigenschaften
public class Basiseigenschaften
{
    [XmlElement("Basiseigenschaft")]
    public List<Basiseigenschaft> basiseigenschaften = new List<Basiseigenschaft>();
}

public class Basiseigenschaft : IName, IID
{
    [XmlAttribute("id")]
    public int id { get; set; }
    public string name { get; set; }
    public string kurzname { get; set; }
}

public class BasiseigenschaftRef : IID
{
    [XmlAttribute("ref")]
    public int id { get; set; }
    [XmlAttribute("max")]
    public int maxValue { get; set; }
    [XmlAttribute("min")]
    public int minValue { get; set; }
}
#endregion

#region Abgeleitete Eigenschaft
public class AbgeleiteteEigenschaft : IName
{
    public string name { get; set; }
    public string kurzname { get; set; }
}
#endregion

#region AbenteurerTypen
public class AbenteurerTypen
{
    [XmlElement("AbenteurerTyp")]
    public List<AbenteurerTyp> listAbenteurerTypen = new List<AbenteurerTyp>();
}

public class AbenteurerTyp : IName, IID, IRestriktion
{
    [XmlAttribute("id")]
    public int id { get; set; }
    public string name { get; set; }
    public string kurzname { get; set; }
    [XmlElement("AbenteurerKategorie")]
    public AbenteurerKategorieRef katID { get; set; }
    [XmlElement("Restriktionen")]
    public Restriktionen restriktionen { get; set; }
}

public class AbenteurerTypRef
{
    [XmlAttribute("ref")]
    public int TypID { get; set; }
}
#endregion  

#region Angeborene Fertigkeiten: Serialisierung für Charakterspeicherung
[Serializable]
public class AngeboreneFertigkeiten
{
	[XmlElement("Sinn")]
	public List<Sinn> listSinne = new List<Sinn> ();
	[XmlElement("Fertigkeit")]
	public List<AngeboreneFertigkeit> listAngFertigkeiten = new List<AngeboreneFertigkeit>();
}

[Serializable]
public class Sinn: IName, IID
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlAttribute("val")]
	public int value { get; set; }
}

[Serializable]
public class AngeboreneFertigkeit: IName, IID
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlAttribute("val")]
	public int value { get; set; }
}


#endregion

#region Fachkenntnisse
public class Fertigkeiten
{

    [XmlElement("Fachkenntnis")]
    public List<Fachkenntnis> fachKenntnisse = new List<Fachkenntnis>();
}


public class Fachkenntnis: IFertigkeit
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlAttribute("Kat")]
	public string kategorie { get; set; }
	public string typ { get; set; }
	[XmlAttribute("LE")]
	public string Leiteigenschaft { get; set; }
	[XmlAttribute("NE")]
	public string Nebeneigenschaft { get; set; }
	[XmlAttribute("Dep")]
	public string Dependency { get; set; }
	[XmlText]
	public string description {get;set;}
}

public class FachkenntnisRef: ILernplan
{
	[XmlAttribute("id")]
	public int id {get;set;}
	[XmlAttribute("val")]
	public string val {get;set;}
	[XmlAttribute("cost")]
	public int cost {get;set;}

}

public class FachkenntnisRefAllgemein: FachkenntnisRef, ISprache
{
	[XmlAttribute("sprache")]
	public string sprache {get;set;}
	[XmlAttribute("variabel")]
	public bool variabel {get;set;}
}

#endregion

#region Waffenfertigkeit 
public class Waffenfertigkeiten
{

    [XmlElement("Waffenfertigkeit")]
    public List<Waffenfertigkeit> waffenFertigkeiten = new List<Waffenfertigkeit>();
}

public class Waffenfertigkeit: IFertigkeit
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlAttribute("Schaden")]
	public string schaden { get; set; }
	[XmlAttribute("Typ")]
	public string typ { get; set; }
	[XmlAttribute("Kat")]
	public string kategorie { get; set; }
	[XmlAttribute("LE")]
	public string Leiteigenschaft { get; set; }
	[XmlAttribute("NE")]
	public string Nebeneigenschaft { get; set; }
	[XmlAttribute("Dep")]
	public string Dependency { get; set; }
	[XmlAttribute("Distanz")]
	public string distanzen { get; set; }
	[XmlText]
	public string description {get;set;}
}

public class WaffenfertigkeitRef: ILernplan
{
	[XmlAttribute("id")]
	public int id {get;set;}
	[XmlAttribute("val")]
	public string val {get;set;}
	[XmlAttribute("cost")]
	public int cost {get;set;}
}
#endregion

#region Zauberformel
public class Zauberformeln
{

	[XmlElement("Zauberformel")]
	public List<Zauberformel> zauberformeln = new List<Zauberformel>();
}

public class Zauberformel: IZauber
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlAttribute("Art")]
	public string art { get; set; }
	[XmlAttribute("Stufe")]
	public string stufe { get; set; }
	[XmlAttribute("Dauer")]
	public string zauberdauer { get; set; }
	[XmlAttribute("Reichweite")]
	public string reichweite { get; set; }
	[XmlAttribute("Wirkungsziel")]
	public string wirkungsziel { get; set; }
	[XmlAttribute("Wirkungsbereich")]
	public string wirkungsbereich { get; set; }
	[XmlAttribute("Wirkungsdauer")]
	public string wirkungszauer { get; set; }
	[XmlAttribute("Ursprung")]
	public string ursprung { get; set; }
	[XmlAttribute("AP")]
	public string kosten { get; set; }
	[XmlText]
	public string description { get; set; }
}

public class ZauberformelRef: IDisplay
{
	[XmlAttribute("id")]
	public int id {get;set;}
	[XmlAttribute("cost")]
	public int cost {get;set;}
}
#endregion

#region Zauberlieder
public class Zauberlieder
{

	[XmlElement("Zauberlied")]
	public List<Zauberlied> zauberlieder = new List<Zauberlied>();
}

public class Zauberlied : IZauber
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlAttribute("Stufe")]
	public string stufe { get; set; }
	[XmlAttribute("Reichweite")]
	public string reichweite { get; set; }
	[XmlAttribute("Wirkungsziel")]
	public string wirkungsziel { get; set; }
	[XmlAttribute("Wirkungsbereich")]
	public string wirkungsbereich { get; set; }
	[XmlAttribute("Wirkungsdauer")]
	public string wirkungszauer { get; set; }
	[XmlAttribute("AP")]
	public string kosten { get; set; }
	[XmlText]
	public string description { get; set; }
}

public class ZauberliedRef: IDisplay
{
	[XmlAttribute("id")]
	public int id {get;set;}
	[XmlAttribute("cost")]
	public int cost {get;set;}
}
#endregion

#region Zaubersalze
public class Zaubersalze
{

	[XmlElement("Zaubersalz")]
	public List<Zaubersalz> zaubersalze = new List<Zaubersalz>();
}

public class Zaubersalz: IZauber
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlAttribute("Zauberdauer")]
	public string zauberdauer { get; set; }
	[XmlAttribute("Reichweite")]
	public string reichweite { get; set; }
	[XmlAttribute("Wirkungsziel")]
	public string wirkungsziel { get; set; }
	[XmlAttribute("Wirkungsbereich")]
	public string wirkungsbereich { get; set; }
	[XmlAttribute("Wirkungsdauer")]
	public string wirkungszauer { get; set; }
	[XmlAttribute("AP")]
	public string kosten { get; set; }
}

public class ZaubersalzRef: IDisplay
{
	[XmlAttribute("id")]
	public int id {get;set;}
	[XmlAttribute("cost")]
	public int cost {get;set;}
}
#endregion


#region Lernpläne
public class Lernplaene
{

	[XmlElement("Lernplan")]
	public List<Lernplan> listLernPlaene = new List<Lernplan>();

}

public class Lernplan : IName, IID
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlAttribute("shortname")]
	public string shortname { get; set; }
	[XmlElement("Fachkenntnis")]
	public List<FachkenntnisRef> fachkenntnisse{ get; set; }
	[XmlElement("Waffenfertigkeit")]
	public List<WaffenfertigkeitRef> waffenfertigkeiten{ get; set; }
	[XmlElement("Zauberformel")]
	public List<ZauberformelRef> zauberformeln{ get; set; }
	[XmlElement("Zaubersalz")]
	public List<ZaubersalzRef> zaubersalze{ get; set; }
	[XmlElement("Zauberlied")]
	public List<ZauberliedRef> zauberlieder{ get; set; }
}

#endregion

#region Allgemeinwissen
public class Allgemeinwissen{

	[XmlElement("WissenLand")]
	public WissenLand landAllgemeinWissen { get; set;}

	[XmlElement("WissenStadt")]
	public WissenStadt stadtAllgemeinWissen { get; set;}
}


public class WissenLand{

	[XmlElement("Fachkenntnis")]
	public List<FachkenntnisRefAllgemein> fachkenntnisse = new List<FachkenntnisRefAllgemein>();
	[XmlElement("Waffenfertigkeit")]
	public List<WaffenfertigkeitRef> waffen = new List<WaffenfertigkeitRef> ();
}

public class WissenStadt{

	[XmlElement("Fachkenntnis")]
	public List<FachkenntnisRefAllgemein> fachkenntnisse = new List<FachkenntnisRefAllgemein>();
	[XmlElement("Waffenfertigkeit")]
	public List<WaffenfertigkeitRef> waffen = new List<WaffenfertigkeitRef> ();
}
#endregion

#region ungewöhnliche Fertigkeiten
public class UngewoehnlicheFertigkeiten{
	[XmlElement("Fachkenntnis")]
	public List<FachkenntnisRefAllgemein> fachkenntnisse = new List<FachkenntnisRefAllgemein>();
}
#endregion

#region berufe
public class Berufe{

	[XmlElement("BerufeLand")]
	public BerufeLand landBerufe { get; set;}

	[XmlElement("BerufeStadt")]
	public BerufeStadt stadtBerufe { get; set;}
}


public class BerufeLand{
	[XmlElement("Beruf")]
	public List<Beruf> berufe = new List<Beruf>();
}

public class BerufeStadt{
	[XmlElement("Beruf")]
	public List<Beruf> berufe = new List<Beruf>();
}

[Serializable]
public class Beruf:IID, IName,IValue
{
	[XmlAttribute("id")]
	public int id { get; set;}
	[XmlAttribute("name")]
	public string name { get; set;}
	[XmlAttribute("val")]
	public string val { get; set;}
	[XmlAttribute("Kat")]
	public string kategorie { get; set;}
	[XmlAttribute("Schicht")]
	public string schicht { get; set;}
	[XmlAttribute("Typus")]
	public string typus { get; set;}

}
#endregion


#region Ungelernte Fertigkeiten
public class UngelernteFertigkeiten
{
	[XmlElement("Fachkenntnis")]
	public List<FachkenntnisRef> fachkenntnisse = new List<FachkenntnisRef>();
	[XmlElement("Waffenfertigkeit")]
	public List<WaffenfertigkeitRef> waffen = new List<WaffenfertigkeitRef> ();
}


#endregion

#region MidgardResourceReader
/// <summary>
/// Liest die geforderten Objekte aus XML-Dateien aus. Die Pfade der Dateien sind vorerst fest in die statischen  Methoden eingebaut
/// </summary>
public class MidgardResourceReader
{
	// Namen der möglichen Midgard Objecte
	public static string MidgardLaender = "MidgardLaender.xml";
	public static string MidgardStaedte = "MidgardStaedte.xml";
	public static string MidgardRassen = "MidgardRassen.xml";
	public static string MidgardAbenteurerKategorie = "MidgardAbenteurerKategorie.xml";
	public static string MidgardBasisEigenschaften = "MidgardBasisEigenschaften.xml";
	public static string MidgardAbgeleiteteEigenschaften = "MidgardAbgeleiteteEigenschaften.xml";
	public static string MidgardAbenteurerTypen = "MidgardAbenteurerTypen.xml";
	public static string MidgardAngeboren = "MidgardAngeboren.xml";
	public static string MidgardFertigkeiten = "MidgardFertigkeiten.xml";
	public static string MidgardUngelernteFertigkeiten = "MidgardUngelFertigkeiten.xml";
	public static string MidgardUngewoehnlicheFertigkeiten = "MidgardUngwFertigkeiten.xml";
	public static string MidgardWaffenFertigkeiten = "MidgardWaffenFertigkeiten.xml";
	public static string MidgardZauber = "MidgardZauber.xml";
	public static string MidgardZauberLieder = "MidgardZauberLieder.xml";
	public static string MidgardZauberSalze = "MidgardZauberSalze.xml";
	public static string MidgardLernplaene = "MidgardLernplaene.xml";
	public static string MidgardAllgemeinWissen = "MidgardAllgemeinWissen.xml";
	public static string MidgardBerufe = "MidgardBerufe.xml";

	/// <summary>
	/// Holt die als XML gespeichert Resource in Objekt
	/// </summary>
	/// <returns>The midgard resource.</returns>
	/// <param name="fileName">File name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T GetMidgardResource<T>(string fileName) where T:class{
		XmlSerializer deserializerResource = new XmlSerializer(typeof(T));
		//TextReader MidgardXMLReader = new StreamReader(@"./" + fileName);
		//TextReader MidgardXMLReader = new StreamReader(@"Assets/Photon Unity Networking/Demos/DemoMidgardUI/Resources/"+ fileName);
		TextReader MidgardXMLReader = new StreamReader(@"/Users/Shared/Unity/Midgard/Assets/Photon Unity Networking/Demos/DemoMidgardCharacterCreation/Resources/"+ fileName);
		T listResource = deserializerResource.Deserialize(MidgardXMLReader) as T;
		MidgardXMLReader.Close();
		return listResource;
	}

	/// <summary>
	/// Holt die als XML gespeichert Resource in Objekt für Testframework
	/// </summary>
	/// <returns>The midgard resource.</returns>
	/// <param name="fileName">File name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T GetMidgardResourceForTests<T>(string fileName) where T:class{
		XmlSerializer deserializerResource = new XmlSerializer(typeof(T));
		//TextReader MidgardXMLReader = new StreamReader(@"./" + fileName);
		TextReader MidgardXMLReader = new StreamReader(@"/Users/Shared/Unity/Midgard/Assets/Photon Unity Networking/Demos/DemoMidgardCharacterCreation/Resources/"+ fileName);
		T listResource = deserializerResource.Deserialize(MidgardXMLReader) as T;
		MidgardXMLReader.Close();
		return listResource;
	}
}

#endregion

