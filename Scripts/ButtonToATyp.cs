using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonToATyp : MonoBehaviour
{
    public GameObject Rasse, AbenteuerTyp;

    public void GoToAbenteuerTyp()
    {
        Rasse.SetActive(false);
        AbenteuerTyp.SetActive(true);
    }
}