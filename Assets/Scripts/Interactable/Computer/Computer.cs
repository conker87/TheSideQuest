using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable {

	[SerializeField]
	string _computerTitle;
	public string ComputerTitle {

		get { return _computerTitle; } 
		set { _computerTitle = value; }

	}

	[SerializeField][TextArea(3, 10)]
	string[] _computerContents;
	public string[] ComputerContents {

		get { return _computerContents; } 
		set { _computerContents = value; }

	}

	public override void DoInteraction(bool sentFromPlayerInput = false) {

		base.DoInteraction (sentFromPlayerInput);

		if (InteractableID == "") {

			Debug.LogWarning ("Interactable::Switch::DoInteraction -- Interactable::Computer does not have an ID and cannot be added to the SaveFile.");
			_canContinue = false;

		}

		if (ComputerContents.Length == 0) {

			Debug.LogWarning ("Interactable::Switch::DoInteraction -- Interactable::Computer has nothing written in LogContents.");
			_canContinue = false;

		}

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

		UIController.instance.DoDialogue (ComputerContents, ComputerTitle);

	}

}
