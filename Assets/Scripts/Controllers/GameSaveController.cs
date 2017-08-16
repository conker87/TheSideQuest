using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Xml;

public static class GameSaveController {

	// TODO: Make it so that it actually saves to a file.
		// Maybe obfuscate the save file to prevent editing? Who cares? Question mark?

	static Player player;

	static List<Item> _itemsInWorld = new List<Item>();
	public static List<Item> ItemsInWorld {

		get { return _itemsInWorld; } 
		set { _itemsInWorld = value; }

	}

	static List<Entity> _enemiesInWorld = new List<Entity>();
	public static List<Entity> EnemiesInWorld {

		get { return _enemiesInWorld; } 
		set { _enemiesInWorld = value; }

	}

	static List<Switch> _switchesInWorld = new List<Switch>();
	public static List<Switch> SwitchesInWorld {

		get { return _switchesInWorld; } 
		set { _switchesInWorld = value; }

	}

	static List<Door> _doorsInWorld = new List<Door>();
	public static List<Door> DoorsInWorld {

		get { return _doorsInWorld; } 
		set { _doorsInWorld = value; }

	}

	static Dictionary<string, bool> _logsFoundByPlayer = new Dictionary<string, bool>();
	public static Dictionary<string, bool> LogsFoundByPlayer {

		get { return _logsFoundByPlayer; } 
		set { _logsFoundByPlayer = value; }

	}

	static XmlWriter xmlWriter;

	static string SaveStationString, AbilitiesString;

	public static void LoadGame() {

		Debug.LogError ("NYI");

	}

	public static void SaveGame() {

		SaveGame ("Test");

	}

	public static void SaveGame(string saveStationID = "test") {

		Debug.Log ("Saving...");

		if (AttemptToFindPlayer ()) {

			xmlWriter = XmlWriter.Create("saveGameTest.xml");
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteWhitespace("\n");

			SaveLocation (saveStationID, xmlWriter);
			SavePlayer (xmlWriter);
			SaveEntityStates (xmlWriter);
			SaveItemStates (xmlWriter);
			SaveSwitchStates (xmlWriter);
			SaveDoorStates (xmlWriter);
			SaveLogs (xmlWriter);

			xmlWriter.WriteEndDocument();
			xmlWriter.Close();

			Debug.Log (SaveStationString);
			Debug.Log (AbilitiesString);

		} else {

			Debug.LogError ("GameSaveController::SaveGame -- Holy fuck buckets Batman! We couldn't find the player, I couldn't possibly begin the explain why this is pretty bad.");

		}

	}

	static void SaveLocation(string stationID, XmlWriter writer) {

		Debug.Log ("GameSaveController::SaveLocation -- Current SaveStationID: " + stationID);

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the save location ID of the SaveStation that the player saved at.");
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("saveLocation");
		xmlWriter.WriteString(stationID);
		xmlWriter.WriteEndElement();

	}

	static void SavePlayer(XmlWriter writer) {

		// TODO: Do we even need list all save stations as they're all static anyway. Maybe just for the debug menu.

		SaveStationString = "GameSaveController::SavePlayer -- All SaveStations: ";
		foreach (SaveStation s in SceneManager.SaveStationLocations) {
			
			SaveStationString += "StationID: " + s.InteractableID + ", Position: " + s.transform.position + "\n";
		}


		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This must be read into Player.Instance.Abilities");
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteStartElement("abilities");

		AbilitiesString = "GameSaveController::SavePlayer -- All Abilities: ";
		foreach (Ability a in GameObject.FindObjectOfType<Player>().Abilities) {

			AbilitiesString += "AbilityName: '" + a.AbilityName + "', AbilityCollected: " + a.AbilityCollected + "\n";

			xmlWriter.WriteWhitespace("\n\t");
			xmlWriter.WriteStartElement(a.AbilityName);
			xmlWriter.WriteValue(a.AbilityCollected);
			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteEndElement();

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the modifer to the damage of the weapon the player has.");
		xmlWriter.WriteWhitespace("\n");

		Debug.Log ("WeaponProjectileModifier: " + player.WeaponProjectileModifier);

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteStartElement("WeaponProjectileModifier");
		xmlWriter.WriteValue(player.WeaponProjectileModifier);
		xmlWriter.WriteEndElement();

	}

	static void SaveLogs(XmlWriter writer) {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Logs that the player has collecteds.");
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("logs");

		foreach (KeyValuePair<string, bool> log in LogsFoundByPlayer) {

			Debug.Log ("Log of ID: '" + log.Key + "' has been found and added to the dictionary");

			xmlWriter.WriteWhitespace("\n\t");

			xmlWriter.WriteStartElement("log_collected");
			xmlWriter.WriteValue(log.Key);
			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveEntityStates(XmlWriter writer) {

		foreach (Enemy e in EnemiesInWorld) {

			Debug.Log("Enemy: '" + e.name + "', ID: '" + e.GetInstanceID() + "', HasBeenKilled: '" + e.HasBeenKilled
				+ "' PermanentlyKillable: "	+ e.PermanentlyKillable + ", Active? " + e.isActiveAndEnabled);

		}

	}

	static void SaveItemStates(XmlWriter writer) {

		foreach (Item i in ItemsInWorld) {

			string debug = "";

			debug += "Item: '" + i.name + "', ID: '" + i.GetInstanceID() + "', HasBeenCollected: " + i.HasBeenCollected;

			if (i is Key || i is Artifact) {

				Key k = (Key) i;

				debug += ", Location: " + k.KeyLocation.ToString();

			}

			Debug.Log(debug);

		}

	}

	static void SaveSwitchStates(XmlWriter writer) {

		foreach (Switch s in SwitchesInWorld) {

			Debug.Log ("Switch: '" + s.name + "', ID: '" + s.GetInstanceID() + "', CurrentState: " + s.IsOn /* s.CurrentState */ + ".");

		}

	}

	static void SaveDoorStates(XmlWriter writer) {

		foreach (Door d in DoorsInWorld) {

			Debug.Log ("Door: '" + d.name + "', ID: '" + d.GetInstanceID() + "', IsOn: '" + d.IsOn + "', CurrentlyLocked: '" + d.IsCurrentlyLocked +
				"', IsOneUseOnly: '" + d.IsOneUseOnly + "', HasBeenUsedOnce: '" + d.HasBeenUsedOnce + "'.");

		}

	}

	static bool AttemptToFindPlayer() {

		if ((player != null) || (player = GameObject.FindObjectOfType<Player> ()) != null) {
			
			return true;

		}

		return false;


	}

}
