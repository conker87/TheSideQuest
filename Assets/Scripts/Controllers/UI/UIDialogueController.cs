using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueController : MonoBehaviour {

	[SerializeField]
	public List<Text> listOfTextElements = new List<Text>();

	public Button NextButton;
	UIButtonController UIBC = null;

	string currentSentence;

	Coroutine typeSentence = null;

	Queue<string> dialogue = new Queue<string> ();

	void Update() {

		// TODO: Change this to allow the use of InputManager.GetButton[]
		if (Input.GetKeyDown (KeyCode.E)) {

			NextButton.onClick.Invoke ();

		}

		if (Player.instance.IsCurrentlyBusy && Input.GetKeyDown (KeyCode.Escape)) {

			EndDialogue ();

		}

	}

	public void ShowDialogueBox (string[] contents, string title = "") {

		if (title != "") {

			title = title.ToUpper ();

		}

		Player.instance.IsCurrentlyBusy = true;
		gameObject.SetActive (true);

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

	IEnumerator TypeSentence (string sentence) {

		listOfTextElements [1].text = "";

		foreach (char letter in sentence) {

			listOfTextElements [1].text += letter;
			yield return new WaitForSeconds(1f/64f);

		}


	}

	public void DisplayNextSentence() {

		if (dialogue.Count == 0) {

			EndDialogue ();
			return;

		}

		if (typeSentence != null && listOfTextElements [0].text != currentSentence) {

			if (typeSentence != null) {
				StopCoroutine (typeSentence);
			}
			typeSentence = null;

			listOfTextElements [1].text = currentSentence;
			return;

		}

		currentSentence = dialogue.Dequeue ();

		if (typeSentence != null) {
			StopCoroutine (typeSentence);
		}

		typeSentence = StartCoroutine (TypeSentence (currentSentence));

		//listOfTextElements [1].text = dialogue.Dequeue();
		UIBC.ButtonLabel.text = (dialogue.Count > 0) ? "NEXT" : "EXIT";

	}

	public void EndDialogue() {

		StopAllCoroutines ();
		typeSentence = null;
		dialogue.Clear ();
		Player.instance.IsCurrentlyBusy = false;
		gameObject.SetActive (false);

	}

}
