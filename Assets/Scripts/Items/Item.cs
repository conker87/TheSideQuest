using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	
	protected Player player;
	protected bool _hasBeenCollected = false;

	void Start() {

		// TODO: Item needs to know if it has already been collected during load.

	}

	void Update() {

		if (_hasBeenCollected) {

			gameObject.SetActive (false);

		}

	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {

		print ("Item::OnTriggerEnter2D -- This has not been correctly overridden for item: " + gameObject);

	}

	public void DisableGameObject(bool disable) {

		_hasBeenCollected = disable;

	}

}
