using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {

	/*[SerializeField]
	DoorState _startingState;
	public DoorState StartingState {

		get { return _startingState; } 
		set { _startingState = value; } 

	}

	[SerializeField]
	DoorState _currentState;
	public DoorState CurrentState {

		get { return _currentState; } 
		set { _currentState = value; } 

  	}
	*/

	[SerializeField]
	LayerMask playerLayerMask;
	[SerializeField]
	float playerCheckRadius = 1.5f;

	[SerializeField]
	bool _startingIsOn;
	public bool StartingIsOn {

		get { return _startingIsOn; }
		set { _startingIsOn = value; }

	}

	[SerializeField]
	bool _isOn;
	public bool IsOn {

		get { return _isOn; }
		set { _isOn = value; }

	}

	float playerCheckTime = 0.5f, playerCheckTimer;

	Animator anim;

	protected override void Start() {

		base.Start ();

		if (SaveStateToFile) {

			GameSaveController.DoorsInWorld.Add (this);

		}

		anim = GetComponent<Animator> ();

	}

	// Update is called once per frame
	protected override void Update () {

		base.Update ();

		DoReset ();

		anim.SetBool ("isOn", IsOn);

	}

	public override void DoInteraction (bool sentFromPlayerInput = false) {

		CheckInteraction ();

		if (!_canContinue) {
			return;
		}

		if (IsOneUseOnly) {
			HasBeenUsedOnce = true;
		}

		switch (IsOn) {

		case true:

			if (!CheckForPlayerInRadius ()) {
				IsOn = false;
			}
			break;

		case false:

			IsOn = true;

			if (isResetInteractable) {

				StartResetTimer ();

			}

			break;

		}

	}

	public void ForceDoorState(bool force) {

		IsOn = force;

		if (isResetInteractable) {

			StartResetTimer ();

		}

	}

	protected bool CheckForPlayerInRadius(bool checkAgain = true) {

		if (Time.time < playerCheckTimer) {

			return true;

		}

		playerCheckTimer = Time.time + playerCheckTime;

		Vector2 currentPosition = transform.position;
		Collider2D col = Physics2D.OverlapCircle (currentPosition, playerCheckRadius, playerLayerMask);

		if (col != null && col.GetComponent<Player> () != null) {
			
			Debug.Log (string.Format("Interactable::Door::CheckForPlayerInRadius -- Player was found within {0} units of {1}!",
				playerCheckRadius, InteractableID));

			if (checkAgain) {
				Invoke ("CheckAgainForPlayer", playerCheckTime + 0.01f);
			}

			return true;

		}

		return false;

}

	protected void CheckAgainForPlayer() {

		if (!CheckForPlayerInRadius ()) {

			IsOn = !IsOn;

		}

	}

	protected void StartResetTimer() {

		if (isResetInteractable) {

			_resetTimer = Time.time + ResetInSeconds;

		}

	}

	protected virtual void DoReset() {

		if (isResetInteractable && IsOn != StartingIsOn && Time.time > _resetTimer ) {

			bool state = StartingIsOn;

			if (!CheckForPlayerInRadius ()) {

				_resetTimer = Mathf.Infinity;

				ChangeDoorState (state);

			}

		}

	}

	public void ChangeDoorState(bool state) {

		IsOn = state;

  	}

	void OnDrawGizmos() {

		Gizmos.DrawWireSphere (transform.position, playerCheckRadius);

	}

}

public enum DoorState { CLOSED, OPENING, OPEN, CLOSING, CHANGING };