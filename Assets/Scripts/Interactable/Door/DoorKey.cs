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
	float _requiredKeys;
	public float RequiredKeys {

		get { return _requiredKeys; }
		set { _requiredKeys = value; }

	}

	public override void DoInteraction (bool sentFromPlayerInput = false)
	{

		base.DoInteraction (sentFromPlayerInput);

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;


	}

}