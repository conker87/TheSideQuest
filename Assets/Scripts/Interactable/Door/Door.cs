using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {

	[SerializeField]
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

	[SerializeField]
	LayerMask playerLayerMask;
	[SerializeField]
	float playerCheckRadius = 1.5f;

	float playerCheckTime = 0.5f, playerCheckTimer;

	// This is for the DoorState.CHANGING to determine which final state the door should be in at the end of the Anim.
	protected bool opening = false, canChangeState = true;

	protected override void Start() {

		base.Start ();

	}

	// Update is called once per frame
	protected override void Update () {

		base.Update ();

		DoReset ();

		switch (CurrentState) {

		case DoorState.OPENING:
			print ("DoorState.OPENING");

			if (canChangeState) {
				// anim.SetBool("Opening", true);
				opening = true;
				ChangeDoorState (DoorState.OPEN); // TODO: Change these back to DoorState.CHANGING once Anims are added.
			}
			break;

		case DoorState.CLOSING:
			print ("DoorState.CLOSING");

			CheckForPlayer ();

			if (canChangeState) {
				
				opening = false;
				// anim.SetBool("Closing", true);
				ChangeDoorState (DoorState.CLOSED);

			}
			break;

		case DoorState.CHANGING:
			// if (!anim.IsPlaying) { ChangeDoorState((opening) ? DoorState.OPEN : DoorState.CLOSED); } 
			break;

			// TODO: These to cases need to be removed once Anims are added
		case DoorState.OPEN:
			GetComponent<SpriteRenderer> ().enabled = false;
			tag = "Interactable";
			//GetComponent<Collider2D> ().enabled = false;
			break;

		case DoorState.CLOSED:
			GetComponent<SpriteRenderer> ().enabled = true;
			tag = "Untagged";
			//GetComponent<Collider2D> ().enabled = true;
			break;

		}

	}

	public override void DoInteraction (bool sentFromPlayerInput = false)
	{

		base.DoInteraction (sentFromPlayerInput);

//		if (!_canContinue) return;
//
//		if (IsOneUseOnly) HasBeenUsedOnce = true;
//
//		DoorState stateToChange = (StartingState == DoorState.CLOSED) ? DoorState.OPENING : DoorState.CLOSING,
//		previousState = (StartingState == DoorState.CLOSED) ? DoorState.CLOSING : DoorState.OPENING;
//
//		if (CurrentState == StartingState) {
//
//			StartResetTimer ();
//			ChangeDoorState (stateToChange);
//
//		} else {
//
//			ChangeDoorState (previousState);
//
//		}

	}

	protected void CheckForPlayer() {

		if (Time.time > playerCheckTimer) {

			playerCheckTimer = Time.time + playerCheckTime;

			Vector2 currentPosition = transform.position;
			Collider2D col;

			if ((col = Physics2D.OverlapCircle (currentPosition, playerCheckRadius, playerLayerMask)) != null) {

				if (col.GetComponent<Player> () != null) {

					print ("Player has been found within range.");
					canChangeState = false;
					return;

				}

			}

			canChangeState = true;

		}

	}

	protected void StartResetTimer() {

		if (isResetInteractable) {

			_resetTimer = Time.time + ResetInSeconds;

		}

	}

	protected virtual void DoReset() {

		if (isResetInteractable && CurrentState != StartingState && Time.time > _resetTimer) {

			DoorState state = (StartingState == DoorState.CLOSED) ? DoorState.CLOSING : DoorState.OPENING;
			_resetTimer = Mathf.Infinity;

			ChangeDoorState (state);


		}

	}

	public void ChangeDoorState(DoorState state) {

		CurrentState = state;

  	}

	void OnDrawGizmos() {

		Gizmos.DrawWireSphere (transform.position, playerCheckRadius);

	}

}

public enum DoorState { CLOSED, OPENING, OPEN, CLOSING, CHANGING };