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

				}

				// 	<abilities JUMP="True" JUMP_DOUBLE="False" JUMP_TRIPLE="False" WALL_SLIDE="False" WALL_JUMP="False" WALL_HIGH_JUMP="False"
				//		DASH="False" DASH_MEGA="False" CHEAT_JUMP="False" CHEAT_DASH="False" WeaponProjectileModifier="1" />
				if (xmlReader.Name == "abilities") {

					while (xmlReader.MoveToNextAttribute ()) {
						
						if (xmlReader.Name == "WeaponProjectileModifier") {

							Player.instance.WeaponProjectileModifier = float.Parse(xmlReader.Value);
							break;

						}

						foreach (Ability a in Player.instance.Abilities) {

							if (xmlReader.Name == a.AbilityName) {

								a.AbilityCollected = bool.Parse(xmlReader.Value);
								break;

							}

						}

					}

				}

				// 	<keys 0="1" 1="0" 2="0" 3="0" 4="0" 5="0" 6="0" />
				if (xmlReader.Name == "keys") {

					while (xmlReader.MoveToNextAttribute ()) {

						Player.instance.Keys [int.Parse(xmlReader.Name.Substring(1))] = int.Parse(xmlReader.Value);

					}

				}

				// <artifacts 0="False" 1="False" 2="False" 3="False" 4="False" 5="False" 6="False" />
				if (xmlReader.Name == "artifacts") {

					while (xmlReader.MoveToNextAttribute ()) {

						Player.instance.Artifacts [int.Parse(xmlReader.Name.Substring(1))] = bool.Parse(xmlReader.Value);

					}

				}

				// <enemy EntityName="Enemy_(63.9, -73.4)" gameObjectInstanceID="-156380" HasBeenKilled="False" PermanentlyKillable="True" />
				if (xmlReader.Name == "enemy") {
					
					foreach (Enemy e in GameObject.Find("World").GetComponentsInChildren<Enemy>(true)) {

						if (e.EntityName == xmlReader.GetAttribute ("EntityName")) {

							e.HasBeenKilled = bool.Parse(xmlReader.GetAttribute ("HasBeenKilled"));

						}

					}

				}

				// <item ItemName="ItemDashMega" gameObjectInstanceID="-1222" HasBeenCollected="False" />
				if (xmlReader.Name == "item") {

					foreach (Item i in GameObject.Find("World").GetComponentsInChildren<Item>(true)) {

						if (i.ItemName == xmlReader.GetAttribute ("ItemName")) {

							i.HasBeenCollected = bool.Parse(xmlReader.GetAttribute ("HasBeenCollected"));

						}

					}

				}

				// TODO: Since we're literally using the same details for both Switch and Door we should just merge the Switch and Door Lists into Interactable.
				// <switch InteractableID="sw" gameObjectInstanceID="-1054" IsCurrentlyLocked="False" HasBeenUsedOnce="False" IsOn="False" />
				if (xmlReader.Name == "switch") {

					foreach (Switch s in GameObject.Find("World").GetComponentsInChildren<Switch>(true)) {

						if (s.InteractableID == xmlReader.GetAttribute ("InteractableID")) {

							s.IsCurrentlyLocked = bool.Parse(xmlReader.GetAttribute ("IsCurrentlyLocked"));
							s.HasBeenUsedOnce = bool.Parse(xmlReader.GetAttribute ("HasBeenUsedOnce"));
							s.IsOn = bool.Parse(xmlReader.GetAttribute ("IsOn"));

						}

					}

				}

				// <door InteractableID="ENTITY_DOOR_TEST" gameObjectInstanceID="-1194" IsCurrentlyLocked="True" HasBeenUsedOnce="False" CurrentState="False" />
				if (xmlReader.Name == "door") {

					foreach (Door d in GameObject.Find("World").GetComponentsInChildren<Door>(true)) {

						if (d.InteractableID == xmlReader.GetAttribute ("InteractableID")) {

							d.IsCurrentlyLocked = bool.Parse(xmlReader.GetAttribute ("IsCurrentlyLocked"));
							d.HasBeenUsedOnce = bool.Parse(xmlReader.GetAttribute ("HasBeenUsedOnce"));
							d.IsOn = bool.Parse(xmlReader.GetAttribute ("IsOn"));

							if (d.GetComponent<DoorOperator> () != null) {

								DoorOperator o = (DoorOperator) d;

								o.DoorOperatorCount = int.Parse(xmlReader.GetAttribute ("DoorOperatorCount"));
								o.DoorOperatorCountTotal = int.Parse(xmlReader.GetAttribute ("DoorOperatorCountTotal"));

							}

						}

					}

				}

				// <log InteractableName="Test Log" HasBeenCollected="True" />
				if (xmlReader.Name == "log") {

					LogsFoundByPlayer.Add (xmlReader.GetAttribute ("InteractableName"), bool.Parse(xmlReader.GetAttribute ("HasBeenCollected")));

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
			SaveEnemyStates ();
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

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteStartElement("keys");
		for (int i = 0; i < Player.instance.Keys.Count(); i++) {

			xmlWriter.WriteAttributeString ("_" + i.ToString(), Player.instance.Keys[i].ToString());

		}
		xmlWriter.WriteEndElement ();

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteStartElement("artifacts");
		for (int i = 0; i < Player.instance.Artifacts.Count(); i++) {

			xmlWriter.WriteAttributeString ("_" + i.ToString(), Player.instance.Artifacts[i].ToString());

		}
		xmlWriter.WriteEndElement ();

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

	static void SaveEnemyStates() {
		
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

			// TODO: Do we need this? I think not.
			// xmlWriter.WriteAttributeString ("PermanentlyKillable", e.PermanentlyKillable.ToString ());

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
			xmlWriter.WriteAttributeString ("IsOn", s.IsOn.ToString());

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
				", HasBeenUsedOnce" + d.HasBeenUsedOnce + ", IsOn: " + d.IsOn + ".";

			xmlWriter.WriteWhitespace("\n\t\t");

			xmlWriter.WriteStartElement("door");
			xmlWriter.WriteAttributeString ("InteractableID", d.InteractableID);
			xmlWriter.WriteAttributeString ("gameObjectInstanceID", d.GetInstanceID().ToString());
			xmlWriter.WriteAttributeString ("IsCurrentlyLocked", d.IsCurrentlyLocked.ToString());
			xmlWriter.WriteAttributeString ("HasBeenUsedOnce", d.HasBeenUsedOnce.ToString());
			xmlWriter.WriteAttributeString ("IsOn", d.IsOn.ToString());

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
