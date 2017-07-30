using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarController : MonoBehaviour {

	public Image UIHealthBar;
	float currentHealthPercentage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		currentHealthPercentage = (float) Player.instance.CurrentHealth / (float) Player.instance.MaximumHealth;

		UIHealthBar.fillAmount = currentHealthPercentage;

		UIHealthBar.color = Color.Lerp (Color.red, Color.green, currentHealthPercentage);

		if (currentHealthPercentage <= 0.25f) {

			float lerpingAlpha = Mathf.PingPong (Time.time, 1f);

			Color currentColor = UIHealthBar.color;

			UIHealthBar.color = new Color (currentColor.r, currentColor.g, currentColor.b, lerpingAlpha);

		} else {

			Color currentColor = UIHealthBar.color;
			UIHealthBar.color = new Color (currentColor.r, currentColor.g, currentColor.b, 255f);

		}

	}


}
