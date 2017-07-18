using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	[SerializeField]
	KeyLocation _keyLocation;
	public KeyLocation KeyLocation {

		get { return _keyLocation; }
		set { _keyLocation = value; }

	}

	bool _isDisabled = false;

	void Start() {

		// TODO: Key needs to know if it has already been collected during load.

	}

	void Update() {

		if (_isDisabled) {

			gameObject.SetActive (false);

		}

	}

	void OnTriggerEnter2D(Collider2D other) {

		Player playerDetails = other.GetComponent<Player>();

		if (playerDetails != null) {

			if (playerDetails.IncrementKeyCount (KeyLocation)) {

				DisableGameObject (true);

			}

		}

	}

	public void DisableGameObject(bool disable) {

		_isDisabled = disable;

	}

}
