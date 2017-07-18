using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text RoomName;

	[SerializeField]
	float roomNameShowTime = 3f;

	Coroutine disableElement;

	public void ShowRoomNameText(string roomName) {

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
