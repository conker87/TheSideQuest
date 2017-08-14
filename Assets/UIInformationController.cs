using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInformationController : MonoBehaviour {

	public Text informationText;
	public float textFadeTime = 3f;

	public void Activate(string informationString) {

		Invoke ("Deactivate", 1f);
		gameObject.SetActive (true);
		informationText.color = new Color (1f, 1f, 1f, 1f);
		informationText.text = string.Format("You have collected {0}", informationString);


	}

	void Deactivate() {

		// Coroutine c = ;

		StartCoroutine(UIController.FadeTextElement (informationText, textFadeTime, new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0f)));

	}

}
