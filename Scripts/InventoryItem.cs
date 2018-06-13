using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;



/// <summary>
/// Inventory item. Verfügt über Namen und Wert
/// </summary>
[Serializable]  
public class InventoryItem: IID, IName, IValue, ICost {
	public int id{ get; set;}
	public string type { get; set;}
	public string name { get; set;}
	public string val { get; set;}

	[NonSerialized]
	private int _cost;
	public int cost { 
		get{ return _cost; }
		set{ _cost = value; }
	}
	[NonSerialized]
	private bool _activated;
	public bool activated { 
		get{ return _activated;}
		set{ _activated = value;}
	}
	[NonSerialized]
	private int[] _dependency;
	public int[] dependency
	{
		get{
			return _dependency;
		}
	}

	public List<int> ReverseDependency { get; set;}

	//Standard
	public InventoryItem(){
		ReverseDependency = new List<int> ();
	}

	public InventoryItem(int _id, string _name, string _val):this(){
		id = _id;
		name = _name;
		val = _val;
	}

	public InventoryItem(int _id, string _name, string _val, int _cost, string _type):this(){
		id = _id;
		name = _name;
		val = _val;
		cost = _cost;
		type = _type;
	}

	/// <summary>
	/// Sets the dependency for the item. 
	/// Heißt: Das InventoryItem hat noch eine Abhängigkeit zu einem anderen Item. Diese wird als String ausgelesen und in ein Array überführt
	/// </summary>
	/// <param name="_dependency">Dependency.</param>
	public void SetDependency(string _dependencyString){
		if (_dependencyString != null && _dependencyString.Length > 0) {
			string[] depArray = _dependencyString.Split (';');
			_dependency = new int[depArray.Length];

			for (int i = 0; i < depArray.Length; i++) {
				_dependency[i]=Convert.ToInt32 (depArray [i]);
			}
		}
	}


	/// <summary>
	/// Gets the first dependency.
	/// </summary>
	/// <returns>The first dependency.</returns>
	public int? GetFirstDependency(){
		if (_dependency != null && _dependency.Length > 0) {
			return _dependency [0];
		}

		return null;
	}
}
