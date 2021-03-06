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

	[SerializeField]
	int _doorOperatorCount;
	public int DoorOperatorCount {

		get { return _doorOperatorCount; }
		set { _doorOperatorCount = value; }

	}

	[SerializeField]
	int _doorOperatorCountTotal = 0;
	public int DoorOperatorCountTotal {

		get { return _doorOperatorCountTotal; }
		set { _doorOperatorCountTotal = value; }

	}

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

		IsOn = (DoorOperatorCount >= DoorOperatorCountTotal) ? true : false;

		IsCurrentlyLocked = !IsOn;

		return IsOn;

	}

	bool DoorOperatorOr() {

		IsOn = (DoorOperatorCount > 0) ? true : false;

		IsCurrentlyLocked = !IsOn;

		return IsOn;

  	}

	public void ResetOperatorCount () {

		DoorOperatorCount = 0;
  
	}

}

public enum DoorOpenOperator { OR, AND };