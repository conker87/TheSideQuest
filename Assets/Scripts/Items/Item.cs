﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Item : MonoBehaviour {
	
	protected Player player;
	protected string _itemName;
	public string ItemName {

		get { return _itemName; }
		protected set { _itemName = value; }

	}
	protected bool _hasBeenCollected = false;
	public bool HasBeenCollected {

		get { return _hasBeenCollected; }
		set { _hasBeenCollected = value; }

	}

	protected virtual void Start() {

		// TODO: Item needs to know if it has already been collected during load.

		RenameGameObject ();

		if (string.IsNullOrEmpty (ItemName)) {
			ItemName = name;
		}

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

	protected void SendInformationText(string information) {

		UIController.instance.ShowInformationText (information);

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
