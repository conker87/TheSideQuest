﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : Item {

	[SerializeField]
	LevelDirection _keyLocation;
	public LevelDirection KeyLocation {

		get { return _keyLocation; }
		set { _keyLocation = value; }

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			if (player.SetArtifact(KeyLocation)) {

				DisableGameObject (true);

			}

		}

	}

}
