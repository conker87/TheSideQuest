using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueController : MonoBehaviour {

	[SerializeField]
	public List<Text> listOfTextElements = new List<Text>();

	public Button NextButton;
	UIButtonController UIBC = null;

	Queue<string> dialogue = new Queue<string> ();

	public void ShowDialogueBox (string[] contents, string title = "") {

		if (title != "") {

			title = title.ToUpper ();

		}

		Player.instance.IsCurrentlyBusy = true;
		gameObject.SetActive (Player.instance.IsCurrentlyBusy);

		if (UIBC == null) {
			UIBC = NextButton.GetComponent<UIButtonController> ();
		}

		dialogue.Clear ();
		foreach (string log in contents) {

			if (log.Length > 1024) {

				Debug.LogWarning ("UIDialogueController::ShowDialogueBox -- One of " + title + "'s contents is more than 1,024 characters");

			}

			dialogue.Enqueue (log);

		}

		// TODO: These need to probably get their data from Localisation, you'd probably give in LocalisationIDs.
		listOfTextElements [0].text = title;

		DisplayNextSentence ();

	}

	public void DisplayNextSentence() {

		if (dialogue.Count == 0) {

			EndDialogue ();
			return;

		}

		listOfTextElements [1].text = dialogue.Dequeue();
		UIBC.ButtonLabel.text = (dialogue.Count > 0) ? "NEXT" : "EXIT";

	}

	public void EndDialogue() {

		Player.instance.IsCurrentlyBusy = false;
		gameObject.SetActive (Player.instance.IsCurrentlyBusy);

	}

}
