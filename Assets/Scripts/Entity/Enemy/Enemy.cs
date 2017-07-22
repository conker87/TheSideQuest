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

		HasBeenKilled = true;
		gameObject.SetActive (false);

	}

}
