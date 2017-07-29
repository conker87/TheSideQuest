using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_ValidateInteractableIDs : MonoBehaviour {

	GameObject worldRoot;

	List<Interactable> interactables = new List<Interactable>();
	List<Interactable> foundInteractables = new List<Interactable>();

	// Use this for initialization
	void Start () {

		worldRoot = GameObject.Find ("World");

	}
	
	public void ValidateInteractableIDs(GameObject rootWorld) {

		if (interactables.Count == 0) {

			foreach (Interactable Interactable in rootWorld.GetComponentsInChildren<Interactable>()) {

				interactables.Add (Interactable);

			}

		}

		foundInteractables.Clear ();

		foreach (Interactable Interactable in interactables) {

			foreach (Interactable InnerInteractable in interactables) {

				if (InnerInteractable.InteractableID == Interactable.InteractableID &&
				    InnerInteractable.GetInstanceID () != Interactable.GetInstanceID ()) {

					foundInteractables.Add (InnerInteractable);

					Debug.LogError (InnerInteractable.name + " at: " + InnerInteractable.transform.position + " and " +
									Interactable.name + " at: " + Interactable.transform.position +
									" have the same InteractableID, this should not happen!");

				}

			}

		}

	}

	void OnDrawGizmos() {

		for (int i = 0; i < foundInteractables.Count; i++) {

			if (i % 2 != 0)
				continue;

			if (i == (foundInteractables.Count - 1))
				continue;

			Gizmos.color = Color.red;
			Gizmos.DrawLine(foundInteractables[i].transform.position, foundInteractables[i+1].transform.position);

		}

	}

}
