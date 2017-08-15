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

		IsCurrentlyLocked = !IsOn;

	}

	public override void DoInteraction (bool sentFromPlayerInput = false)
	{
		CheckInteraction ();

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

		if (sentFromPlayerInput && !IsCurrentlyLocked) {

			UIController.instance.ShowInformationText ("This device is closed elsewhere.", new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 0f));

		}

		switch (dDoorOpenOperator) {

		case DoorOpenOperator.AND:

			if (!DoorOperatorAnd () && sentFromPlayerInput) {

				UIController.instance.ShowInformationText ("This device is locked elsewhere.", new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 0f));

			}
			break;

		case DoorOpenOperator.OR:

			if (!DoorOperatorOr () && sentFromPlayerInput) {

				UIController.instance.ShowInformationText ("This device is locked elsewhere.", new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 0f));

			}
			break;

		}

	}

	bool DoorOperatorAnd() {

		IsOn = (GetOperatorCount () >= _doorOperatorCountTotal) ? true : false;

		IsCurrentlyLocked = !IsOn;

		return IsOn;

	}

	bool DoorOperatorOr() {

		IsOn = (GetOperatorCount () > 0) ? true : false;

		IsCurrentlyLocked = !IsOn;

		return IsOn;

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