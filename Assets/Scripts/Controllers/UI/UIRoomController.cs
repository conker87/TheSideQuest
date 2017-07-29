using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoomController : MonoBehaviour {
	
	Coroutine disableElement;
	[SerializeField]
	float roomNameShowTime = 3f;

	public Text RoomNameText;

	public void ShowRoomNameText(string roomName) {

		if (roomName == "") {

			return;

		}

		if (disableElement != null) {

			StopCoroutine (disableElement);

		}

		RoomNameText.text = roomName;
		RoomNameText.gameObject.SetActive (true);

		disableElement = StartCoroutine(ClearTextElement(roomNameShowTime));

	}

	public IEnumerator ClearTextElement(float seconds) {

		yield return new WaitForSeconds (seconds);

		RoomNameText.text = "";

	}

}
