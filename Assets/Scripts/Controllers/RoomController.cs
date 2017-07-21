using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour {

	UIController ui;

	[SerializeField]
	string _roomName;
	public string RoomName {

		get { return _roomName; }
		set { _roomName = value; }

	}

	[SerializeField]
	bool _bossRoom;
	public bool BossRoom {

		get { return _bossRoom; }
		set { _bossRoom = value; }

	}

	[SerializeField]
	bool _isCurrentlyInRoom;
	public bool IsCurrentlyInRoom {

		get { return _isCurrentlyInRoom; }
		set { _isCurrentlyInRoom = value; }

	}
		
	[SerializeField]
	List<EnemyRoomSpawn> _enemiesInRoom;
	public List<EnemyRoomSpawn> EnemiesInRoom {

		get { return _enemiesInRoom; }
		set { _enemiesInRoom = value; }

	}

	void Start() {

		ui = GameObject.FindObjectOfType<UIController> ();

	}

	public void EnteredRoom() {

		SpawnEnemies ();

		if (ui != null) {

			ui.ShowRoomNameText (RoomName); // Localisation.GetLocalisedText(RoomName, Localisation.CurrentLocal);

		}

	}

	public void LeftRoom() {

		DespawnEnemies ();

	}

	void SpawnEnemies() {

		Enemy[] currentEnemies = GetComponentsInChildren<Enemy> (true);
		Vector2 trans = transform.position;
		bool hasFoundEnemy = false;

		foreach (EnemyRoomSpawn ers in EnemiesInRoom) {

			if (ers.HasBeenKilled && ers.PermanentlyKillable) {

				continue;

			}

			ers.HasBeenKilled = false;

			foreach (Enemy e in currentEnemies) {

				if (e.GetInstanceID () == ers.EnemyID) {

					hasFoundEnemy = true;
					e.gameObject.SetActive (true);
					e.PermanentlyKillable = ers.PermanentlyKillable;

				}
			
			}

			if (!hasFoundEnemy) {

				Enemy spawnedEnemy;
				spawnedEnemy = Instantiate (ers.Enemy, trans + ers.SpawnLocation, Quaternion.identity, transform) as Enemy;
				ers.EnemyID = spawnedEnemy.GetInstanceID ();

			}

		}

	}

	void DespawnEnemies() {

		Enemy[] currentEnemies = GetComponentsInChildren<Enemy> (true);

		foreach (Enemy e in currentEnemies) {

			foreach (EnemyRoomSpawn ers in EnemiesInRoom) {

				if (e.GetInstanceID () == ers.EnemyID) {

					e.PermanentlyKillable = ers.PermanentlyKillable;

					if (!e.isActiveAndEnabled) {

						e.HasBeenKilled = ers.HasBeenKilled = true;

					}

				}


			}

			e.gameObject.SetActive(false);

		}

	}

	void OnDrawGizmos() {

		if (_enemiesInRoom != null) {

			Vector2 trans = transform.position;

			foreach (EnemyRoomSpawn e in EnemiesInRoom) {

				Gizmos.color = Color.blue;

				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x, 		trans.y + e.SpawnLocation.y + 1f));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x + 1f, 	trans.y + e.SpawnLocation.y));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x, 		trans.y + e.SpawnLocation.y - 1f));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x - 1f, 	trans.y + e.SpawnLocation.y));

			}

		}

	}

}
