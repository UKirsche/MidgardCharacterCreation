using UnityEngine;
using System.Collections;

/// <summary>
/// Masken type. Gibt Masekntypen zurück
/// </summary>
public abstract class MaskenType  {

	protected ContextMask _contextPanelType;
	protected LernPlanHelper lpHelper;


	protected MaskenType(){
		this.lpHelper = Toolbox.Instance.lernHelper;
	}


	/// <summary>
	/// Sets the type of the context panel.
	/// </summary>
	/// <param name="contextPanelName">Context panel name.</param>
	public abstract void SetContextPanelType();

	/// <summary>
	/// Gets the lern punkte rest. This is depending on the chosen context as set in ContextPanelType
	/// </summary>
	/// <returns>The lern punkte rest.</returns>
	public abstract int GetLernPunkteRest ();

	/// <summary>
	/// Sets the lern punkte rest. This is depending on the chosen context as set in ContextPanelType
	/// </summary>
	/// <returns>The lern punkte rest.</returns>
	public abstract void SetLernPunkteRest(int val);

	/// <summary>
	/// Resets the lern punkte. For cycling back in masks
	/// </summary>
	/// <returns>The lern punkte.</returns>
	/// <param name="val">If set to <c>true</c> value.</param>
	public abstract void ResetLernPunkte(bool val);	

	/// <summary>
	/// Gets the right panel. As panel can vary depending on which mask is activated.
	/// Three right panels exist: Fach, Waffen und Zauber. Activated panel is returned
	/// </summary>
	/// <returns>The right panel.</returns>
	public Transform GetRightPanel ()
	{
		Transform rightPanelDisplay=null;
		//Get active panel
		GameObject fachPanel = GameObject.Find ("GewähltFach");
		GameObject waffenPanel = GameObject.Find ("GewähltWaffen");
		GameObject zauberPanel = GameObject.Find ("GewähltZauber");

		if (fachPanel != null) {
			rightPanelDisplay = fachPanel.transform;
		} else if (waffenPanel != null) {
			rightPanelDisplay = waffenPanel.transform;
		} else if (zauberPanel != null) {
			rightPanelDisplay = zauberPanel.transform;
		}

		return rightPanelDisplay;
	}

	/// <summary>
	/// Adds the fertigkeit to character.
	/// </summary>
	/// <param name="item">Item.</param>
	public abstract void AddFertigkeitToCharacter (InventoryItem item);


	/// <summary>
	/// Deletes the fertigkeit from character.
	/// </summary>
	/// <param name="item">Item.</param>
	public abstract void  DeleteFertigkeitFromCharacter (InventoryItem item);
}
