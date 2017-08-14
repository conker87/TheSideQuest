using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Item : MonoBehaviour {
	
	protected Player player;
	protected bool _hasBeenCollected = false;
	public bool HasBeenCollected {

		get { return _hasBeenCollected; }
		protected set { _hasBeenCollected = value; }

	}

	protected virtual void Start() {

		// TODO: Item needs to know if it has already been collected during load.

		RenameGameObject ();

		GameSaveController.ItemsInWorld.Add (this);

	}

	protected virtual void Update() {

		if (_hasBeenCollected) {

			gameObject.SetActive (false);

		}

	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {

		print ("Item::OnTriggerEnter2D -- This has not been correctly overridden for item: " + gameObject);

	}

	protected void SendCollectedText(string itemName) {

		UIController.instance.ShowCollectedText (itemName);

	}

	public void DisableGameObject(bool disable, bool destroy = false) {

		if (destroy) {

			Destroy (gameObject);
			return;

		}

		HasBeenCollected = disable;

	}

	protected virtual void RenameGameObject() {

	}

}
