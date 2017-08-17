using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Xml;

public static class GameSaveController {

	// TODO: Make it so that it actually saves to a file.
		// Maybe obfuscate the save file to prevent editing? Who cares? Question mark?

	static Player player;

	static List<SaveStation> _saveStationLocations = new List<SaveStation> ();
	public static List<SaveStation> SaveStationLocations {

		get { return _saveStationLocations; }
		set { _saveStationLocations = value; }

	}

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

	static string SaveStationString, AbilitiesString, LogsString, EnemiesString, ItemsString, SwitchString, DoorString;

	public static void LoadGame() {

		XmlReader xmlReader = XmlReader.Create("saveGameTest.xml");

		while(xmlReader.Read()) {

			if (xmlReader.NodeType == XmlNodeType.Element) {

				// <SaveData SaveFileName="SaveFileName" SaveFileDate="SaveFileDate" SaveFileTime="SaveFileTime" SaveFileTimeOnGame="SaveFileTimeOnGame" stationID="SAVE_STATION_A">
				if (xmlReader.Name == "SaveData") {
					
					Debug.Log (string.Format("<SaveData SaveFileName=\"{0}\" SaveFileDate=\"{1}\" SaveFileTime=\"{2}\" SaveFileTimeOnGame=\"{3}\" stationID=\"{4}\"",
						xmlReader.GetAttribute("SaveFileName"), xmlReader.GetAttribute("SaveFileDate"), xmlReader.GetAttribute("SaveFileTime"),
						xmlReader.GetAttribute("SaveFileTimeOnGame"), xmlReader.GetAttribute ("stationID")));

					Player.instance.transform.position = SaveStationLocations.FirstOrDefault (a => a.InteractableID == xmlReader.GetAttribute ("stationID")).transform.position;
					// TODO: Really none of this is useful to the player, SaveFileTimeOnGame might not even exist if you ask me

				}

				// <saveLocation>SAVE_STATION_A</saveLocation>
				if (xmlReader.Name == "saveLocation") {
					


				}


			}


//			if((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "saveLocation")) {
//
//				Debug.Log ("SaveLocation");
//
//				if(xmlReader.HasAttributes)
//					Debug.Log(xmlReader.GetAttribute("currency") + ": " + xmlReader.GetAttribute("rate"));  
//				
//			}
		}

	}

	public static void SaveGame() {

		SaveGame ("SAVE_STATION_A");

	}

	public static void SaveGame(string saveStationID = "SAVE_STATION_A") {

		Debug.Log ("Saving...");

		if (AttemptToFindPlayer ()) {

			xmlWriter = XmlWriter.Create("saveGameTest.xml");
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteWhitespace("\n");

			xmlWriter.WriteStartElement("SaveData");
			xmlWriter.WriteAttributeString ("SaveFileName", "SaveFileName");
			xmlWriter.WriteAttributeString ("SaveFileDate", "SaveFileDate");
			xmlWriter.WriteAttributeString ("SaveFileTime", "SaveFileTime");
			xmlWriter.WriteAttributeString ("SaveFileTimeOnGame", "SaveFileTimeOnGame");
			xmlWriter.WriteAttributeString ("stationID", saveStationID);


			// SaveLocation (saveStationID);
			SavePlayer ();
			SaveEntityStates ();
			SaveItemStates ();
			SaveSwitchStates ();
			SaveDoorStates ();
			SaveLogs ();

			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Close();

			Debug.Log (SaveStationString);
			Debug.Log (AbilitiesString);
			Debug.Log (LogsString);
			Debug.Log (EnemiesString);
			Debug.Log (ItemsString);
			Debug.Log (SwitchString);
			Debug.Log (DoorString);

		} else {

			Debug.LogError ("GameSaveController::SaveGame -- Holy fuck buckets Batman! We couldn't find the player, I couldn't possibly begin the explain why this is pretty bad.");

		}

	}

	static void SaveLocation(string stationID) {

		Debug.Log ("GameSaveController::SaveLocation -- Current SaveStationID: " + stationID);

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the save location ID of the SaveStation that the player saved at.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("saveLocation");
		xmlWriter.WriteAttributeString ("stationID", stationID);
		// xmlWriter.WriteString(stationID);
		xmlWriter.WriteEndElement();

	}

