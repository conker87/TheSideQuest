using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyNumberController : MonoBehaviour {

	public Image keycardImage;
	public Text keycardAmountText;
	int currentLevelDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {

		if (keycardImage == null || keycardAmountText == null) {

			return;

		}

		currentLevelDirection = (int)UIController.instance.CurrentLevelDirection;

		keycardImage.color = Constants.LevelDirectionColor [currentLevelDirection];
		keycardAmountText.text = Player.instance.Keys [currentLevelDirection].ToString();

	}

}
