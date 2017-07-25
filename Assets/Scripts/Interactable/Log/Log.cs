using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Interactable {

	// TODO: The UI for these need adding
	// TODO: A JSON (?) file with all Logs need to be added along with Localisations.

	[SerializeField]
	string _logTitle;
	public string LogTitle {

		get { return _logTitle; } 
		set { _logTitle = value; }

	}

	[SerializeField][TextArea(3, 10)]
	string[] _logContents;
	public string[] LogContents {

		get { return _logContents; } 
		set { _logContents = value; }

	}

	public override void DoInteraction(bool sentFromPlayerInput = false) {

		base.DoInteraction (sentFromPlayerInput);

		if (InteractableID == "") {

			Debug.LogWarning ("Interactable::Switch::DoInteraction -- Interactable::Log does not have an ID and cannot be added to the SaveFile.");
			_canContinue = false;

		}

		if (LogContents.Length == 0) {

			Debug.LogWarning ("Interactable::Switch::DoInteraction -- Interactable::Log has nothing written in LogContents.");
			_canContinue = false;

		}

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

		// TODO: Maybe make a DialogueManager that manages certain dialogues?

		UIController.instance.DoDialogue (LogContents, LogTitle);

		if (!GameSaveController.LogsFoundByPlayer.ContainsKey(InteractableID)) {
			GameSaveController.LogsFoundByPlayer.Add (InteractableID, true);
		}

	}

}
