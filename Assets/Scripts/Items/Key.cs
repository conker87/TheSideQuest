using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	public KeyLocation location;
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

		Debug.Log ("I HAS HIT SOMMMUT");

		PlayerDetails playerDetails = other.GetComponent<PlayerDetails>();

		if (playerDetails == null) {

			Debug.Log (gameObject + "::Key::OnTriggerEnter2D - This object is not hitting the Player");

		} else {

			playerDetails.IncrementKeyCount(location);

			DisableGameObject (true);

		}

	}

	public void DisableGameObject(bool disable) {

		_isDisabled = disable;

	}

}
