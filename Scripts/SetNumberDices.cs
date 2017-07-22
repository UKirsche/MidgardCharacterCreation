using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetNumberDices : MonoBehaviour {

    public InputField numberField;
    public Slider slider;

    public void SetNumberOfDices()
    {
        numberField.text = slider.value.ToString();
    }
}
