using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : Door {

	[SerializeField]
	LevelDirection _keyLocation;
	public LevelDirection KeyLocation {

		get { return _keyLocation; }
		set { _keyLocation = value; }

	}

	[SerializeField]
	int _requiredKeys;
	public int RequiredKeys {

		get { return _requiredKeys; }
		set { _requiredKeys = value; }

	}

	public override void DoInteraction (bool sentFromPlayerInput = false)
	{

		if (player == null) {

			return;

		}

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

		DoorState stateToChange = (StartingState == DoorState.CLOSED) ? DoorState.OPENING : DoorState.CLOSING,
		previousState = (StartingState == DoorState.CLOSED) ? DoorState.CLOSING : DoorState.OPENING;

		if (CurrentState == StartingState) {

			if (!IsCurrentlyLocked || player.GetKeyCount (KeyLocation) >= RequiredKeys) {

				if (IsCurrentlyLocked) {

					IsCurrentlyLocked = false;
					player.DecrementKeyCount (KeyLocation, RequiredKeys);

				}

				StartResetTimer ();
				ChangeDoorState (stateToChange);

			}

		} else {
			
			ChangeDoorState (previousState);

		}

	}

}