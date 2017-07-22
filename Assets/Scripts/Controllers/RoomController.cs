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
	List<EnemyRoomSpawn> _enemiesInRoom;
	public List<EnemyRoomSpawn> EnemiesInRoom {

		get { return _enemiesInRoom; }
		set { _enemiesInRoom = value; }

	}

	Enemy[] currentEnemies;

	Boss b;

	void Start() {

		ui = GameObject.FindObjectOfType<UIController> ();

	}

	public void EnteredRoom() {

		IsCurrentlyInRoom = true;
		SpawnEnemies ();

		if (ui != null) {

			ui.ShowRoomNameText (RoomName); // Localisation.GetLocalisedText(RoomName, Localisation.CurrentLocal);

			ui.ShowBossHealth (b);

		}

	}

	public void LeftRoom() {

		IsCurrentlyInRoom = false;
		DespawnEnemies ();

		b = null;

		if (ui != null) {

			ui.ShowBossHealth (b);

		}

	}

	void SpawnEnemies() {

		currentEnemies = GetComponentsInChildren<Enemy> (true);
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

					e.transform.position = trans + ers.SpawnLocation;
					e.gameObject.SetActive (true);

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

			foreach (EnemyRoomSpawn ers in EnemiesInRoom) {

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

			foreach (EnemyRoomSpawn e in EnemiesInRoom) {

				Gizmos.color = (e.Enemy is Boss) ? Color.red : Color.blue;

				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x, 		trans.y + e.SpawnLocation.y + 1f));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x + 1f, 	trans.y + e.SpawnLocation.y));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x, 		trans.y + e.SpawnLocation.y - 1f));
				Gizmos.DrawLine (trans + e.SpawnLocation, new Vector2 (trans.x + e.SpawnLocation.x - 1f, 	trans.y + e.SpawnLocation.y));

			}

		}

	}

}
