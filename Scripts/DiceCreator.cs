using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


/// <summary>
/// Würfeltypen: w6 - sechsseitiger Würfel
/// </summary>
public enum WürfelTyp
{
    w6, w10, w20, w100
}

/// <summary>
/// Verbindet die Kamera mit dem Würfel, Kamera wird schon vorbereitet
/// </summary>
public class DiceWithCam
{
    public GameObject cam;
    public GameObject dice;

    //Erzeuge den Würfel immer 
    public DiceWithCam(WürfelTyp _type)
    {
        dice = new GameObject("Würfel_" + _type.ToString());
        cam = new GameObject("WürfelCamera");
    }

}


public class DiceCreator : MonoBehaviour {

    //Referenzen Eingabefelder UI
    public InputField inW6, inW10, inW20;
    //Referenzen PreFabs Würfel, Gesamtes Würefeldeck
    public GameObject W6preFabBlack, W10preFabBlack, W20preFabBlack, W6preFabBlue, W10preFabBlue, W20preFabBlue, WuerfelDeck;
    //Grundstartposition eines jeden erzeugten Würfels
    public Vector3 startPositionDice;
    //verschiebt die Würfel verschiedener Art diagonal in der x-z-Ebene
    public float xzOffsetDice; 

    //Parent-Game-Objekt für alle erzeugen Würfel innerhalb Würfeldeck
    protected GameObject wuerfelGO;

    //Liste aller erzeugten Würfel-Kamera-Game-objekte
    protected List<DiceWithCam> createdDiceWithCam;

    protected int countAllDice;


	public void ThrowDice()
    {
        //Zerstöre alle alten Würfel, ereuge neue Liste für alle Würfel
        DestroyOldDice();

        //Erzeuge neue Liste für Dice with Cam
        createdDiceWithCam = new List<DiceWithCam>();


        //Setze den Counter für alle Würfel
        countAllDice = 0;

        //Lege Startposition der einzelnen Würfeltypen fest
        Vector3 startPositionDiceW6 = startPositionDice;
        Vector3 startPositionDiceW10 = new Vector3(startPositionDice.x + xzOffsetDice, startPositionDice.y, startPositionDice.z + xzOffsetDice);
        Vector3 startPositionDiceW20 = new Vector3(startPositionDiceW10.x + xzOffsetDice, startPositionDiceW10.y, startPositionDiceW10.z + xzOffsetDice);

        //WürfelDeck-Gamobjekt -> parent für Würfel GameObject
        WuerfelDeck = GameObject.Find("WürfelDeck");

        //Hole der jeweiligen würfel aus den eingabefeldern
        int intW6 = ConvertInFieldToInt(inW6);
        int intW10 = ConvertInFieldToInt(inW10);
        int intW20 = ConvertInFieldToInt(inW20);

        //Instantiere die Würfel mit Kameras!
        InstantiateDice(WürfelTyp.w20, startPositionDiceW20, intW20);
        InstantiateDice(WürfelTyp.w10, startPositionDiceW10, intW10);
        InstantiateDice(WürfelTyp.w6, startPositionDiceW6, intW6);
    }

	protected virtual void DestroyOldDice()
    {
        DestroyObject(wuerfelGO);

        if(createdDiceWithCam!=null && createdDiceWithCam.Count > 0)
        {
            foreach (var dCam in createdDiceWithCam)
            {
                DestroyObject(dCam.dice);
                DestroyObject(dCam.cam);
            }
        }
    }

    /// <summary>
    /// Instanzieert die Würfel und speichert sie inklusive eigener Kamera in dcam-Liste ab
    /// </summary>
    /// <param name="typ"></param>
    /// <param name="position"></param>
    /// <param name="anzahl"></param>
	protected virtual void InstantiateDice(WürfelTyp typ, Vector3 position, int anzahl)
    {
        if (anzahl > 0)
        {
            DiceWithCam dCam;
            CreateParentGameObject();

            for (int count = 0; count < anzahl; count++)
            {
                //Erzeuge jeweils neue Kamera mit Würfel
                dCam = new DiceWithCam(typ);
                position.y += count;

                SetParentGameObject(dCam);

                switch (typ)
                {
                    case WürfelTyp.w6:
                        if (count % 2 == 0)
                        {
                            dCam.dice = Instantiate(W6preFabBlack, position, Quaternion.identity) as GameObject;
                        } else
                        {
                            dCam.dice = Instantiate(W6preFabBlue, position, Quaternion.identity) as GameObject;
                        }
                        
                        dCam.dice.name = "W6_" + count;
                        break;
                    case WürfelTyp.w10:
                        if (count % 2 == 0)
                        {
                            dCam.dice = Instantiate(W10preFabBlack, position, Quaternion.identity) as GameObject;
                        }
                        else
                        {
                            dCam.dice = Instantiate(W10preFabBlue, position, Quaternion.identity) as GameObject;
                        }
                        dCam.dice.name = "W10_" + count;
                        break;
                    case WürfelTyp.w20:
                        if (count % 2 == 0)
                        {
                            dCam.dice = Instantiate(W20preFabBlack, position, Quaternion.identity) as GameObject;
                        }
                        else
                        {
                            dCam.dice = Instantiate(W20preFabBlue, position, Quaternion.identity) as GameObject;
                        }
                        dCam.dice.name = "W20_" + count;
                        break;
                    default:
                        break;
                }

                //Beschleunige und rotiere den Würfel
                RotateAndAccelerate(dCam.dice);

                //Positioniere die Kamera auf Würfel
                PositionCam(dCam);

                //füge den Würfel mit Kamera der UpdateListe hinzu
                createdDiceWithCam.Add(dCam);

                //Zähle aktuelle Wüfel nach oben
                countAllDice++;
            }
        }
    }

