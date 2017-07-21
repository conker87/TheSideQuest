using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveController : MonoBehaviour {

	// TODO: Figure out how to check what Interactables have SaveStateToFile as true and only populate the Lists with those.

	[SerializeField]
	List<Entity> enemiesInWorld = new List<Entity>();

	[SerializeField]
	List<Switch> switchesInWorld = new List<Switch>();

	[SerializeField]
	List<Door> doorsInWorld = new List<Door>();

	void Start() {

		RemoveBadSwitches ();
		RemoveBadDoors ();
		Invoke("PopulateEntityList", 1f);

	}

	public void SaveGame() {

		SavePlayer ();
		SaveEntityStates ();
		SaveSwitchStates ();
		SaveDoorStates ();

	}

	void SavePlayer() {



	}

	void SaveEntityStates() {

		foreach (Enemy e in enemiesInWorld) {

			print ("Enemy: '" + e.name + "', ID: '" + e.GetInstanceID() + "', HasBeenKilled: '" + e.HasBeenKilled
				+ "' PermanentlyKillable: "	+ e.PermanentlyKillable + ", Active? " + e.isActiveAndEnabled);

		}

	}

	void SaveSwitchStates() {

		foreach (Switch s in switchesInWorld) {

			print ("Switch: '" + s.name + "', ID: '" + s.GetInstanceID() + "', CurrentState: '" + s.CurrentState + "'.");

		}

	}

	void SaveDoorStates() {

		foreach (Door d in doorsInWorld) {

			print ("Door: '" + d.name + "', ID: '" + d.GetInstanceID() + "', CurrentState: '" + d.CurrentState + "'.");

		}

	}

	void PopulateEntityList() {

		Enemy[] allEnemies = transform.parent.GetComponentsInChildren<Enemy>(true);

		foreach (Enemy e in allEnemies) {

			enemiesInWorld.Add (e);

		}

	}

	void RemoveBadSwitches() {

		for (int i = switchesInWorld.Count - 1; i >= 0; i--) {

			if (!switchesInWorld [i].SaveStateToFile) {

				switchesInWorld.RemoveAt (i);

			}

		}

	}

	void RemoveBadDoors() {

		for (int i = 0; i < doorsInWorld.Count; i++) {

			if (!doorsInWorld [i].SaveStateToFile) {

				doorsInWorld.RemoveAt (i);

			}

		}

	}

}
