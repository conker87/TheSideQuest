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

	static string SaveStationString, AbilitiesString, LogsString, EnemiesString, ItemsString, SwitchString, DoorString;

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

			SaveLocation (saveStationID);
			SavePlayer ();
			SaveEntityStates ();
			SaveItemStates ();
			SaveSwitchStates ();
			SaveDoorStates ();
			SaveLogs ();

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
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("saveLocation");
		xmlWriter.WriteString(stationID);
		xmlWriter.WriteEndElement();

	}

	static void SavePlayer() {

		// TODO: Do we even need list all save stations as they're all static anyway. Maybe just for the debug menu.

		SaveStationString = "GameSaveController::SavePlayer -- All SaveStations:";
		foreach (SaveStation s in SceneManager.SaveStationLocations) {
			
			SaveStationString += " StationID: " + s.InteractableID + ", Position: " + s.transform.position + ".";
		}

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This must be read into Player.Instance.Abilities");
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteStartElement("abilities");

		AbilitiesString = "GameSaveController::SavePlayer -- All Abilities: ";
		foreach (Ability a in GameObject.FindObjectOfType<Player>().Abilities) {

			AbilitiesString += " AbilityName: '" + a.AbilityName + "', AbilityCollected: " + a.AbilityCollected + ".";

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

		xmlWriter.WriteStartElement("WeaponProjectileModifier");
		xmlWriter.WriteValue(player.WeaponProjectileModifier);
		xmlWriter.WriteEndElement();

	}

	static void SaveLogs() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Logs that the player has collected.");
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("logs");

		LogsString = "GameSaveController::SaveLogs -- All collected Logs:";
		foreach (KeyValuePair<string, bool> log in LogsFoundByPlayer) {

			LogsString += " Name: " + log.Key + ".";

			xmlWriter.WriteWhitespace("\n\t");

			xmlWriter.WriteStartElement("log_collected");
			xmlWriter.WriteValue(log.Key);

			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveEntityStates() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Enemies ingame.");
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("enemies");

		EnemiesString = "GameSaveController::SaveEntityStates -- All enemies:";
		foreach (Enemy e in EnemiesInWorld) {

			EnemiesString += " EntityName: '" + e.EntityName + "', ID: '" + e.GetInstanceID () + "', HasBeenKilled: '" + e.HasBeenKilled
			+ "' PermanentlyKillable: "	+ e.PermanentlyKillable + ".";

			xmlWriter.WriteWhitespace ("\n\t");

			xmlWriter.WriteStartElement ("enemy");
			xmlWriter.WriteAttributeString ("EntityName", e.EntityName);
			xmlWriter.WriteAttributeString ("gameObjectInstanceID", e.GetInstanceID ().ToString ());
			xmlWriter.WriteAttributeString ("HasBeenKilled", e.HasBeenKilled.ToString ());
			xmlWriter.WriteAttributeString ("PermanentlyKillable", e.PermanentlyKillable.ToString ());

			xmlWriter.WriteEndElement ();

		}

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveItemStates() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Items the player has collected.");
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("items");

		ItemsString = "GameSaveController::SaveItemStates -- All Items:";
		foreach (Item i in ItemsInWorld) {
			
			ItemsString += " ItemName: '" + i.ItemName + "', ID: '" + i.GetInstanceID() + "', HasBeenCollected: " + i.HasBeenCollected;
			
			xmlWriter.WriteWhitespace("\n\t");

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

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveSwitchStates() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Switches.");
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("switches");

		SwitchString = "GameSaveController::SaveSwitchStates -- All Switches:";
		foreach (Switch s in SwitchesInWorld) {
			
			SwitchString += " InteractableID: '" + s.InteractableID + "', ID: '" + s.GetInstanceID() + "', IsCurrentlyLocked: " + s.IsCurrentlyLocked +
				", HasBeenUsedOnce" + s.HasBeenUsedOnce + ", CurrentState: " + s.IsOn + ".";

			xmlWriter.WriteWhitespace("\n\t");

			xmlWriter.WriteStartElement("switch");
			xmlWriter.WriteAttributeString ("InteractableID", s.InteractableID);
			xmlWriter.WriteAttributeString ("gameObjectInstanceID", s.GetInstanceID().ToString());
			xmlWriter.WriteAttributeString ("IsCurrentlyLocked", s.IsCurrentlyLocked.ToString());
			xmlWriter.WriteAttributeString ("HasBeenUsedOnce", s.HasBeenUsedOnce.ToString());
			xmlWriter.WriteAttributeString ("CurrentState", s.IsOn.ToString());

			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveDoorStates() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Doors.");
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("doors");

		DoorString = "GameSaveController::SaveDoorStates -- All Doors:";
		foreach (Door d in DoorsInWorld) {
			
			DoorString += " InteractableID: '" + d.InteractableID + "', ID: '" + d.GetInstanceID() + "', IsCurrentlyLocked: " + d.IsCurrentlyLocked +
				", HasBeenUsedOnce" + d.HasBeenUsedOnce + ", CurrentState: " + d.IsOn + ".";

			xmlWriter.WriteWhitespace("\n\t");

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

		xmlWriter.WriteWhitespace("\n");
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
