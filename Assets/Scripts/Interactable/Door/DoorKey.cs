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

	protected override void Start() {

		base.Start ();

	}

	public override void DoInteraction (bool sentFromPlayerInput = false)
	{

		CheckInteraction ();

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

		switch (IsOn) {

		case true: // Door is OPEN

			if (!CheckForPlayerInRadius ()) {
				IsOn = false;
			}
			break;

		case false:

			if (!IsCurrentlyLocked || Player.instance.GetKeyCount (KeyLocation) >= RequiredKeys) {

				if (IsCurrentlyLocked) {

					IsCurrentlyLocked = false;
					Player.instance.DecrementKeyCount (KeyLocation, RequiredKeys);

				}

				StartResetTimer ();
				IsOn = true;

			}
				
			break;

		}
			
	}

}