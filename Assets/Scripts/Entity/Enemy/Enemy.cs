using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	[SerializeField]
	bool _hasBeenKilled;
	public bool HasBeenKilled {

		get { return _hasBeenKilled; }
		set { _permanentlyKillable = value; }

	}

	[SerializeField]
	bool _permanentlyKillable;
	public bool PermanentlyKillable {

		get { return _permanentlyKillable; }
		set { _permanentlyKillable = value; }

	}

	protected override void Start() {

		GameSaveController.EnemiesInWorld.Add (this);

	}

}
