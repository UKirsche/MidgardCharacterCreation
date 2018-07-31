using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

[Serializable]
public class MidgardCharacterSavings{

	public List<MidgardCharakter> savedCharacters = new List<MidgardCharakter>();
	public MidgardCharakter chosenCharakter;

}


/// <summary>
/// Midgard character save load.
/// </summary>
public static class MidgardCharacterSaveLoad {

	public static MidgardCharacterSavings midgardSavings = new MidgardCharacterSavings();

	//it's static so we can call it from anywhere
	public static bool Save(MidgardCharakter mCharacter) {
		bool successSerialize = true;
		MidgardCharacterSaveLoad.midgardSavings.savedCharacters.Add(mCharacter);
		successSerialize= SerializeFile ();
		return successSerialize;
	}   

	//it's static so we can call it from anywhere
	public static bool SaveChosen(MidgardCharakter mCharacter) {
		bool successSerialize = true;
		MidgardCharacterSaveLoad.midgardSavings.chosenCharakter = mCharacter;
		successSerialize= SerializeFile ();
		return successSerialize;
	}   

	public static bool Load() {
		bool successDeserialize = true;
		if(File.Exists("midgardCharacters.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open("midgardCharacters.gd", FileMode.Open);
			try {
				MidgardCharacterSaveLoad.midgardSavings = (MidgardCharacterSavings)bf.Deserialize(file);
				
			} catch (SerializationException ex) {
				Debug.LogError ("Deserialisierung fehl geschlagen: " + ex.Message);
				successDeserialize = false;
			}
			file.Close();
		}
		return successDeserialize;
	}

	private static bool SerializeFile ()
	{
		bool successSerialize = true;
		BinaryFormatter bf = new BinaryFormatter ();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Open ("midgardCharacters.gd", FileMode.OpenOrCreate);
		//you can call it anything you want
		try {
			bf.Serialize (file, MidgardCharacterSaveLoad.midgardSavings);
		}
		catch (SerializationException ex) {
			Debug.LogError ("Serialisierung fehl geschlagen: " + ex.Message);
			successSerialize = false;
		}
		file.Close ();
		return successSerialize;
	}
}