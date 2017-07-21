using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSaveController {

	// TODO: Make it so that it actually saves to a file.
		// Maybe obfuscate the save file to prevent editing? Who cares? Question mark?

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

	public static void SaveGame() {

		SaveGame ("Test");

	}

	public static void SaveGame(string saveStationID = "test") {

		Debug.Log ("Saving...");

		SaveLocation (saveStationID);
		SavePlayer ();
		SaveEntityStates ();
		SaveItemStates ();
		SaveSwitchStates ();
		SaveDoorStates ();

	}

	static void SaveLocation(string stationID) {

		Debug.Log ("Save game location ID: " + stationID);

	}

	static void SavePlayer() {

		foreach (SaveStation s in SceneManager.SaveStationLocations) {
			
			Debug.Log ("Name: '" + s.name + "', StationID: " + s.InteractableName + ", Position: " + s.transform.position);
		}

		foreach (Ability a in GameObject.FindObjectOfType<Player>().Abilities) {

			Debug.Log ("AbilityName: '" + a.AbilityName + "', AbilityCollected: " + a.AbilityCollected);

		}

	}

	static void SaveEntityStates() {

		foreach (Enemy e in EnemiesInWorld) {

			Debug.Log("Enemy: '" + e.name + "', ID: '" + e.GetInstanceID() + "', HasBeenKilled: '" + e.HasBeenKilled
				+ "' PermanentlyKillable: "	+ e.PermanentlyKillable + ", Active? " + e.isActiveAndEnabled);

		}

	}

	static void SaveItemStates() {

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

	static void SaveSwitchStates() {

		foreach (Switch s in SwitchesInWorld) {

			Debug.Log ("Switch: '" + s.name + "', ID: '" + s.GetInstanceID() + "', CurrentState: " + s.CurrentState + ".");

		}

	}

	static void SaveDoorStates() {

		foreach (Door d in DoorsInWorld) {

			Debug.Log ("Door: '" + d.name + "', ID: '" + d.GetInstanceID() + "', CurrentState: '" + d.CurrentState + "'.");

		}

	}

}
