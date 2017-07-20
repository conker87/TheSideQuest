using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Item : MonoBehaviour {
	
	protected Player player;
	protected bool _hasBeenCollected = false;

	protected virtual void Start() {

		// TODO: Item needs to know if it has already been collected during load.

		RenameGameObject ();

	}

	protected virtual void Update() {

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

	protected virtual void RenameGameObject() {

	}

}
