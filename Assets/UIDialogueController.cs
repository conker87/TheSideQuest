using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueController : MonoBehaviour {

	[SerializeField]
	public List<Text> listOfTextElements = new List<Text>();

	Queue<string> dialogue = new Queue<string> ();

	public void ShowDialogueBox (string[] contents, string title = "") {

		if (title != "") {

			title = title.ToUpper ();

		}

		Player.instance.IsCurrentlyBusy = true;
		gameObject.SetActive (Player.instance.IsCurrentlyBusy);

		dialogue.Clear ();
		foreach (string log in contents) {

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

	}

	public void EndDialogue() {

		Player.instance.IsCurrentlyBusy = false;
		gameObject.SetActive (Player.instance.IsCurrentlyBusy);

	}

}
