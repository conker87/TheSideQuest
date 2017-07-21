﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text RoomName;

	public Text Health;
	public string healthy = "green", unhealthy = "navy", healthBarCharacter = "þ";

	public Button saveTest;

	Player player;

	[SerializeField]
	float roomNameShowTime = 3f;

	Coroutine disableElement;

	void Start() { 

		player = GameObject.FindObjectOfType<Player>();

		if (saveTest != null) {

			saveTest.onClick.AddListener (GameSaveController.SaveGame);

		}


	}

	void Update() {

		ShowHealthText();

	}

	void ShowHealthText() {

		if (Health == null) {

			return;

		}

		string healthString = "Health: ";
		string currentColor = "";

		for (int i = 0; i < player.MaximumHealth; i++) {

			currentColor = (i >= player.CurrentHealth) ? unhealthy : healthy;

			healthString += "<color=" + currentColor + ">" + healthBarCharacter + "</color>";

		}

		Health.text = healthString;

	}

	public void ShowRoomNameText(string roomName) {

		if (RoomName == null) {

			return;

		}

		if (disableElement != null) {

			StopCoroutine (disableElement);

		}

		RoomName.text = roomName;
		RoomName.gameObject.SetActive (true);

		disableElement = StartCoroutine(DisableElement (RoomName.gameObject, roomNameShowTime));

	}

	IEnumerator DisableElement(GameObject element, float seconds) {

		if (element == null) {

			print (element + " is null");

			yield return null;

		}

		yield return new WaitForSeconds (seconds);

		element.SetActive (false);

	}

}
