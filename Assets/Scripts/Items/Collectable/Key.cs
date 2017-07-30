using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item {

	[SerializeField]
	LevelDirection _keyLocation;
	public LevelDirection KeyLocation {

		get { return _keyLocation; }
		set { _keyLocation = value; }

	}

	protected override void Start () {

		base.Start ();

		GetComponent<SpriteRenderer> ().color = Constants.LevelDirectionColor [(int) KeyLocation];

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			if (player.IncrementKeyCount (KeyLocation)) {

				DisableGameObject (true);

			}

		}

	}

	protected override void RenameGameObject() {

		string name;

		name = "Key_(" + transform.position.x + "," + transform.position.y + ")_" + KeyLocation + "_" + GetInstanceID ();

		gameObject.name = name;

	}

}
