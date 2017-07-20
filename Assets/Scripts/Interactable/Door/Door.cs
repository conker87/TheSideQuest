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
	DoorOperator _doorOperator;
	public DoorOperator DoorOperator {

		get { return _doorOperator; }
		private set { _doorOperator = value; }

	}

	[SerializeField]
	float _resetInSeconds = 0;
	float _resetTimer;
	bool _isResetDoor;
	public float ResetInSeconds {

		get { return _resetInSeconds; }
		set { _resetInSeconds = value; }

	}

	// This is for the DoorState.CHANGING to determine which final state the door should be in at the end of the Anim.
	bool opening = false;

	int _doorOperatorCount, _doorOperatorCountTotal = 0;

	// Use this for initialization
	protected override void Start () {

	}

	// Update is called once per frame
	protected override void Update () {

		base.Update ();

		switch (CurrentState) {

		case DoorState.OPENING:
			print ("DoorState.OPENING");
			// anim.SetBool("Opening", true);
			opening = true;
			ChangeDoorState (DoorState.OPEN); // TODO: Change these back to DoorState.CHANGING once Anims are added.
			break;

		case DoorState.CLOSING:
			print ("DoorState.CLOSING");
			opening = false;
			// anim.SetBool("Closing", true);
			ChangeDoorState (DoorState.CLOSED);
			break;

		case DoorState.CHANGING:
			// if (!anim.IsPlaying) { ChangeDoorState((opening) ? DoorState.OPEN : DoorState.CLOSED); } 
			break;

			// TODO: These to cases need to be removed once Anims are added
		case DoorState.OPEN:
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<Collider2D> ().enabled = false;
			break;

		case DoorState.CLOSED:
			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<Collider2D> ().enabled = true;
			break;

		}

	}

	public override void DoInteraction (bool sentFromPlayerInput = false)
	{
		
		base.DoInteraction (sentFromPlayerInput);

		switch (DoorOperator) {

		case DoorOperator.AND:

			DoorOperatorAnd ();
			break;

		case DoorOperator.OR:

			DoorOperatorOr ();
			break;

		}

	}

	void DoorOperatorAnd() {

		DoorState stateToChange = (StartingState == DoorState.CLOSED) ? DoorState.OPENING : DoorState.CLOSING,
			previousState = (StartingState == DoorState.CLOSED) ? DoorState.CLOSING : DoorState.OPENING;

		if (GetOperatorCount () >= _doorOperatorCountTotal) {

			ChangeDoorState (stateToChange);
			print ("Door will open");

		} else {

			if (CurrentState != StartingState) {

				ChangeDoorState (previousState);
				print ("Do closed?");

			}

		}

	}

	void DoorOperatorOr() {

		DoorState stateToChange = (StartingState == DoorState.CLOSED) ? DoorState.OPENING : DoorState.CLOSING,
		previousState = (StartingState == DoorState.CLOSED) ? DoorState.CLOSING : DoorState.OPENING;

		if (GetOperatorCount () > 0) {

			ChangeDoorState (stateToChange);
			print ("Door will open");

		} else {

			if (CurrentState != StartingState) {
			
				ChangeDoorState (previousState);
				print ("Do closed?");

			}

		}

  	}

	#region Operator Counts

	public void IncrementTotalOperatorCount() {

		_doorOperatorCountTotal++;

	}

	public int GetTotalOperatorCount() {

		return _doorOperatorCountTotal;

	}

	public void IncrementOperatorCount() {

		_doorOperatorCount++;

	}

	public void DecrementOperatorCount() {

		_doorOperatorCount--;

	}

	public int GetOperatorCount() {

		return _doorOperatorCount;

	}

	public void ResetOperatorCount () {

		_doorOperatorCount = 0;
  
	}

	#endregion

	public void ChangeDoorState(DoorState state) {

		CurrentState = state;

  	}

}

public enum DoorOperator { OR, AND };
public enum DoorState { CLOSED, OPENING, OPEN, CLOSING, CHANGING };