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

				switch (IsOn) {

		case true: // Door is OPEN

			if (!CheckForPlayerInRadius ()) {
				IsOn = false;
			}
			break;

		case false:

			if (!IsCurrentlyLocked || Player.instance.GetKeyCount (KeyLocation) >= RequiredKeys) {

				IsCurrentlyLocked = false;
				Player.instance.DecrementKeyCount (KeyLocation, RequiredKeys);

				if (IsOneUseOnly) HasBeenUsedOnce = true;

				StartResetTimer ();
				IsOn = true;

			} else {

				UIController.instance.ShowInformationText ("This device is locked.", new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 0f));

			}
				
			break;

		}
			
	}

}