using UnityEngine;

[System.Serializable]
public class EnemyItemSpawn  {

	public Item itemToSpawn;
	[Range(0, 100)]
	public float spawnChance;
	[Range(1, 15)]
	public int maximumSpawns;
	public bool overrideSpawnLocation = false;
	public bool spawnLocationRelative = false;
	public Vector2 overrideSpawnLocationPosition;

}