    /// <summary>
    /// Falls Würfel erzeugt werden sollen: Schaffe Parent, falls noch nicht geschehen. Wird in WürfelDeck eingehakt
    /// Hier sollen alle Kids eingeordnet werden
    /// </summary>
	protected void CreateParentGameObject()
    {
        wuerfelGO = new GameObject("Würfel");
        wuerfelGO.transform.parent = WuerfelDeck.transform;
    }

    /// <summary>
    /// Rotiert und beschleunigt den Würfel
    /// </summary>
    /// <param name="dice"></param>
    protected void RotateAndAccelerate(GameObject dice)
    {
        Rigidbody RB = dice.GetComponent<Rigidbody>(); ;
        float rotationspeed = 160;
        float forceStrength = 800;
        

        //Quaternion -> Drehe
        RB.angularVelocity = UnityEngine.Random.insideUnitSphere * rotationspeed;
        Vector3 forceDirectionDown = UnityEngine.Random.insideUnitSphere;
        if (forceDirectionDown.y > 0)
        {
            forceDirectionDown.y *= -1;
        }

        forceDirectionDown.x *= forceStrength;
        forceDirectionDown.z *= forceStrength;
        forceDirectionDown.y *= forceStrength * 2;

        //Werfe nach unten irgendwohin
        RB.AddForce(forceDirectionDown);

    }



    /// <summary>
    /// Setzt das Parent-GameObject für Kamera und Würfel (s.o.)
    /// </summary>
    /// <param name="dCam"></param>
    protected void SetParentGameObject(DiceWithCam dCam)
    {
        //Setze gleiche Parents
        dCam.dice.transform.parent = wuerfelGO.transform;
        dCam.cam.transform.parent = wuerfelGO.transform;
    }

    /// <summary>
    /// Positioniert Rect-Port-Kamera über Würfel und gibt das Bild am unteren Bildschirmrand aus
    /// Bei vielen Würfeln: verschiebt immer um 1/8 der Bildschirmbreite (aktueller Würfel: countAllDice)
    /// </summary>
    /// <param name="dCam"></param>
	protected void PositionCam(DiceWithCam dCam)
    {
        float broadOffset = 8.0f;

        //Hole Kamera
        dCam.cam.AddComponent<Camera>();
        Camera WCam = dCam.cam.GetComponent<Camera>();

        //Positionierung folgt in LateUpdate, Rotation hier
        WCam.transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.down);
        WCam.rect = new Rect(countAllDice / broadOffset, 0, 0.15f, 0.15f);
    }

    /// <summary>
    /// Verschiebt die Kamera jedes Würfels entsprechen Position in x-z in Richtung y um eins
    /// </summary>
    public void LateUpdate()
    {
        if(wuerfelGO!=null)
        {
            foreach (var dCam in createdDiceWithCam)
            {
                Camera WCam = dCam.cam.GetComponent<Camera>();
                WCam.transform.position = dCam.dice.transform.position + 1.3f*Vector3.up;
            }
        }
    }


    /// <summary>
    /// Konvertiert Eingabe aus Ui in Integer
    /// </summary>
    /// <param name="diceNumber"></param>
    /// <returns>int</returns>
    public static int ConvertInFieldToInt(InputField diceNumber)
    {
        int retValue=0;
        string diceNumberText = diceNumber.text;

        if (diceNumberText.Length > 0)
        {
            retValue = Convert.ToInt32(diceNumberText); 
        }
        return retValue;
    }

}
