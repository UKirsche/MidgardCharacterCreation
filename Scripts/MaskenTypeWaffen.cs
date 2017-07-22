using UnityEngine;
using System.Collections;

public class MaskenTypeWaffen : MaskenType {

	public MaskenTypeWaffen():base(){

	}

	public override void SetContextPanelType(){
		this._contextPanelType = ContextMask.WAFFEN;
	}

	public override int GetLernPunkteRest(){
		return lpHelper.LernPunkteWaffen;
	}

	public override void SetLernPunkteRest(int val){
		lpHelper.LernPunkteWaffen = val;
	}

	public override void ResetLernPunkte (bool val)
	{
		lpHelper.lernPunkteResetWaffe = val;
	}

	public override void AddFertigkeitToCharacter (InventoryItem item)
	{
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;
		mCharacter.waffenFertigkeiten.Add (item);
	}

	public override void DeleteFertigkeitFromCharacter (InventoryItem item)
	{
		MidgardCharakter mCharacter = Toolbox.Instance.mCharacter;
		mCharacter.waffenFertigkeiten.Remove (item);
		
	}
}
