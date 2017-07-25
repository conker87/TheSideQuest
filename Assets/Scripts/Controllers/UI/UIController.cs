using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	#region Singleton

	public static UIController instance = null;

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


	Coroutine disableElement;

	void Start() { 

		if (saveTest != null) {

			saveTest.onClick.AddListener (GameSaveController.SaveGame);


		}

	}

	void Update() {

		ShowHealthText();

	}

	public void ShowBossHealth (Enemy e) {

		BossBarParent.gameObject.SetActive ((e != null));
		UI_BossHealthBar.SetEnemy (e);

	}

	void ShowHealthText() {

		if (Health == null) {

			return;

		}

		string healthString = "Health: ";
		string currentColor = "";

		for (int i = 0; i < Player.instance.MaximumHealth; i++) {

			currentColor = (i >= Player.instance.CurrentHealth) ? unhealthy : healthy;

			healthString += "<color=" + currentColor + ">" + healthBarCharacter + "</color>";

		}

		Health.text = healthString;

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

	public IEnumerator DisableElement(GameObject element, float seconds) {

		if (element == null) {

			print (element + " is null");

			yield return null;

		}

		yield return new WaitForSeconds (seconds);

		element.SetActive (false);

	}

}
