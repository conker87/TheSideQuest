using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Switch : Interactable {

	[SerializeField]
	bool _fireOnChangeState;
	public bool FireOnAnyState {

		get { return _fireOnChangeState; } 
		set { _fireOnChangeState = value; } 

	}


	[SerializeField]
	SwitchState _startingState;
	public SwitchState StartingState {

		get { return _startingState; } 
		set { _startingState = value; } 

	}

	[SerializeField]
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

	protected override void Start() {

		base.Start ();

		if (CheckConnectedInteractablesForSelf ()) {

			Debug.LogError ("Switch: '" + gameObject + "' has itself as a connected interactable, this WILL break the game!");

		}

		// TODO: This needs to load settings from save file.
		CurrentState = StartingState;

		if (CurrentState == SwitchState.ON) {

			GetComponent<SpriteRenderer> ().flipY = true;

		} else if (CurrentState == SwitchState.OFF) {

			GetComponent<SpriteRenderer> ().flipY = false;

		}

	}

	public override void DoInteraction(bool sentFromPlayerInput = false) {

		base.DoInteraction (sentFromPlayerInput);

		if (!CheckForRequiredKeys ()) {

			print ("Interactable::Switch::DoInteraction -- We do not have all the keys required from: " + KeyLocation.ToString());
			_canContinue = false;

		}

		if (!_canContinue) {

			return;

		}

		print ("Interactable::Switch::DoInteraction -- All is well, continuing with method in: ");

		switch (CurrentState) {

		case SwitchState.ON:

			SwitchState_TurnOff ();
			break;

		case SwitchState.OFF:

			SwitchState_TurnOn ();
			break;

		}

	}

	void SwitchState_TurnOff() {

		if (FireOnAnyState) {

			foreach (Interactable interact in ConnectedInteractables) {

				interact.DoInteraction ();

			}

		}

		GetComponent<SpriteRenderer> ().flipY = false;

		ChangeSwitchState (SwitchState.OFF);

	}

	void SwitchState_TurnOn() {

		foreach (Interactable interact in ConnectedInteractables) {
			
			interact.DoInteraction ();

		}

		GetComponent<SpriteRenderer> ().flipY = true;

		ChangeSwitchState (SwitchState.ON);

	}

	bool CheckConnectedInteractablesForSelf() {

		if (ConnectedInteractables != null && ConnectedInteractables.Count() > 0) {

			foreach (Interactable interactable in ConnectedInteractables) {

				if (gameObject.GetInstanceID() == interactable.gameObject.GetInstanceID ()) {

					return true;

				}
					
			}

		}

		return false;

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

	void OnDrawGizmos() {

		if (ConnectedInteractables != null && ConnectedInteractables.Count() > 0) {

			foreach (Interactable interactable in ConnectedInteractables) {

				Gizmos.color = Color.blue;
				Gizmos.DrawWireSphere (transform.position, 0.2f);

				Gizmos.color = Color.green;
				Gizmos.DrawLine (transform.position, interactable.transform.position);

			}

		}


	}

}

public enum SwitchState { OFF, ON, };
