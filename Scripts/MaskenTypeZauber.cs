using UnityEngine;
using System.Collections;

public class MaskenTypeZauber : MaskenType {

	public MaskenTypeZauber():base(){

	}

	public override void SetContextPanelType(){
		this._contextPanelType = ContextMask.ZAUBER;
	}

	public override int GetLernPunkteRest(){
		return lpHelper.LernPunkteZauber;
	}

	public override void SetLernPunkteRest(int val){
		lpHelper.LernPunkteZauber = val;
	}

	public override void ResetLernPunkte (bool val)
	{
		lpHelper.lernPunkteResetZauber = val;
	}

	public override void AddFertigkeitToCharacter (InventoryItem item)
	{
		string type = item.type;
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;

		if(type == ("Zauberformel")){
			mCharacter.zauberFormeln.Add (item);
		} else if(type == ("Zaubersalz")){
			mCharacter.zauberSalze.Add (item);
		} else if(type == ("Zauberlied")){
			mCharacter.zauberLieder.Add (item);
		}
	}

	public override void DeleteFertigkeitFromCharacter (InventoryItem item)
	{
		string type = item.type;
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;

		if(type == ("Zauberformel")){
			mCharacter.zauberFormeln.Remove (item);
		} else if(type == ("Zaubersalz")){
			mCharacter.zauberSalze.Remove (item);
		} else if(type == ("Zauberlied")){
			mCharacter.zauberLieder.Remove (item);
		}
	}
}
