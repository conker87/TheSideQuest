using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorParentInteractable : MonoBehaviour {

	public Door doorParent;

	void DisableInteractability() {

		doorParent.IsCurrentlyInteractable = false;

	}

	void EnableInteractability() {

		doorParent.IsCurrentlyInteractable = true;

	}

}
