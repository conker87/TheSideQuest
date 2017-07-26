using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseController : MonoBehaviour {

	public GameObject UIPauseBorder;
	public UIPauseMain UIPauseMain;
	public UIPauseGameplay UIPauseGameplay;
	public UIPauseVideo UIPauseVideo;
	public UIPauseSound UIPauseSound;
	public UIPauseControls UIPauseControls;

	[SerializeField]
	string _currentPauseState = "NONE";
	public string CurrentPauseState {

		get { return _currentPauseState; }
		set { _currentPauseState = value; }

	}

	void Update () {

		switch (CurrentPauseState) {

		case "NONE":
			DisableAllMenus ();

			UIPauseBorder.SetActive (false);

			break;

		case "MAIN":
			DisableAllMenus ();

			Player.instance.IsCurrentlyBusy = true;

			UIPauseBorder.SetActive (true);
			UIPauseMain.gameObject.SetActive(true);

			break;

		case "GAMEPLAY":
			DisableAllMenus ();
			UIPauseGameplay.gameObject.SetActive (true);

			break;

		case "VIDEO":
			DisableAllMenus ();
			UIPauseVideo.gameObject.SetActive(true);

			break;

		case "SOUND":
			DisableAllMenus ();
			UIPauseSound.gameObject.SetActive(true);

			break;

		case "CONTROLS":
			DisableAllMenus ();
			UIPauseControls.gameObject.SetActive(true);

			break;

		}

	}

	public void DisableAllMenus() {

		UIPauseMain.gameObject.SetActive (false);
		UIPauseGameplay.gameObject.SetActive (false);
		UIPauseVideo.gameObject.SetActive(false);
		UIPauseSound.gameObject.SetActive(false);
		UIPauseControls.gameObject.SetActive(false);

	}

	public void SetPauseState(string pauseState) {

		CurrentPauseState = pauseState;

	}

	public void ReturnToGame() {

		SetPauseState ("NONE");
		Player.instance.IsCurrentlyBusy = false;

	}

	public void QuitGame() {

		// TODO: This needs to be changed to load the Main Menu.
		Application.Quit();

	}

}
