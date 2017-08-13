using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : Item {

	float destroyInTime = 15f;

	protected override void Start () {

		Invoke ("DestroyDroppedItem", Random.Range(destroyInTime - 5f, destroyInTime + 5f));

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			player.HealCurrentHealth ();
			DisableGameObject (true, true);

		}

	}

	void DestroyDroppedItem() {

		DisableGameObject (true, true);

	}

}
