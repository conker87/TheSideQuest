using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Switch : Interactable {

	SwitchState _startingState;
	public SwitchState StartingState {

		get { return _startingState; } 
		set { _startingState = value; } 

	}

	SwitchState _currentState;
	public SwitchState CurrentState {

		get { return _currentState; } 
		set { _currentState = value; } 

	}

	[SerializeField]
	Interactable[] _connectedInteractables;
	public Interactable[] ConnectedInteractables {

		get { return _connectedInteractables; }
		set { _connectedInteractables = value; }

	}

	[SerializeField]
	KeyLocation _keyLocation;
	public KeyLocation KeyLocation {

		get { return _keyLocation; }
		set { _keyLocation = value; }

	}

	[SerializeField]
	int _requiredKeyCount = 0;
	public int RequiredKeyCount {

		get { return _requiredKeyCount; }
		set { _requiredKeyCount = value; }

	}

	public override void DoInteraction() {

		base.DoInteraction ();

		if (!CheckForConnectedInteractables ()) {

			print ("Interactable::Switch::DoInteraction -- No Connected Interactables.");
			_canContinue = false;

		}

		if (!CheckForRequiredKeys ()) {

			print ("Interactable::Switch::DoInteraction -- We do not have all the keys required from: " + KeyLocation.ToString());
			_canContinue = false;

		}

		if (!_canContinue) {

			return;

		}

		print ("Interactable::Switch::DoInteraction -- All is well, continuing with method.");

		switch (CurrentState) {

			case SwitchState.OFF:

				SwitchState_Off ();
				break;

			case SwitchState.TURNING_ON:

				SwitchState_Turning_On ();
				break;

			case SwitchState.ON:
				
				SwitchState_On ();
				break;

			case SwitchState.TURNING_OFF: 
				
				SwitchState_Turning_Off ();
				break;

		}

	}

	void SwitchState_Off() {



	}

	void SwitchState_Turning_On() {



	}

	void SwitchState_On() {

		

	}

	void SwitchState_Turning_Off() {



	}

	bool CheckForConnectedInteractables() {

		if (ConnectedInteractables == null || ConnectedInteractables.Count == 0) {

			return false;

		}

		return true;

	}

	bool CheckForRequiredKeys() {

		if (RequiredKeyCount < 1) {
			
			return true;

		}

		Player player = FindObjectOfType<Player> ();

		if (player == null) {

			print ("Interactable::Switch::CheckForRequiredKeys -- " + gameObject + " -- Player is null!");
			return false;

		}

		if (player.GetKeyCount(KeyLocation) >= RequiredKeyCount) {

			return true;

		}

		return false;

	}

	public void ChangeSwitchState(SwitchState state) {

		CurrentState = state;

	}

}

public enum SwitchState { OFF, TURNING_ON, ON, TURNING_OFF };
