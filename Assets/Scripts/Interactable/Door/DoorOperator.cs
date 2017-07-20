using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOperator : Door {

	[SerializeField]
	DoorOpenOperator _doorOpenOperator;
	public DoorOpenOperator dDoorOpenOperator {

		get { return _doorOpenOperator; }
		private set { _doorOpenOperator = value; }

  	}

	int _doorOperatorCount, _doorOperatorCountTotal = 0;

	protected override void Start () {
		
		base.Start ();

		ResetInSeconds = 0f;
		isResetInteractable = false;

	}

	public override void DoInteraction (bool sentFromPlayerInput = false)
	{

		if (IsCurrentlyLocked) {

			print ("Interactable::Switch::DoInteraction -- Interactable is currently locked.");
			_canContinue = false;

		}

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

		switch (dDoorOpenOperator) {

		case DoorOpenOperator.AND:

			DoorOperatorAnd ();
			break;

		case DoorOpenOperator.OR:

			DoorOperatorOr ();
			break;

		}

	}

	void DoorOperatorAnd() {

		DoorState stateToChange = (StartingState == DoorState.CLOSED) ? DoorState.OPENING : DoorState.CLOSING,
			previousState = (StartingState == DoorState.CLOSED) ? DoorState.CLOSING : DoorState.OPENING;

		if (GetOperatorCount () >= _doorOperatorCountTotal) {

			ChangeDoorState (stateToChange);
			print ("Door will open: " + stateToChange.ToString());

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

}

public enum DoorOpenOperator { OR, AND };