using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour {

	[SerializeField]
	string _roomName;
	public string RoomName {

		get { return _roomName; }
		set { _roomName = value; }

	}

	[SerializeField]
	bool _isBossRoom;
	public bool IsBossRoom {

		get { return _isBossRoom; }
		set { _isBossRoom = value; }

	}

	[SerializeField]
	bool _isCurrentlyInRoom;
	public bool IsCurrentlyInRoom {

		get { return _isCurrentlyInRoom; }
		set { _isCurrentlyInRoom = value; }

	}
		
	[SerializeField]
	List<RoomSpawnEnemy> _enemiesInRoom;
	public List<RoomSpawnEnemy> EnemiesInRoom {

		get { return _enemiesInRoom; }
		set { _enemiesInRoom = value; }

	}

	[SerializeField]
	LevelDirection _roomLevelDirection;
	public LevelDirection RoomLevelDirection {

		get { return _roomLevelDirection; }
		set { _roomLevelDirection = value; }

	}

	Enemy[] currentEnemies;

	Boss b = null;

	void Start() {

		// Rename
		gameObject.name = string.Format("{0}_{1}_{2}", RoomName, transform.position, RoomLevelDirection.ToString());

		SpawnEnemies ();

	}

	public void EnteredRoom() {

		IsCurrentlyInRoom = true;
		SpawnEnemies ();
		print ("Entering: " + RoomName);

		UIController.instance.DoRoomName (RoomName); // Localisation.GetLocalisedText(RoomName, Localisation.CurrentLocal);
		UIController.instance.ShowBossHealth (b);

		// TODO: Decide whether to store the current direction into the Player or keep it here.
		UIController.instance.CurrentLevelDirection = RoomLevelDirection;

	}

	public void LeftRoom() {

		IsCurrentlyInRoom = false;
		DespawnEnemies ();

		b = null;

		UIController.instance.ShowBossHealth (b);

	}

	void SpawnEnemies(bool disableOnSpawn = false) {

		currentEnemies = GetComponentsInChildren<Enemy> (true);
		Vector2 trans = transform.position;
		bool hasFoundEnemy = false;

		foreach (RoomSpawnEnemy ers in EnemiesInRoom) {
			
			if (ers.HasBeenKilled && ers.PermanentlyKillable) {

				continue;

			}

			ers.HasBeenKilled = false;

			foreach (Enemy e in currentEnemies) {

				if (e.GetInstanceID () == ers.EnemyID) {

					hasFoundEnemy = true;

					e.transform.position = trans + ers.SpawnLocation;
					e.gameObject.SetActive (true);

					if (disableOnSpawn) {
						e.gameObject.SetActive (false);
					}

					e.PermanentlyKillable = ers.PermanentlyKillable;
					e.HasBeenKilled = false;

					e.SetHealthToMax ();

					if (ers.Enemy.GetComponent<Boss>() != null) {

						b = e as Boss;

					}

				}
			
			}

			if (!hasFoundEnemy) {

				Enemy spawnedEnemy;
				spawnedEnemy = Instantiate (ers.Enemy, trans + ers.SpawnLocation, Quaternion.identity, transform) as Enemy;
				spawnedEnemy.gameObject.name = "Enemy_" + spawnedEnemy.GetInstanceID ();

				ers.EnemyID = spawnedEnemy.GetInstanceID ();
				spawnedEnemy.SetHealthToMax ();
				spawnedEnemy.PermanentlyKillable = ers.PermanentlyKillable;

				if (ers.Enemy.GetComponent<Boss>() != null) {

					ers.PermanentlyKillable = true;
					IsBossRoom = true;
					b = spawnedEnemy as Boss;

				}

			}

		}

	}

	void DespawnEnemies() {

		currentEnemies = GetComponentsInChildren<Enemy> (true);

		foreach (Enemy e in currentEnemies) {

			foreach (RoomSpawnEnemy ers in EnemiesInRoom) {

				if (e.GetInstanceID () == ers.EnemyID) {

					ers.HasBeenKilled = e.HasBeenKilled;

				}

			}

			e.gameObject.SetActive(false);

		}

	}

	void OnDrawGizmos() {

		if (_enemiesInRoom != null) {

			Vector2 trans = transform.position;

			foreach (RoomSpawnEnemy e in EnemiesInRoom) {

				Gizmos.color = (e.Enemy is Boss) ? Color.red : Color.blue;

				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x, 		trans.y + e.SpawnLocation.y + 1f));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x + 1f, 	trans.y + e.SpawnLocation.y));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x, 		trans.y + e.SpawnLocation.y - 1f));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x - 1f, 	trans.y + e.SpawnLocation.y));

			}

		}

	}

}
