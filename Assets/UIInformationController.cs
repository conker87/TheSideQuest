using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInformationController : MonoBehaviour {

	public Text informationText;
	public float textShowTime = 3f, textFadeTime = 3f;

	public Color startColor = new Color (1f, 1f, 1f, 1f), endColor = new Color (1f, 1f, 1f, 0f);

	public void Activate(string informationString) {

		StopAllCoroutines ();

		StartCoroutine(Constants.FadeTextElement (informationText, textFadeTime, startColor, endColor, true, textShowTime));

		informationText.color = startColor;
		informationText.text = informationString;

	}

	public void Activate(string informationString, Color startColorOverride, Color endColorOverride) {

		StopAllCoroutines ();

		StartCoroutine(Constants.FadeTextElement (informationText, textFadeTime, startColorOverride, endColorOverride, true, textShowTime));

		informationText.color = startColorOverride;
		informationText.text = informationString;

	}

}
