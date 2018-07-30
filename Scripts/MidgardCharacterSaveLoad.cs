using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

/// <summary>
/// Midgard character save load.
/// </summary>
public static class MidgardCharacterSaveLoad {

	public static List<MidgardCharakter> savedCharacters = new List<MidgardCharakter>();
	public static MidgardCharakter chosenCharacter;

	//it's static so we can call it from anywhere
	public static bool Save(MidgardCharakter mCharacter) {
		bool successSerialize = true;
		MidgardCharacterSaveLoad.savedCharacters.Add(mCharacter);
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Open ("midgardCharacters.gd", FileMode.OpenOrCreate); //you can call it anything you want
		try {
			bf.Serialize(file, MidgardCharacterSaveLoad.savedCharacters);
		} catch (SerializationException ex) {
			Debug.LogError ("Serialisierung fehl geschlagen: " + ex.Message);
			successSerialize = false;
		}
		file.Close();
		return successSerialize;
	}   

	public static bool Load() {
		bool successDeserialize = true;
		if(File.Exists("midgardCharacters.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open("midgardCharacters.gd", FileMode.Open);
			try {
				MidgardCharacterSaveLoad.savedCharacters = (List<MidgardCharakter>)bf.Deserialize(file);
				
			} catch (SerializationException ex) {
				Debug.LogError ("Deserialisierung fehl geschlagen: " + ex.Message);
				successDeserialize = false;
			}
			file.Close();
		}
		return successDeserialize;
	}
}