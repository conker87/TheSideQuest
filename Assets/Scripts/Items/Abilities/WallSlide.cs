using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : Item {

	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			player.WallSlide = true;
			DisableGameObject (true);

		}

	}

}
