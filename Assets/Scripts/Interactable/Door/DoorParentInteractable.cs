using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorParentInteractable : MonoBehaviour {

	public Door doorParent;

	void DisableInteractability() {

		doorParent.IsCurrentlyBusy = true;

	}

	void EnableInteractability() {

		doorParent.IsCurrentlyBusy = false;

	}

}
