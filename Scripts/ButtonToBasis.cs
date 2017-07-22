using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonToBasis : MonoBehaviour
{
    public GameObject AbenteuerTyp, BasisEigenschaften;

    public void GoToBasis()
    {
        AbenteuerTyp.SetActive(false);
        BasisEigenschaften.SetActive(true);
    }
}