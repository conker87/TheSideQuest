using UnityEngine;

[System.Serializable]
public class RoomSpawnEnemy {

	public RoomSpawnEnemy(Enemy e, Vector2 spawn, bool killed, bool perm, int id) {

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
