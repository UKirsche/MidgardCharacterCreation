using UnityEngine;
using System.Collections;

public class MaskenTypeFach : MaskenType {

	public MaskenTypeFach():base(){
		
	}

	public override void SetContextPanelType(){
		this._contextPanelType = ContextMask.FACH;
	}

	public override int GetLernPunkteRest(){
		return lpHelper.LernPunkteFach;
	}

	public override void SetLernPunkteRest(int val){
		lpHelper.LernPunkteFach = val;
	}

	public override void ResetLernPunkte (bool val)
	{
		lpHelper.lernPunkteResetFach = val;
	}

	public override void AddFertigkeitToCharacter (InventoryItem item)
	{
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;
		mCharacter.fertigkeiten.Add (item);
	}

	public override void DeleteFertigkeitFromCharacter (InventoryItem item)
	{
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;
		mCharacter.fertigkeiten.Remove (item);
	}
}
