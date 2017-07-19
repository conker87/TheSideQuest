using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Switch : Interactable {

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

		if (StartingState == SwitchState.ON) {

			GetComponent<SpriteRenderer> ().flipY = true;

		} else if (StartingState == SwitchState.OFF) {

			GetComponent<SpriteRenderer> ().flipY = false;

		}

	}

	protected override void Update() {

		//GetComponent<SpriteRenderer> ().flipY = (CurrentState == SwitchState.ON) ? true : false;

	}

	public virtual void DoInteraction(bool sentFromPlayerInput = false) {

		base.DoInteraction (sentFromPlayerInput);

		if (!CheckForConnectedInteractables ()) {

			print ("Interactable::Switch::DoInteraction -- No Connected Interactables.");
			// _canContinue = false;

		}

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

//		case SwitchState.TURNING_ON:
//
//			SwitchState_Turning_On ();
//			break;
//
//		case SwitchState.TURNING_OFF: 
//			
//			SwitchState_Turning_Off ();
//			break;

		}

	}

	void SwitchState_TurnOff() {

		GetComponent<SpriteRenderer> ().flipY = false;

		ChangeSwitchState (SwitchState.OFF);

	}

	void SwitchState_TurnOn() {

		int i = 0;

		foreach (Interactable interact in ConnectedInteractables) {

			print ("Itterating: " + interact + " _ " + i.ToString());
			interact.DoInteraction ();

			i++;

		}

		GetComponent<SpriteRenderer> ().flipY = true;

		ChangeSwitchState (SwitchState.ON);

	}

//	void SwitchState_Off() {
//
//		ChangeSwitchState (SwitchState.TURNING_ON);
//
//		print ("Turning ON");
//
//	}
//
//	void SwitchState_Turning_On() {
//
//		foreach (Interactable interact in ConnectedInteractables) {
//
//			interact.DoInteraction ();
//
//		}
//
//		GetComponent<SpriteRenderer> ().flipY = false;
//
//		ChangeSwitchState (SwitchState.ON);
//
//		print ("ON");
//
//	}
//
//	void SwitchState_On() {
//		
//		ChangeSwitchState (SwitchState.TURNING_OFF);
//
//		print ("Turning OFF");
//
//	}
//
//	void SwitchState_Turning_Off() {
//
//		GetComponent<SpriteRenderer> ().flipY = true;
//
//		ChangeSwitchState (SwitchState.OFF);
//
//		print ("OFF");
//
//	}

	bool CheckForConnectedInteractables() {

		if (ConnectedInteractables == null || ConnectedInteractables.Count() == 0) {

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

public enum SwitchState { OFF, TURNING_ON, ON, TURNING_OFF };
