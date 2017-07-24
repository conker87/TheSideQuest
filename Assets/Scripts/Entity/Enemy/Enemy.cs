using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	[SerializeField]
	int _weaknessModifier = 1;
	public int WeaknessModifier {

		get { return _weaknessModifier; }
		set { _weaknessModifier = value; }

	}

	[SerializeField]
	bool _hasBeenKilled;
	public bool HasBeenKilled {

		get { return _hasBeenKilled; }
		set { _hasBeenKilled = value; }

	}

	[SerializeField]
	bool _permanentlyKillable;
	public bool PermanentlyKillable {

		get { return _permanentlyKillable; }
		set { _permanentlyKillable = value; }

	}

	[SerializeField]
	EnemyItemSpawn[] _enemyItemSpawn;
	public EnemyItemSpawn[] EnemyItemSpawn {

		get { return _enemyItemSpawn; }
		set { _enemyItemSpawn = value; }

	}


	protected override void Start() {

		base.Start ();

		GameSaveController.EnemiesInWorld.Add (this);

		TOTAL_MAXIMUM_HEALTH = 10000000;

	}

	protected override void Update() {

		base.Update ();

		if (CurrentHealth < 1) {

			EnemyDeath ();

		}

	}

	protected virtual void EnemyDeath () {

		IterateThroughItemSpawn ();

		HasBeenKilled = true;
		gameObject.SetActive (false);

	}

	protected virtual void IterateThroughItemSpawn() {

		if (EnemyItemSpawn == null || EnemyItemSpawn.Length == 0) {

			return;

		}

		foreach (EnemyItemSpawn eis in EnemyItemSpawn) {

			for (int i = 0; i < eis.maximumSpawns; i++) {

				if (Random.value * 100 < eis.spawnChance) {

					Vector2 positionToSpawnAt = (eis.overrideSpawnLocation) ? eis.overrideSpawnLocationPosition :
						new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f));

					positionToSpawnAt = (!eis.spawnLocationRelative) ? positionToSpawnAt : new Vector2 (transform.position.x + positionToSpawnAt.x, transform.position.y + positionToSpawnAt.y);

					Instantiate (eis.itemToSpawn, positionToSpawnAt, Quaternion.identity);

				} else { 

					break;

				}

			}

		}

	}

}
