using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyRoomSpawn {

	public EnemyRoomSpawn(Enemy e, Vector2 spawn, bool killed, bool perm, int id) {

		Enemy = e;
		SpawnLocation = spawn;
		HasBeenKilled = killed;
		PermanentlyKillable = perm;
		EnemyID = id;

	}

	public Enemy Enemy;
	public Vector2 SpawnLocation;
	public bool HasBeenKilled;
	public bool PermanentlyKillable;
	public int EnemyID;

}