	static void SavePlayer() {

		// TODO: Do we even need list all save stations as they're all static anyway. Maybe just for the debug menu.

		SaveStationString = "GameSaveController::SavePlayer -- All SaveStations:";
		foreach (SaveStation s in SaveStationLocations) {
			
			SaveStationString += " StationID: " + s.InteractableID + ", Position: " + s.transform.position + ".";
		}

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This must be read into Player.Instance.Abilities");
		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteStartElement("abilities");

		AbilitiesString = "GameSaveController::SavePlayer -- All Abilities: ";
		foreach (Ability a in Player.instance.Abilities) {

			AbilitiesString += " AbilityName: '" + a.AbilityName + "', AbilityCollected: " + a.AbilityCollected + ".";

			xmlWriter.WriteAttributeString (a.AbilityName, a.AbilityCollected.ToString());

		}

		AbilitiesString += "WeaponProjectileModifier: " + player.WeaponProjectileModifier;

		xmlWriter.WriteAttributeString ("WeaponProjectileModifier", Player.instance.WeaponProjectileModifier.ToString());

		xmlWriter.WriteEndElement ();

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the modifer to the damage of the weapon the player has.");
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveLogs() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Logs that the player has collected.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("logs");

		LogsString = "GameSaveController::SaveLogs -- All collected Logs:";
		foreach (KeyValuePair<string, bool> log in LogsFoundByPlayer) {

			LogsString += " Name: " + log.Key + ".";

			xmlWriter.WriteWhitespace("\n\t\t");

			xmlWriter.WriteStartElement("log");
			xmlWriter.WriteAttributeString ("InteractableName", log.Key);
			xmlWriter.WriteAttributeString ("HasBeenCollected", log.Value.ToString());

			xmlWriter.WriteEndElement ();

		}

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveEntityStates() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Enemies ingame.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("enemies");

		EnemiesString = "GameSaveController::SaveEntityStates -- All enemies:";
		foreach (Enemy e in EnemiesInWorld) {

			EnemiesString += " EntityName: '" + e.EntityName + "', ID: '" + e.GetInstanceID () + "', HasBeenKilled: '" + e.HasBeenKilled
			+ "' PermanentlyKillable: "	+ e.PermanentlyKillable + ".";

			xmlWriter.WriteWhitespace ("\n\t\t");

			xmlWriter.WriteStartElement ("enemy");
			xmlWriter.WriteAttributeString ("EntityName", e.EntityName);
			xmlWriter.WriteAttributeString ("gameObjectInstanceID", e.GetInstanceID ().ToString ());
			xmlWriter.WriteAttributeString ("HasBeenKilled", e.HasBeenKilled.ToString ());
			xmlWriter.WriteAttributeString ("PermanentlyKillable", e.PermanentlyKillable.ToString ());

			xmlWriter.WriteEndElement ();

		}

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveItemStates() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Items the player has collected.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("items");

		ItemsString = "GameSaveController::SaveItemStates -- All Items:";
		foreach (Item i in ItemsInWorld) {
			
			ItemsString += " ItemName: '" + i.ItemName + "', ID: '" + i.GetInstanceID() + "', HasBeenCollected: " + i.HasBeenCollected;
			
			xmlWriter.WriteWhitespace("\n\t\t");

			xmlWriter.WriteStartElement("item");
			xmlWriter.WriteAttributeString ("ItemName", i.ItemName);
			xmlWriter.WriteAttributeString ("gameObjectInstanceID", i.GetInstanceID().ToString());
			xmlWriter.WriteAttributeString ("HasBeenCollected", i.HasBeenCollected.ToString());

			if (i.GetComponent<Key>() != null) {

				Key k = (Key) i;

				xmlWriter.WriteAttributeString ("KeyLocation", k.KeyLocation.ToString());

				ItemsString += ", Location: " + k.KeyLocation.ToString();

			}

			if (i.GetComponent<Artifact>() != null) {

				Artifact k = (Artifact) i;

				// TODO: Do we need this? We just need to save data that can change.
				xmlWriter.WriteAttributeString ("KeyLocation", k.KeyLocation.ToString());

				ItemsString += ", KeyLocation: " + k.KeyLocation.ToString();

			}

			ItemsString += ".";

			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveSwitchStates() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Switches.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("switches");

		SwitchString = "GameSaveController::SaveSwitchStates -- All Switches:";
		foreach (Switch s in SwitchesInWorld) {
			
			SwitchString += " InteractableID: '" + s.InteractableID + "', ID: '" + s.GetInstanceID() + "', IsCurrentlyLocked: " + s.IsCurrentlyLocked +
				", HasBeenUsedOnce" + s.HasBeenUsedOnce + ", CurrentState: " + s.IsOn + ".";

			xmlWriter.WriteWhitespace("\n\t\t");

			xmlWriter.WriteStartElement("switch");
			xmlWriter.WriteAttributeString ("InteractableID", s.InteractableID);
			xmlWriter.WriteAttributeString ("gameObjectInstanceID", s.GetInstanceID().ToString());
			xmlWriter.WriteAttributeString ("IsCurrentlyLocked", s.IsCurrentlyLocked.ToString());
			xmlWriter.WriteAttributeString ("HasBeenUsedOnce", s.HasBeenUsedOnce.ToString());
			xmlWriter.WriteAttributeString ("CurrentState", s.IsOn.ToString());

			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveDoorStates() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Doors.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("doors");

		DoorString = "GameSaveController::SaveDoorStates -- All Doors:";
		foreach (Door d in DoorsInWorld) {
			
			DoorString += " InteractableID: '" + d.InteractableID + "', ID: '" + d.GetInstanceID() + "', IsCurrentlyLocked: " + d.IsCurrentlyLocked +
				", HasBeenUsedOnce" + d.HasBeenUsedOnce + ", CurrentState: " + d.IsOn + ".";

			xmlWriter.WriteWhitespace("\n\t\t");

			xmlWriter.WriteStartElement("door");
			xmlWriter.WriteAttributeString ("InteractableID", d.InteractableID);
			xmlWriter.WriteAttributeString ("gameObjectInstanceID", d.GetInstanceID().ToString());
			xmlWriter.WriteAttributeString ("IsCurrentlyLocked", d.IsCurrentlyLocked.ToString());
			xmlWriter.WriteAttributeString ("HasBeenUsedOnce", d.HasBeenUsedOnce.ToString());
			xmlWriter.WriteAttributeString ("CurrentState", d.IsOn.ToString());

			if (d.GetComponent<DoorOperator> () != null) {

				DoorOperator o = (DoorOperator) d;

				xmlWriter.WriteAttributeString ("DoorOperatorCount", o.DoorOperatorCount.ToString());
				xmlWriter.WriteAttributeString ("DoorOperatorCountTotal", o.DoorOperatorCountTotal.ToString());

			}

			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static bool AttemptToFindPlayer() {

		if ((player != null) || (player = GameObject.FindObjectOfType<Player> ()) != null) {
			
			return true;

		}

		return false;


	}

}
