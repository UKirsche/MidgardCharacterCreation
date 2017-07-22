using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetCharacterBasisEigenschaften : MonoBehaviour {

    public InputField inSt, inGs, inGw, inKo, inIn, inZt, inSchB, inAusb;

    // Use this for initialization
    public void SetBasisEigenschaften () {
        Toolbox globalVars = Toolbox.Instance;

        int St = DiceCreator.ConvertInFieldToInt(inSt);
        int Gs = DiceCreator.ConvertInFieldToInt(inGs);
        int Gw = DiceCreator.ConvertInFieldToInt(inGw);
        int Ko = DiceCreator.ConvertInFieldToInt(inKo);
        int In = DiceCreator.ConvertInFieldToInt(inIn);
        int Zt = DiceCreator.ConvertInFieldToInt(inZt);

        globalVars.mCharacter.St = St;
        globalVars.mCharacter.Gs = Gs;
        globalVars.mCharacter.Gw = Gw;
        globalVars.mCharacter.Ko = Ko;
        globalVars.mCharacter.In = In;
        globalVars.mCharacter.Zt = Zt;

        CharacterEngine.ComputeEigenschaften(globalVars.mCharacter);
        Debug.Log(globalVars.mCharacter.ToString());
	}

    #region BasisEigenschaften automatisch auswürfeln
    public void SetBasisSt(){
		Toolbox globalVars = Toolbox.Instance;
		inSt.text = CharacterEngine.ComputeBasisSt (globalVars.mCharacter).ToString();
	}

	public void SetBasisGs(){
		Toolbox globalVars = Toolbox.Instance;
		inGs.text = CharacterEngine.ComputeBasisGs(globalVars.mCharacter).ToString();
	}

	public void SetBasisGw(){
		Toolbox globalVars = Toolbox.Instance;
		inGw.text = CharacterEngine.ComputeBasisGw(globalVars.mCharacter).ToString();
	}

	public void SetBasisKo(){
		Toolbox globalVars = Toolbox.Instance;
		inKo.text = CharacterEngine.ComputeBasisKo(globalVars.mCharacter).ToString();
	}

	public void SetBasisIn(){
		Toolbox globalVars = Toolbox.Instance;
		inIn.text = CharacterEngine.ComputeBasisIn(globalVars.mCharacter).ToString();
	}

	public void SetBasisZt(){
		Toolbox globalVars = Toolbox.Instance;
		inZt.text = CharacterEngine.ComputeBasisZt(globalVars.mCharacter).ToString();
	}
    #endregion

    #region Abgel.Eigenschaften in nächstes Panel, nachdem Basis festgelegt
    public void SetAbgeleiteteEigenschaften()
    {
        Toolbox globalVars = Toolbox.Instance;

        CharacterEngine.ComputeAbgeleiteteEigenschaften(globalVars.mCharacter);
        inSchB.text = globalVars.mCharacter.SchB.ToString();
        inAusb.text = globalVars.mCharacter.AusB.ToString();
    }

    #endregion
}
