using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthOrb : Item {
	
	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			player.IncreaseMaximumHealth ();
			DisableGameObject (true);

		}

	}

}
