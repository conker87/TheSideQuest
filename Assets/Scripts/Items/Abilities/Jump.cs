using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Item {

	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			player.Jump = true;
			DisableGameObject (true);

		}

	}

}
