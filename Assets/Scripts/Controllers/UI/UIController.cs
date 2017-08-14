using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	#region Singleton

	public static UIController instance = null;

	public LevelDirection CurrentLevelDirection;

	void Awake() {
		
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}

		DontDestroyOnLoad(gameObject);

	}

	#endregion

	public Text Health;
	public string healthy = "green", unhealthy = "navy", healthBarCharacter = "þ";

	public Button saveTest;

	public GameObject BossBarParent;
	public UI_BossHealthBar UI_BossHealthBar;

	// Dialogue
	public UIDialogueController UIDialogueController;

	// Room Name
	public UIRoomController UIRoomController;

	// Pause
	public UIPauseController UIPauseController;

	public UIInformationController UIInformationController;

	Coroutine disableElement;

	void Start() { 

		if (saveTest != null) {

			saveTest.onClick.AddListener (GameSaveController.SaveGame);


		}

	}

	public void ShowBossHealth (Enemy e) {

		BossBarParent.gameObject.SetActive ((e != null));
		UI_BossHealthBar.SetEnemy (e);

	}

	public void ShowCollectedText(string collectedItemName) {
		
		UIInformationController.gameObject.SetActive (true);
		UIInformationController.Activate (collectedItemName);

	}

	public void ShowPauseMenu() {

		UIPauseController.SetPauseState ("MAIN");

	}

	public void DoRoomName(string roomName) {

		UIRoomController.ShowRoomNameText (roomName);

	}

	public void DoDialogue(string[] contents, string title) {

		UIDialogueController.ShowDialogueBox (contents, title);

	}

	public static IEnumerator FadeTextElement(Text element, float totalTime, Color startColor, Color endColor) {

		if (element == null) {

			yield return null;

		}

		float elapsedTime = 0f;

		while(elapsedTime < totalTime) {
			
			elapsedTime += Time.deltaTime;
			element.color = Color.Lerp(startColor, endColor, elapsedTime/totalTime);
			yield return null;

		}

		element.gameObject.SetActive (false);

	}

	public static IEnumerator DisableElement(GameObject element, float seconds) {

		if (element == null) {

			print (element + " is null");

			yield return null;

		}

		yield return new WaitForSeconds (seconds);

		element.SetActive (false);

	}

}
