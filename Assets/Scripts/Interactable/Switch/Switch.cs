using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Switch : Interactable {

	[SerializeField]
	bool _interactOnAnyState = true;
	public bool InteractOnAnyState {

		get { return _interactOnAnyState; } 
		set { _interactOnAnyState = value; } 

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
	LevelDirection _keyLocation;
	public LevelDirection KeyLocation {

		get { return _keyLocation; }
		set { _keyLocation = value; }

	}

	[SerializeField]
	int _requiredKeyCount = 0;
	public int RequiredKeyCount {

		get { return _requiredKeyCount; }
		set { _requiredKeyCount = value; }

	}

	DoorOperator door;

	protected override void Start() {

		base.Start ();

		if (CheckConnectedInteractablesForSelf ()) {

			Debug.LogError ("Switch: '" + gameObject + "' has itself as a connected interactable, this WILL break the game!");

		}

		// Find all Doors connected to this Interactable and increment their OperatorCounter
		foreach (Interactable interactable in ConnectedInteractables) {

			DoorOperator current = interactable.GetComponent<DoorOperator>();

			if (current != null && current.dDoorOpenOperator == DoorOpenOperator.AND) {

				current.IncrementTotalOperatorCount ();

			}

		}

		// TODO: This needs to load settings from save file.
		CurrentState = StartingState;

		if (CurrentState == SwitchState.ON) {

			GetComponent<SpriteRenderer> ().flipY = true;

		} else if (CurrentState == SwitchState.OFF) {

			GetComponent<SpriteRenderer> ().flipY = false;

		}

	}

	protected override void Update() {

		base.Update ();

		DoReset ();

	}

	public override void DoInteraction(bool sentFromPlayerInput = false) {

		base.DoInteraction (sentFromPlayerInput);

		if (!CheckForRequiredKeys ()) {

			print ("Interactable::Switch::DoInteraction -- We do not have all the keys required from: " + KeyLocation.ToString());
			_canContinue = false;

		}

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

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

		if (InteractOnAnyState) {

			foreach (Interactable interact in ConnectedInteractables) {

				if ((door = interact.GetComponent<DoorOperator>()) != null) {

					door.DecrementOperatorCount();

				}

				interact.DoInteraction ();

			}

		}

		GetComponent<SpriteRenderer> ().flipY = false;

		if (StartingState == SwitchState.ON) {

			StartResetTimer ();

		}

		ChangeSwitchState (SwitchState.OFF);

	}

	void SwitchState_TurnOn() {

		foreach (Interactable interact in ConnectedInteractables) {

			if ((door = interact.GetComponent<DoorOperator>()) != null) {

				door.IncrementOperatorCount();
				print (door.GetOperatorCount() + " " + door.GetTotalOperatorCount());

			}

			interact.DoInteraction ();

		}

		GetComponent<SpriteRenderer> ().flipY = true;

		if (StartingState == SwitchState.OFF) {

			StartResetTimer ();

		}

		ChangeSwitchState (SwitchState.ON);

	}

	void StartResetTimer() {

		if (isResetInteractable) {

			_resetTimer = Time.time + ResetInSeconds;

		}
			
	}

	void DoReset() {

		if (isResetInteractable && CurrentState != StartingState && Time.time > _resetTimer) {

			if (StartingState == SwitchState.OFF) {

				SwitchState_TurnOff ();

			} else {

				SwitchState_TurnOn ();

			}

		}

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

	protected override void RenameGameObject() {

		string name = "Switch_(" + transform.position.x + "," + transform.position.y + ")_";

		name += GetInstanceID ();

		gameObject.name = name;

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
