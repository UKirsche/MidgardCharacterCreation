using UnityEngine;
using System.Collections;


public enum ContextMask{

	FACH, WAFFEN, ZAUBER, ZAUBERFORMEL, ZAUBERSALZ, ZAUBERLIED
}
	
	

/// <summary>
/// Handle mask types and return correct masktype
/// </summary>
public class HandleMaskTypeFactory {

	public static MaskenType GetMaskType(string contextPanelName){
		if (contextPanelName.Contains ("Fach")) {
			return new MaskenTypeFach ();
		} else if (contextPanelName.Contains ("Waffen")) {
			return new MaskenTypeWaffen();
		} else if (contextPanelName.Contains ("Zauber")) {
			return new MaskenTypeZauber();
		}

		return null;
	}
}
