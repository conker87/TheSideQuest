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

	[SerializeField]
	bool _startingOn;
	public bool StartingIsOn {

		get { return _startingOn; }
		set { _startingOn = value; }

	}

	[SerializeField]
	bool _isOn;
	public bool IsOn {

		get { return _isOn; }
		set { _isOn = value; }

	}

	DoorOperator door;
	Animator anim;

	protected override void Start() {

		base.Start ();

		if (SaveStateToFile) {
			GameSaveController.SwitchesInWorld.Add (this);
		}

		CheckConnectedInteractablesForSelf ();

		// Find all Doors connected to this Interactable and increment their OperatorCounter
		foreach (Interactable interactable in ConnectedInteractables) {

			DoorOperator current = interactable.GetComponent<DoorOperator>();

			if (current != null && current.dDoorOpenOperator == DoorOpenOperator.AND) {

				current.IncrementTotalOperatorCount ();
				print (current.GetTotalOperatorCount ());

			}

		}

		// TODO: This needs to load settings from save file.
		IsOn = StartingIsOn;

		anim = GetComponent<Animator> ();

	}

	protected override void Update() {

		base.Update ();

		DoReset ();

		anim.SetBool ("isOn", IsOn);

	}

	public override void DoInteraction(bool sentFromPlayerInput = false) {

		CheckInteraction ();

		if (!CheckForRequiredKeys ()) {

			print ("Interactable::Switch::DoInteraction -- We do not have all the keys required from: " + KeyLocation.ToString());
			_canContinue = false;

		}

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

		switch (IsOn) {

		case true:

			SwitchState_TurnOff ();
			break;

		case false:

			SwitchState_TurnOn ();
			break;

		}

	}

	public void ForceSwitchState( bool force ) {

		if (force) {

			SwitchState_TurnOn ();

		}

		if (!force) {

			SwitchState_TurnOff ();

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

		if ( StartingIsOn ) {

			StartResetTimer ();

		}

		IsOn = false;

	}

	void SwitchState_TurnOn() {

		foreach (Interactable interact in ConnectedInteractables) {

			if ((door = interact.GetComponent<DoorOperator>()) != null) {

				door.IncrementOperatorCount();

			}

			interact.DoInteraction ();

		}

		if ( StartingIsOn ) {

			StartResetTimer ();

		}

		IsOn = true;

	}

	void StartResetTimer() {

		if (isResetInteractable) {

			_resetTimer = Time.time + ResetInSeconds;

		}
			
	}

	void DoReset() {

		if (isResetInteractable && IsOn != StartingIsOn && Time.time > _resetTimer) {

			if ( !StartingIsOn ) {

				SwitchState_TurnOff ();

			} else {

				SwitchState_TurnOn ();

			}

		}

	}

	void CheckConnectedInteractablesForSelf() {

		if (ConnectedInteractables != null && ConnectedInteractables.Count() > 0) {

			foreach (Interactable interactable in ConnectedInteractables) {

				if (gameObject.GetInstanceID() == interactable.gameObject.GetInstanceID ()) {

					Debug.LogError ("Switch: '" + gameObject + "' has itself as a connected interactable!");

				}
					
			}

		}

	}

	bool CheckForRequiredKeys() {

		if (RequiredKeyCount < 1) {
			
			return true;

		}

		if (Player.instance.GetKeyCount(KeyLocation) >= RequiredKeyCount) {

			return true;

		}

		return false;

	}

	public void ChangeSwitchState(bool state) {

		IsOn = state;

	}

	protected override void RenameGameObject() {

		string name = "Switch_(" + transform.position.x + "," + transform.position.y + ")_";

		name += GetInstanceID ();

		gameObject.name = name;

	}

	void OnDrawGizmos() {

		if (ConnectedInteractables != null && ConnectedInteractables.Count() > 0) {

			foreach (Interactable interactable in ConnectedInteractables) {

				if (interactable != null) {

					Gizmos.color = Color.blue;
					Gizmos.DrawWireSphere (transform.position, 0.2f);

					Gizmos.color = Color.green;
					Gizmos.DrawLine (transform.position, interactable.transform.position);

				}

			}

		}


	}

}
