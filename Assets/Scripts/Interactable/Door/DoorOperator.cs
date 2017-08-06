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
		CheckInteraction ();

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

		IsOn = (GetOperatorCount () >= _doorOperatorCountTotal) ? true : false;

	}

	void DoorOperatorOr() {

		IsOn = (GetOperatorCount () > 0) ? true : false;

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