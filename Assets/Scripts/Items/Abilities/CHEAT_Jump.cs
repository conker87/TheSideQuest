using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHEAT_Jump : Item {

	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			player.CHEAT_Jump = true;
			DisableGameObject (true);

		}

	}

}
