using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : Interactable {

	// Use this for initialization
	protected override void Start () {

		IsCurrentlyInteractable = true;
		IsOneUseOnly = HasBeenUsedOnce = false;

		GameSaveController.SaveStationLocations.Add (this);

		if (InteractableID == "") {

			Debug.LogWarning ("Interactable::SaveStation -- '" + gameObject.name + " (" + transform.position.x + "," + transform.position.y
				+ ") has now InteractableName and therefore no ID, this will royallu fuck up saving games.");

		}

	}
	
	public override void DoInteraction(bool sentFromPlayerInput = false) {

		CheckInteraction ();

		if (!_canContinue) return;

		GameSaveController.SaveGame (InteractableID);

	}

}
