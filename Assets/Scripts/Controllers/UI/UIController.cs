﻿using System.Collections;
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

	public Button loadTest, saveTest;

	public GameObject BossBarParent;
	public UI_BossHealthBar UI_BossHealthBar;

	// Dialogue
	public UIDialogueController UIDialogueController;

	// Room Name
	public UIRoomController UIRoomController;

	// Pause
	public UIPauseController UIPauseController;

	public UIInformationController UIInformationController;

	void Start() { 

		if (loadTest != null) {

			loadTest.onClick.AddListener (GameSaveController.LoadGame);

		}

		if (saveTest != null) {

			saveTest.onClick.AddListener (GameSaveController.SaveGame);

		}

	}

	public void ShowBossHealth (Enemy e) {

		BossBarParent.gameObject.SetActive ((e != null));
		UI_BossHealthBar.SetEnemy (e);

	}

	public void ShowInformationText(string collectedItemName) {
		
		UIInformationController.Activate (collectedItemName);

	}

	public void ShowInformationText(string collectedItemName, Color startColorOverride, Color endColorOverride) {

		UIInformationController.Activate (collectedItemName, startColorOverride, endColorOverride);

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

}
