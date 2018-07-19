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

	//it's static so we can call it from anywhere
	public static bool Save(MidgardCharakter mCharacter) {
		bool successSerialize = true;
		MidgardCharacterSaveLoad.savedCharacters.Add(mCharacter);
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create ("midgardCharacters.gd"); //you can call it anything you want
		try {
			bf.Serialize(file, MidgardCharacterSaveLoad.savedCharacters);
		} catch (SerializationException ex) {
			Debug.LogError ("Serialisierung fehl geschlagen: " + ex.Message);
			successSerialize = false;
		}
		file.Close();
		return successSerialize;
	}   

	public static void Load() {
		if(File.Exists("midgardCharacters.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			MidgardCharacterSaveLoad.savedCharacters = (List<MidgardCharakter>)bf.Deserialize(file);
			file.Close();
		}
	}
}